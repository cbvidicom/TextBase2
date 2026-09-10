using DM = Textbase.Domain.Models;

namespace Textbase.Integration.Localization;

public sealed class TextbaseLocalizer(
	ITextbaseContext context)
	: ITextbaseLocalizer
{
	private IReadOnlyDictionary<TranslationKey, string> _translations = new Dictionary<TranslationKey, string>();

	public void SetTranslations(
		IEnumerable<DM.FlatTranslation> translations)
	{
		ArgumentNullException.ThrowIfNull(translations);

		_translations = translations.ToDictionary(
		t => new TranslationKey(t.TextKey, t.LocaleKey, t.FormalityKey, t.PresentationKey),
		t => t.Value);
	}

	public string GetLocalisedText(
		string textKey,
		string? localeKey = null,
		string? formalityKey = null,
		string? presentationKey = null)
	{
		ArgumentException.ThrowIfNullOrWhiteSpace(textKey);

		TranslationKey key = new(
			textKey,
			localeKey ?? context.LocaleKey,
			formalityKey ?? context.FormalityKey,
			presentationKey ?? context.PresentationKey);

		return _translations.TryGetValue(key, out string? value) ? value : textKey;
	}
}
