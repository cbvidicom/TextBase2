using DM = Textbase.Domain.Models;

namespace Textbase.Integration.Localization;

public interface ITranslationStore
{
	ValueTask<IReadOnlyCollection<DM.FlatTranslation>> GetAsync(
		CancellationToken cancellationToken = default);

	ValueTask SetAsync(
		IReadOnlyCollection<DM.FlatTranslation> translations,
		CancellationToken cancellationToken = default);
}
