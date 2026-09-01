/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using Uwn.Common.Querying;
using Uwn.EntityFrameworkCore.Querying;

namespace Textbase.Application.Features.Translations;

public sealed partial class TranslationFilter
	: QueryFilterBase
{
	public StringFilter? LocaleKey { get; set; }
	public StringFilter? TextKey { get; set; }
	public StringFilter? FormalityKey { get; set; }
	public StringFilter? PresentationKey { get; set; }
	public StringFilter? Value { get; set; }
	
	public override bool IsEmpty =>
		!StringFilter.IsSet(LocaleKey) &&
		!StringFilter.IsSet(TextKey) &&
		!StringFilter.IsSet(FormalityKey) &&
		!StringFilter.IsSet(PresentationKey) &&
		!StringFilter.IsSet(Value);

	public static TranslationFilter All()
		=> All<TranslationFilter>();
}