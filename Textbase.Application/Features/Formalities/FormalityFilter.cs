/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using Uwn.Common.Querying;
using Uwn.EntityFrameworkCore.Querying;

namespace Textbase.Application.Features.Formalities;

public sealed partial class FormalityFilter
	: QueryFilterBase
{
	public StringFilter? FormalityKey { get; set; }
	public StringFilter? Description { get; set; }
	
	public override bool IsEmpty =>
		!StringFilter.IsSet(FormalityKey) &&
		!StringFilter.IsSet(Description);

	public static FormalityFilter All()
		=> All<FormalityFilter>();
}