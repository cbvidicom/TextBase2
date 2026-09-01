/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using Uwn.Common.Querying;
using Uwn.EntityFrameworkCore.Querying;

namespace Textbase.Application.Features.FlatTranslations;

public sealed partial class FlatTranslationFilter
	: QueryFilterBase
{
	public StringFilter? LocaleKey { get; set; }
	public StringFilter? SourceLocaleKey { get; set; }
	public StringFilter? TextKey { get; set; }
	public StringFilter? FormalityKey { get; set; }
	public StringFilter? PresentationKey { get; set; }
	public StringFilter? Value { get; set; }
	
	public override bool IsEmpty =>
		!StringFilter.IsSet(LocaleKey) &&
		!StringFilter.IsSet(SourceLocaleKey) &&
		!StringFilter.IsSet(TextKey) &&
		!StringFilter.IsSet(FormalityKey) &&
		!StringFilter.IsSet(PresentationKey) &&
		!StringFilter.IsSet(Value);

	public static FlatTranslationFilter All()
		=> All<FlatTranslationFilter>();
}