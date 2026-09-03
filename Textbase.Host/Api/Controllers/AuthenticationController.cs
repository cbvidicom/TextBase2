using Microsoft.AspNetCore.Mvc;
using Textbase.Host.Authorization;

namespace Textbase.Host.Api.Controllers;

[ApiController]
[Route("api/auth")]
public sealed class AuthenticationController(
	ICurrentPrincipalAccessor currentPrincipalAccessor)
	: ControllerBase
{
	[HttpGet("me")]
	public async Task<ActionResult<CurrentPrincipal>> Read(
		CancellationToken cancellationToken)
	{
		CurrentPrincipal? principal = await currentPrincipalAccessor.GetAsync(cancellationToken);

		return principal is null ? Forbid() : Ok(principal);
	}
}
