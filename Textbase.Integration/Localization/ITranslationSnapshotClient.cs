using DM = Textbase.Domain.Models;

namespace Textbase.Integration.Localization;

public interface ITranslationSnapshotClient
{
	Task<IReadOnlyList<DM.FlatTranslation>> GetAsync(
		Guid clientApplicationGuid,
		CancellationToken cancellationToken = default);
}
