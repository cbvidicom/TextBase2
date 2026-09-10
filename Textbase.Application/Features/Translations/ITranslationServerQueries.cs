namespace Textbase.Application.Features.Translations;

public interface ITranslationServerQueries
	: ITranslationQueries
{
	Task<bool> HasActiveApplicationAsync(string localeKey, string textKey, CancellationToken cancellationToken = default);
	Task<IReadOnlyList<MissingTranslationRequirement>> ListMissingTranslationsAsync(
		IReadOnlyCollection<Guid>? clientApplicationGuids, IReadOnlyCollection<string>? localeKeys, CancellationToken cancellationToken = default);
}
