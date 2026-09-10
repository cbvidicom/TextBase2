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

	public async Task<IReadOnlyList<MissingTranslation>> ListMissingTranslationsAsync(
		IReadOnlyCollection<Guid>? clientApplicationGuids,
		IReadOnlyCollection<string>? localeKeys,
		CancellationToken cancellationToken = default)
	{
		await using TextbaseDbContext dbContext = await _DbContextFactory.CreateDbContextAsync(cancellationToken);

		IQueryable<MissingTranslationApplication> query =
			from clientApplication in dbContext.ClientApplications
			join clientApplicationLocale in dbContext.ClientApplicationLocales on clientApplication.ClientApplicationGuid equals clientApplicationLocale.ClientApplicationGuid
			join clientApplicationTextResource in dbContext.ClientApplicationTextResources on clientApplication.ClientApplicationGuid equals clientApplicationTextResource.ClientApplicationGuid
			where clientApplication.IsActive &&
				!dbContext.FlatTranslations.Any(flatTranslation =>
					flatTranslation.LocaleKey == clientApplicationLocale.LocaleKey &&
					flatTranslation.TextKey == clientApplicationTextResource.TextKey &&
					flatTranslation.FormalityKey == "Default" &&
					flatTranslation.PresentationKey == "Default")
			select new MissingTranslationApplication(
				clientApplication.ClientApplicationGuid,
				clientApplication.Name,
				clientApplicationLocale.LocaleKey,
				clientApplicationTextResource.TextKey);

		if (clientApplicationGuids is not null)
		{
			query = query.Where(item => clientApplicationGuids.Contains(item.ClientApplicationGuid));
		}

		if (localeKeys is not null)
		{
			query = query.Where(item => localeKeys.Contains(item.LocaleKey));
		}

		MissingTranslationApplication[] items = await query.Distinct().ToArrayAsync(cancellationToken);

		return [.. items
			.GroupBy(item => new { item.LocaleKey, item.TextKey })
			.Select(group => new MissingTranslation(
				group.Key.LocaleKey,
				group.Key.TextKey,
				String.Join(", ", group.Select(item => item.ClientApplicationName).Distinct(StringComparer.OrdinalIgnoreCase).Order(StringComparer.OrdinalIgnoreCase))))
			.OrderBy(item => item.LocaleKey, StringComparer.OrdinalIgnoreCase)
			.ThenBy(item => item.TextKey, StringComparer.OrdinalIgnoreCase)];
	}

	private sealed record MissingTranslationApplication(
		Guid ClientApplicationGuid,
		string ClientApplicationName,
		string LocaleKey,
		string TextKey);
}
