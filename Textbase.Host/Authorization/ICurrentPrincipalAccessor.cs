namespace Textbase.Host.Authorization;

public interface ICurrentPrincipalAccessor
{
	Task<CurrentPrincipal?> GetAsync(CancellationToken cancellationToken = default);
}
