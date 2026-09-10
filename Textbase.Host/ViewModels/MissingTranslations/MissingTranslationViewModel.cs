using Microsoft.EntityFrameworkCore;
using Textbase.Host.Authorization;
using Textbase.Infrastructure.Persistence;

namespace Textbase.Host.ViewModels.MissingTranslations;

public sealed class MissingTranslationViewModel(
	ICurrentPrincipalAccessor _currentPrincipalAccessor,
	IDbContextFactory<TextbaseDbContext> _dbContextFactory)
{
	public IReadOnlyList<MissingTranslation> Items { get; private set; } = [];

	public async Task LoadAsync(
		CancellationToken cancellationToken = default)
	{
		CurrentPrincipal? principal = await _currentPrincipalAccessor.GetAsync(cancellationToken);
		if (principal is null)
		{
			Items = [];
			return;
		}

		await using TextbaseDbContext dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

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

		if (principal.HasApplicationRestrictions)
		{
			Guid[] clientApplicationGuids = [.. principal.ClientApplicationGuids];
			query = query.Where(requirement => clientApplicationGuids.Contains(requirement.ClientApplicationGuid));
		}

		if (principal.HasLocaleRestrictions)
		{
			string[] localeKeys = [.. principal.LocaleKeys];
			query = query.Where(requirement => localeKeys.Contains(requirement.LocaleKey));
		}

		List<MissingTranslationRequirement> requirements = await query
			.Distinct()
			.ToListAsync(cancellationToken);

		Items = [.. requirements
			.GroupBy(requirement => new { requirement.LocaleKey, requirement.TextKey })
			.Select(group => new MissingTranslation(
				group.Key.LocaleKey,
				group.Key.TextKey,
				String.Join(", ", group.Select(requirement => requirement.ClientApplicationName).Distinct(StringComparer.OrdinalIgnoreCase).Order(StringComparer.OrdinalIgnoreCase))))
			.OrderBy(item => item.LocaleKey, StringComparer.OrdinalIgnoreCase)
			.ThenBy(item => item.TextKey, StringComparer.OrdinalIgnoreCase)];
	}

	private sealed record MissingTranslationRequirement(
		Guid ClientApplicationGuid,
		string ClientApplicationName,
		string LocaleKey,
		string TextKey);
}
