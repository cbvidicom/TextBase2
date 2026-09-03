using Microsoft.AspNetCore.Authorization;

namespace Textbase.Host.Authorization;

public sealed class ActivePrincipalAuthorizationHandler(
	ICurrentPrincipalAccessor _currentPrincipalAccessor,
	IHttpContextAccessor _httpContextAccessor)
	: AuthorizationHandler<ActivePrincipalRequirement>
{
	protected override async Task HandleRequirementAsync(
		AuthorizationHandlerContext context,
		ActivePrincipalRequirement requirement)
	{
		CancellationToken cancellationToken = _httpContextAccessor.HttpContext?.RequestAborted ?? default;
		CurrentPrincipal? principal = await _currentPrincipalAccessor.GetAsync(cancellationToken);

		if (principal is not null)
			context.Succeed(requirement);
	}
}
