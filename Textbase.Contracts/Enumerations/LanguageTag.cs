namespace Textbase.Contracts.Enumerations;

public enum LanguageTag
{
	Locale,
	Iso2,
	Iso3,
	IsoN,
	Lcid,
	WinApi
}

public static class LanguageTagExtensions
{
	public static string GetDisplayText(
		this LanguageTag? languageTag)
		=> languageTag switch
		{
			LanguageTag.Locale => "Locale",
			LanguageTag.Iso2 => "ISO-2",
			LanguageTag.Iso3 => "ISO-3",
			LanguageTag.IsoN => "ISO-N",
			LanguageTag.Lcid => "LCID",
			LanguageTag.WinApi => "Win API",
			_ => languageTag is null ? String.Empty : languageTag.ToString()!
		};

	public static LanguageTag? FromDisplayText(
		string? displayText)
		=> displayText switch
		{
			"Locale" => LanguageTag.Locale,
			"ISO-2" => LanguageTag.Iso2,
			"ISO-3" => LanguageTag.Iso3,
			"ISO-N" => LanguageTag.IsoN,
			"LCID" => LanguageTag.Lcid,
			"Win API" => LanguageTag.WinApi,
			_ => null
		};
}
