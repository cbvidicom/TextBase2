using DM = Textbase.Domain.Models;

namespace Textbase.Application.Features.FlatTranslations;

public interface IFlatTranslationRuntimeQueries
{
	Task<IReadOnlyList<DM.FlatTranslation>> ListForClientApplicationAsync(
		Guid clientApplicationGuid,
		CancellationToken cancellationToken = default);
}
