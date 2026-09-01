/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

namespace Textbase.Contracts.Models;

public partial class LocaleDto
{
	public required string LocaleKey { get; set; }
	public string? ParentLocaleKey { get; set; }
	public string? LanguageIso2 { get; set; }
	public string? LanguageIso3 { get; set; }
	public int? LanguageIsoN { get; set; }
	public int? LanguageLCID { get; set; }
	public string? LanguageWinApi { get; set; }
	public string? CountryIso2 { get; set; }
	public string? CountryIso3 { get; set; }
	public required string NativeName { get; set; }
	public required string EnglishName { get; set; }
}
