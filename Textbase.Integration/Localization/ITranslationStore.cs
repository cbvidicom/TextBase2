using CM = Textbase.Contracts.Models;

namespace Textbase.Integration.Localization;

public interface ITranslationStore
{
	ValueTask<CM.RuntimeLocalizationSnapshotDto?> GetAsync(
		CancellationToken cancellationToken = default);

	ValueTask SetAsync(
		CM.RuntimeLocalizationSnapshotDto snapshot,
		CancellationToken cancellationToken = default);
}
