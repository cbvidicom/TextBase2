using DM = Textbase.Domain.Models;

namespace Textbase.Integration.Localization;

public sealed class TextbaseTranslationProvider(
	ITranslationSnapshotClient client,
	ITranslationStore store)
{
	public async Task<IReadOnlyCollection<DM.FlatTranslation>> RefreshAsync(
		Guid clientApplicationGuid,
		CancellationToken cancellationToken = default)
	{
		IReadOnlyList<DM.FlatTranslation> translations = await client.GetAsync(clientApplicationGuid, cancellationToken);
		await store.SetAsync(translations, cancellationToken);
		return translations;
	}
}
