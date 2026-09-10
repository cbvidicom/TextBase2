using CM = Textbase.Contracts.Models;

namespace Textbase.Integration.Localization;

public sealed class InMemoryTranslationStore
	: ITranslationStore
{
	private CM.RuntimeLocalizationSnapshotDto? _snapshot;

	public ValueTask<CM.RuntimeLocalizationSnapshotDto?> GetAsync(
		CancellationToken cancellationToken = default)
		=> ValueTask.FromResult(_snapshot);

	public ValueTask SetAsync(
		CM.RuntimeLocalizationSnapshotDto snapshot,
		CancellationToken cancellationToken = default)
	{
		ArgumentNullException.ThrowIfNull(snapshot);
		_snapshot = snapshot;
		return ValueTask.CompletedTask;
	}
}
