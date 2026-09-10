using Microsoft.EntityFrameworkCore;
using Textbase.Infrastructure.Persistence;

namespace Textbase.Application.Features.AuthPrincipals;

public sealed partial class AuthPrincipalQueries
	: IAuthPrincipalServerQueries
{
	public async Task<IReadOnlyDictionary<Guid, AuthPrincipalReferenceCounts>> GetReferenceCountsAsync(
		IReadOnlyCollection<Guid> entraObjectIds,
		CancellationToken cancellationToken = default)
	{
		await using TextbaseDbContext dbContext = await _DbContextFactory.CreateDbContextAsync(cancellationToken);

		AuthPrincipalReferenceCounts[] counts = await dbContext.AuthPrincipals
			.Where(principal => entraObjectIds.Contains(principal.EntraObjectId))
			.Select(principal => new AuthPrincipalReferenceCounts(
				principal.EntraObjectId,
				principal.AuthPrincipalClientApplications.Count,
				principal.AuthPrincipalLocales.Count))
			.ToArrayAsync(cancellationToken);

		return counts.ToDictionary(count => count.EntraObjectId);
	}
}
