using CM = Textbase.Contracts.Models;

namespace Textbase.Integration.Localization;

public sealed class TextbaseLocalizer(
	ITextbaseContext context)
	: ITextbaseLocalizer
{
	private IReadOnlyDictionary<TranslationKey, string> _translations = new Dictionary<TranslationKey, string>();

	public void SetTranslations(
		IEnumerable<CM.FlatTranslationDto> translations)
	{
		ArgumentNullException.ThrowIfNull(translations);

		_translations = translations.ToDictionary(
		T => new TranslationKey(T.TextKey, T.LocaleKey, T.FormalityKey, T.PresentationKey),
		T => T.Value);
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
