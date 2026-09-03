using Microsoft.EntityFrameworkCore;
using Textbase.Infrastructure.Persistence;

namespace Textbase.Application.Common;

internal static class CommandIntegrityExtensions
{
	public static Task<bool> HasActiveApplicationForTextAsync(
		this TextbaseDbContext dbContext,
		string textKey,
		CancellationToken cancellationToken)
		=> dbContext.ClientApplicationTextResources.AnyAsync(
			clientApplicationTextResource => clientApplicationTextResource.TextKey == textKey &&
				dbContext.ClientApplications.Any(clientApplication =>
					clientApplication.ClientApplicationGuid == clientApplicationTextResource.ClientApplicationGuid && clientApplication.IsActive),
			cancellationToken);

	public static Task<int> CountActiveApplicationsForTranslationAsync(
		this TextbaseDbContext dbContext,
		string localeKey,
		string textKey,
		CancellationToken cancellationToken)
		=> (
			from clientApplication in dbContext.ClientApplications
			join clientApplicationTextResource in dbContext.ClientApplicationTextResources
				on clientApplication.ClientApplicationGuid equals clientApplicationTextResource.ClientApplicationGuid
			join clientApplicationLocale in dbContext.ClientApplicationLocales
				on clientApplication.ClientApplicationGuid equals clientApplicationLocale.ClientApplicationGuid
			where clientApplication.IsActive &&
				clientApplicationTextResource.TextKey == textKey &&
				clientApplicationLocale.LocaleKey == localeKey
			select clientApplication.ClientApplicationGuid)
			.Distinct()
			.CountAsync(cancellationToken);

	public static Task<bool> HasConnectedTranslationsAsync(
		this TextbaseDbContext dbContext,
		Guid clientApplicationGuid,
		CancellationToken cancellationToken)
		=> dbContext.Translations.AnyAsync(
			translation =>
				dbContext.ClientApplicationTextResources.Any(clientApplicationTextResource =>
					clientApplicationTextResource.ClientApplicationGuid == clientApplicationGuid &&
					clientApplicationTextResource.TextKey == translation.TextKey) &&
				dbContext.ClientApplicationLocales.Any(clientApplicationLocale =>
					clientApplicationLocale.ClientApplicationGuid == clientApplicationGuid &&
					clientApplicationLocale.LocaleKey == translation.LocaleKey),
			cancellationToken);
}
