namespace Textbase.Application.Features.ClientApplications;

public partial interface IClientApplicationQueries
{
	Task<IReadOnlyDictionary<Guid, ClientApplicationReferenceCounts>> GetReferenceCountsAsync(
		IReadOnlyCollection<Guid> clientApplicationGuids, CancellationToken cancellationToken = default);
}
