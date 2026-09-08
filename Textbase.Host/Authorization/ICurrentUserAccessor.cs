using System.Security.Claims;

namespace Textbase.Host.Authorization;

public interface ICurrentUserAccessor
{
	Task<ClaimsPrincipal> GetAsync();
}
