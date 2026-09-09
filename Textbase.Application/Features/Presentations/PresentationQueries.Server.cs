using Microsoft.EntityFrameworkCore;
using Textbase.Infrastructure.Persistence;

namespace Textbase.Application.Features.Presentations;

public sealed partial class PresentationQueries
	: IPresentationServerQueries
{
	public async Task<int> GetReferenceCountAsync(
		string presentationKey,
		CancellationToken cancellationToken = default)
	{
		await using TextbaseDbContext dbContext = await _DbContextFactory.CreateDbContextAsync(cancellationToken);

		return await dbContext.Translations.CountAsync(translation => translation.PresentationKey == presentationKey, cancellationToken);
	}
}
