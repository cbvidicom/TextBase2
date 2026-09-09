namespace Textbase.Application.Features.Formalities;

public interface IFormalityServerQueries : IFormalityQueries
{
	Task<int> GetReferenceCountAsync(
		string formalityKey,
		CancellationToken cancellationToken = default);
}
