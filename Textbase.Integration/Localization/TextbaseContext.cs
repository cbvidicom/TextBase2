namespace Textbase.Integration.Localization;

public sealed class TextbaseContext(
	string localeKey,
	string formalityKey = "Default",
	string presentationKey = "Default")
	: ITextbaseContext
{
	public string LocaleKey { get; set; } = localeKey;
	public string FormalityKey { get; set; } = formalityKey;
	public string PresentationKey { get; set; } = presentationKey;
}
