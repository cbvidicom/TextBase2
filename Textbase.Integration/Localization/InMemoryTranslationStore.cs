using DM = Textbase.Domain.Models;

namespace Textbase.Integration.Localization;

public sealed class InMemoryTranslationStore
	: ITranslationStore
{
	private IReadOnlyCollection<DM.FlatTranslation> _translations = [];

	public ValueTask<IReadOnlyCollection<DM.FlatTranslation>> GetAsync(
		CancellationToken cancellationToken = default)
		=> ValueTask.FromResult(_translations);

	public ValueTask SetAsync(
		IReadOnlyCollection<DM.FlatTranslation> translations,
		CancellationToken cancellationToken = default)
	{
		ArgumentNullException.ThrowIfNull(translations);
		_translations = translations;
		return ValueTask.CompletedTask;
	}
}
