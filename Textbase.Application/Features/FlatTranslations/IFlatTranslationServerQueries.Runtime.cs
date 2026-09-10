using CM = Textbase.Contracts.Models;

namespace Textbase.Application.Features.FlatTranslations;

public interface IFlatTranslationRuntimeQueries
{
	Task<CM.RuntimeLocalizationSnapshotDto?> GetClientApplicationSnapshotAsync(
		Guid clientApplicationGuid,
		CancellationToken cancellationToken = default);
}
