using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Textbase.Host.Authorization;

public sealed class CurrentUserAccessor(
	AuthenticationStateProvider authenticationStateProvider)
	: ICurrentUserAccessor
{
	public async Task<ClaimsPrincipal> GetAsync()
		=> (await authenticationStateProvider.GetAuthenticationStateAsync()).User;
}
