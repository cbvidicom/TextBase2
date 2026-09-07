namespace Textbase.Application.Features.ClientApplications;

public interface IClientApplicationServerQueries
	: IClientApplicationQueries
{
	Task<IReadOnlyDictionary<Guid, ClientApplicationReferenceCounts>> GetReferenceCountsAsync(
		IReadOnlyCollection<Guid> clientApplicationGuids, CancellationToken cancellationToken = default);
}
