using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;

namespace Textbase.Integration.Localization;

public sealed class TextbaseTranslationProvider(
	ITranslationSnapshotClient client,
	ITranslationStore store)
{
	public async Task<CM.RuntimeLocalizationSnapshotDto> RefreshAsync(
		Guid clientApplicationGuid,
		CancellationToken cancellationToken = default)
	{
		CM.RuntimeLocalizationSnapshotDto snapshot = await client.GetAsync(clientApplicationGuid, cancellationToken);
		List<DM.FlatTranslation> translations = snapshot.Translations.Select(T => new DM.FlatTranslation
		{
			LocaleKey = T.LocaleKey,
			SourceLocaleKey = T.SourceLocaleKey,
			TextKey = T.TextKey,
			FormalityKey = T.FormalityKey,
			PresentationKey = T.PresentationKey,
			Value = T.Value
		}).ToList();

		await store.SetAsync(translations, cancellationToken);
		return snapshot;
	}
}
