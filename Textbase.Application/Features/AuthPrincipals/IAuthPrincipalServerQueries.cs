namespace Textbase.Application.Features.AuthPrincipals;

public interface IAuthPrincipalServerQueries
	: IAuthPrincipalQueries
{
	Task<IReadOnlyDictionary<Guid, AuthPrincipalReferenceCounts>> GetReferenceCountsAsync(IReadOnlyCollection<Guid> entraObjectIds, CancellationToken cancellationToken = default);
}
