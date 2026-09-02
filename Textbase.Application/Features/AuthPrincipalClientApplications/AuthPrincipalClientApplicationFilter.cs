/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using Uwn.Common.Querying;
using Uwn.EntityFrameworkCore.Querying;

namespace Textbase.Application.Features.AuthPrincipalClientApplications;

public sealed partial class AuthPrincipalClientApplicationFilter
	: QueryFilterBase
{
	public GuidFilter? EntraObjectId { get; set; }
	public GuidFilter? ClientApplicationGuid { get; set; }
	
	public override bool IsEmpty =>
		!GuidFilter.IsSet(EntraObjectId) &&
		!GuidFilter.IsSet(ClientApplicationGuid);

	public static AuthPrincipalClientApplicationFilter All()
		=> All<AuthPrincipalClientApplicationFilter>();
}