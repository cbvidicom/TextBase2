using Microsoft.EntityFrameworkCore;
using Textbase.Infrastructure.Persistence;

namespace Textbase.Application.Features.TextResources;

public sealed partial class TextResourceQueries
	: ITextResourceServerQueries
{
	public async Task<IReadOnlyDictionary<string, TextResourceReferenceCounts>> GetReferenceCountsAsync(
		IReadOnlyCollection<string> textKeys,
		CancellationToken cancellationToken = default)
	{
		await using TextbaseDbContext dbContext = await _DbContextFactory.CreateDbContextAsync(cancellationToken);

		TextResourceReferenceCounts[] counts = await dbContext.TextResources
			.Where(textResource => textKeys.Contains(textResource.TextKey))
			.Select(textResource => new TextResourceReferenceCounts(
				textResource.TextKey,
				textResource.ClientApplicationTextResources.Count,
				textResource.Translations.Select(translation => translation.LocaleKey).Distinct().Count(),
				textResource.Translations.Count))
			.ToArrayAsync(cancellationToken);

		return counts.ToDictionary(count => count.TextKey);
	}
}
