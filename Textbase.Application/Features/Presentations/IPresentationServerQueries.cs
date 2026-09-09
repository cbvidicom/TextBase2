namespace Textbase.Application.Features.Presentations;

public interface IPresentationServerQueries : IPresentationQueries
{
	Task<int> GetReferenceCountAsync(
		string presentationKey,
		CancellationToken cancellationToken = default);
}
