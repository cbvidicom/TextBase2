/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using Uwn.Common.Querying;
using Uwn.EntityFrameworkCore.Querying;

namespace Textbase.Application.Features.AuthPrincipalLocales;

public sealed partial class AuthPrincipalLocaleFilter
	: QueryFilterBase
{
	public GuidFilter? EntraObjectId { get; set; }
	public StringFilter? LocaleKey { get; set; }
	
	public override bool IsEmpty =>
		!GuidFilter.IsSet(EntraObjectId) &&
		!StringFilter.IsSet(LocaleKey);

	public static AuthPrincipalLocaleFilter All()
		=> All<AuthPrincipalLocaleFilter>();
}