/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using Uwn.Common.Querying;
using Uwn.EntityFrameworkCore.Querying;

namespace Textbase.Application.Features.ClientApplicationTextResources;

public sealed partial class ClientApplicationTextResourceFilter
	: QueryFilterBase
{
	public GuidFilter? ClientApplicationGuid { get; set; }
	public StringFilter? TextKey { get; set; }
	public StringFilter? ReferenceId { get; set; }
	
	public override bool IsEmpty =>
		!GuidFilter.IsSet(ClientApplicationGuid) &&
		!StringFilter.IsSet(TextKey) &&
		!StringFilter.IsSet(ReferenceId);

	public static ClientApplicationTextResourceFilter All()
		=> All<ClientApplicationTextResourceFilter>();
}