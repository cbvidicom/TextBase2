namespace Textbase.Integration.Localization;

public interface ITextbaseContext
{
	string LocaleKey { get; set; }
	string FormalityKey { get; set; }
	string PresentationKey { get; set; }
}
