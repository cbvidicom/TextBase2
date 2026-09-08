using System.Security.Claims;

namespace Textbase.Host.Authorization;

public interface ICurrentPrincipalAccessor
{
	Task<CurrentPrincipal?> GetAsync(CancellationToken cancellationToken = default);

	Task<ClaimsPrincipal> GetUserAsync();
}
