using System.Security.Claims;
using Textbase.Application.Features.AuthPrincipalLocales;
using Textbase.Contracts.Models;
using Textbase.Domain.Enumerations;
using Textbase.Host.Authorization;

namespace Textbase.Host.Api.Authorization;

public sealed class AuthPrincipalLocaleAuthorization(AuthorizationScope _scope)
	: IAuthPrincipalLocaleAuthorization
{
	public ValueTask<bool> CanCreateAsync(AuthPrincipalLocaleDto dto, ClaimsPrincipal user, CancellationToken cancellationToken = default)
		=> IsSysAdminAsync(cancellationToken);

	public ValueTask<bool> CanReadAsync(Guid entraObjectId, string localeKey, ClaimsPrincipal user, CancellationToken cancellationToken = default)
		=> IsSysAdminAsync(cancellationToken);

	public ValueTask<bool> CanCountAsync(AuthPrincipalLocaleFilter filter, ClaimsPrincipal user, CancellationToken cancellationToken = default)
		=> IsSysAdminAsync(cancellationToken);

	public ValueTask<bool> CanListAsync(AuthPrincipalLocaleFilter filter, ClaimsPrincipal user, CancellationToken cancellationToken = default)
		=> IsSysAdminAsync(cancellationToken);

	public ValueTask<bool> CanUpdateAsync(Guid entraObjectId, string localeKey, AuthPrincipalLocaleDto dto, ClaimsPrincipal user,
		CancellationToken cancellationToken = default)
		=> IsSysAdminAsync(cancellationToken);

	public ValueTask<bool> CanDeleteAsync(Guid entraObjectId, string localeKey, ClaimsPrincipal user, CancellationToken cancellationToken = default)
		=> IsSysAdminAsync(cancellationToken);

	private async ValueTask<bool> IsSysAdminAsync(
		CancellationToken cancellationToken)
	{
		CurrentPrincipal? principal = await _scope.GetPrincipalAsync(cancellationToken);
		return principal is not null && principal.RolesValue.HasFlag(Roles.SysAdmin);
	}
}
