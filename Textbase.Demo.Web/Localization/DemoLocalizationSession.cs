using Textbase.Integration.Localization;
using CM = Textbase.Contracts.Models;

namespace Textbase.Demo.Web.Localization;

public sealed class DemoLocalizationSession(
	ITranslationStore store,
	TextbaseTranslationProvider provider,
	TextbaseContext context,
	TextbaseLocalizer localizer)
{
	public static readonly Guid ClientApplicationGuid = Guid.Parse("01a08b02-016b-7fa8-9bad-4d09466cc714");

	public async Task InitializeAsync(
		IReadOnlyList<string> browserLanguages,
		CancellationToken cancellationToken = default)
	{
		CM.RuntimeLocalizationSnapshotDto? snapshot = await store.GetAsync(cancellationToken);
		if (snapshot is null)
		{
			snapshot = await provider.RefreshAsync(ClientApplicationGuid, cancellationToken);
		}

		string localeKey = snapshot.DefaultLocaleKey;
		foreach (string browserLanguage in browserLanguages)
		{
			string? supportedLocale = snapshot.SupportedLocaleKeys.FirstOrDefault(L => String.Equals(L, browserLanguage, StringComparison.OrdinalIgnoreCase));
			if (supportedLocale is null)
			{
				continue;
			}

			localeKey = supportedLocale;
			break;
		}

		context.LocaleKey = localeKey;
		localizer.SetTranslations(snapshot.Translations);
	}
}
