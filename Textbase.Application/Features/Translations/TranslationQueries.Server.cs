using Microsoft.EntityFrameworkCore;
using Textbase.Infrastructure.Persistence;

namespace Textbase.Application.Features.Translations;

public sealed partial class TranslationQueries
	: ITranslationServerQueries
{
	public async Task<bool> HasActiveApplicationAsync(
		string localeKey,
		string textKey,
		CancellationToken cancellationToken = default)
	{
		await using TextbaseDbContext dbContext = await _DbContextFactory.CreateDbContextAsync(cancellationToken);

		return await dbContext.ClientApplications.AnyAsync(
			clientApplication => clientApplication.IsActive &&
				clientApplication.ClientApplicationLocales.Any(locale => locale.LocaleKey == localeKey) &&
				clientApplication.ClientApplicationTextResources.Any(textResource => textResource.TextKey == textKey),
			cancellationToken);
	}

	public async Task<IReadOnlyList<MissingTranslationRequirement>> ListMissingTranslationsAsync(
		IReadOnlyCollection<Guid>? clientApplicationGuids,
		IReadOnlyCollection<string>? localeKeys,
		CancellationToken cancellationToken = default)
	{
		await using TextbaseDbContext dbContext = await _DbContextFactory.CreateDbContextAsync(cancellationToken);

		IQueryable<MissingTranslationRequirement> query =
			from clientApplication in dbContext.ClientApplications
			join clientApplicationLocale in dbContext.ClientApplicationLocales on clientApplication.ClientApplicationGuid equals clientApplicationLocale.ClientApplicationGuid
			join clientApplicationTextResource in dbContext.ClientApplicationTextResources on clientApplication.ClientApplicationGuid equals clientApplicationTextResource.ClientApplicationGuid
			where clientApplication.IsActive &&
				!dbContext.FlatTranslations.Any(flatTranslation =>
					flatTranslation.LocaleKey == clientApplicationLocale.LocaleKey &&
					flatTranslation.TextKey == clientApplicationTextResource.TextKey &&
					flatTranslation.FormalityKey == "Default" &&
					flatTranslation.PresentationKey == "Default")
			select new MissingTranslationRequirement(
				clientApplication.ClientApplicationGuid,
				clientApplication.Name,
				clientApplicationLocale.LocaleKey,
				clientApplicationTextResource.TextKey);

		if (clientApplicationGuids is not null)
		{
			query = query.Where(requirement => clientApplicationGuids.Contains(requirement.ClientApplicationGuid));
		}

		if (localeKeys is not null)
		{
			query = query.Where(requirement => localeKeys.Contains(requirement.LocaleKey));
		}

		return await query.Distinct().ToArrayAsync(cancellationToken);
	}
}
