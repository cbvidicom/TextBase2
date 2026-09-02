/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using Uwn.Common.Querying;
using Uwn.EntityFrameworkCore.Querying;

namespace Textbase.Application.Features.AuthPrincipals;

public sealed partial class AuthPrincipalFilter
	: QueryFilterBase
{
	public GuidFilter? EntraObjectId { get; set; }
	public NumericFilter<int>? Role { get; set; }
	public StringFilter? DisplayName { get; set; }
	public StringFilter? EmailAddress { get; set; }
	
	public override bool IsEmpty =>
		!GuidFilter.IsSet(EntraObjectId) &&
		!NumericFilter.IsSet(Role) &&
		!StringFilter.IsSet(DisplayName) &&
		!StringFilter.IsSet(EmailAddress);

	public static AuthPrincipalFilter All()
		=> All<AuthPrincipalFilter>();
}