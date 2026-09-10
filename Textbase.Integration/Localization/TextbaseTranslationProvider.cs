using CM = Textbase.Contracts.Models;

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
		await store.SetAsync(snapshot, cancellationToken);
		return snapshot;
	}
}
