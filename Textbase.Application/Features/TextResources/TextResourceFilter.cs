/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using Uwn.Common.Querying;
using Uwn.EntityFrameworkCore.Querying;

namespace Textbase.Application.Features.TextResources;

public sealed partial class TextResourceFilter
	: QueryFilterBase
{
	public StringFilter? TextKey { get; set; }
	public StringFilter? Description { get; set; }
	
	public override bool IsEmpty =>
		!StringFilter.IsSet(TextKey) &&
		!StringFilter.IsSet(Description);

	public static TextResourceFilter All()
		=> All<TextResourceFilter>();
}