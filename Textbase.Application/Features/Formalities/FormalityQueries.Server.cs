using Microsoft.EntityFrameworkCore;
using Textbase.Infrastructure.Persistence;

namespace Textbase.Application.Features.Formalities;

public sealed partial class FormalityQueries
	: IFormalityServerQueries
{
	public async Task<int> GetReferenceCountAsync(
		string formalityKey,
		CancellationToken cancellationToken = default)
	{
		await using TextbaseDbContext dbContext = await _DbContextFactory.CreateDbContextAsync(cancellationToken);

		return await dbContext.Translations.CountAsync(translation => translation.FormalityKey == formalityKey, cancellationToken);
	}
}
