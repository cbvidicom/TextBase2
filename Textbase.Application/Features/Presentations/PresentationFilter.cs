/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using Uwn.Common.Querying;
using Uwn.EntityFrameworkCore.Querying;

namespace Textbase.Application.Features.Presentations;

public sealed partial class PresentationFilter
	: QueryFilterBase
{
	public StringFilter? PresentationKey { get; set; }
	public StringFilter? Description { get; set; }
	
	public override bool IsEmpty =>
		!StringFilter.IsSet(PresentationKey) &&
		!StringFilter.IsSet(Description);

	public static PresentationFilter All()
		=> All<PresentationFilter>();
}