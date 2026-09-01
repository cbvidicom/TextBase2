/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using Uwn.Common.Querying;
using Uwn.EntityFrameworkCore.Querying;

namespace Textbase.Application.Features.Locales;

public sealed partial class LocaleFilter
	: QueryFilterBase
{
	public StringFilter? LocaleKey { get; set; }
	public StringFilter? ParentLocaleKey { get; set; }
	public StringFilter? LanguageIso2 { get; set; }
	public StringFilter? LanguageIso3 { get; set; }
	public NumericFilter<int>? LanguageIsoN { get; set; }
	public NumericFilter<int>? LanguageLCID { get; set; }
	public StringFilter? LanguageWinApi { get; set; }
	public StringFilter? CountryIso2 { get; set; }
	public StringFilter? CountryIso3 { get; set; }
	public StringFilter? NativeName { get; set; }
	public StringFilter? EnglishName { get; set; }
	
	public override bool IsEmpty =>
		!StringFilter.IsSet(LocaleKey) &&
		!StringFilter.IsSet(ParentLocaleKey) &&
		!StringFilter.IsSet(LanguageIso2) &&
		!StringFilter.IsSet(LanguageIso3) &&
		!NumericFilter.IsSet(LanguageIsoN) &&
		!NumericFilter.IsSet(LanguageLCID) &&
		!StringFilter.IsSet(LanguageWinApi) &&
		!StringFilter.IsSet(CountryIso2) &&
		!StringFilter.IsSet(CountryIso3) &&
		!StringFilter.IsSet(NativeName) &&
		!StringFilter.IsSet(EnglishName);

	public static LocaleFilter All()
		=> All<LocaleFilter>();
}