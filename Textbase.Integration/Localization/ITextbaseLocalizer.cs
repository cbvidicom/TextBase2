namespace Textbase.Integration.Localization;

public interface ITextbaseLocalizer
{
	string GetLocalisedText(
		string textKey,
		string? localeKey = null,
		string? formalityKey = null,
		string? presentationKey = null);
}
