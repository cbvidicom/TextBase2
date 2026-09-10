namespace Textbase.Application.Features.TextResources;

public interface ITextResourceServerQueries
	: ITextResourceQueries
{
	Task<IReadOnlyDictionary<string, TextResourceReferenceCounts>> GetReferenceCountsAsync(
		IReadOnlyCollection<string> textKeys, CancellationToken cancellationToken = default);

	Task<bool> HasActiveApplicationAsync(
		string textKey, CancellationToken cancellationToken = default);
}
