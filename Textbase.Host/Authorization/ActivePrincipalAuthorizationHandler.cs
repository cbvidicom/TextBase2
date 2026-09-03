using Microsoft.AspNetCore.Authorization;

namespace Textbase.Host.Authorization;

public sealed class ActivePrincipalAuthorizationHandler(
	ICurrentPrincipalAccessor currentPrincipalAccessor,
	IHttpContextAccessor httpContextAccessor)
	: AuthorizationHandler<ActivePrincipalRequirement>
{
	protected override async Task HandleRequirementAsync(
		AuthorizationHandlerContext context,
		ActivePrincipalRequirement requirement)
	{
		CancellationToken cancellationToken = httpContextAccessor.HttpContext?.RequestAborted ?? default;
		CurrentPrincipal? principal = await currentPrincipalAccessor.GetAsync(cancellationToken);

		if (principal is not null)
		{
			context.Succeed(requirement);
		}
	}
}
