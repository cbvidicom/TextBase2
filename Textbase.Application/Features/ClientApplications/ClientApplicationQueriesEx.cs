using Microsoft.EntityFrameworkCore;
using Textbase.Infrastructure.Persistence;

namespace Textbase.Application.Features.ClientApplications;

public sealed partial class ClientApplicationQueries
{
	public async Task<IReadOnlyDictionary<Guid, ClientApplicationReferenceCounts>> GetReferenceCountsAsync(
		IReadOnlyCollection<Guid> clientApplicationGuids,
		CancellationToken cancellationToken = default)
	{
		await using TextbaseDbContext dbContext = await _DbContextFactory.CreateDbContextAsync(cancellationToken);

		ClientApplicationReferenceCounts[] counts = await dbContext.ClientApplications
			.Where(application => clientApplicationGuids.Contains(application.ClientApplicationGuid))
			.Select(application => new ClientApplicationReferenceCounts(
				application.ClientApplicationGuid,
				application.ClientApplicationLocales.Count,
				application.ClientApplicationTextResources.Count))
			.ToArrayAsync(cancellationToken);

		return counts.ToDictionary(count => count.ClientApplicationGuid);
	}
}
