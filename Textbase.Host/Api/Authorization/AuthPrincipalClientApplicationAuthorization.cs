using System.Security.Claims;
using Textbase.Application.Features.AuthPrincipalClientApplications;
using Textbase.Contracts.Models;
using Textbase.Domain.Enumerations;
using Textbase.Host.Authorization;

namespace Textbase.Host.Api.Authorization;

public sealed class AuthPrincipalClientApplicationAuthorization(AuthorizationScope _scope)
	: IAuthPrincipalClientApplicationAuthorization
{
	public ValueTask<bool> CanCreateAsync(AuthPrincipalClientApplicationDto dto, ClaimsPrincipal user, CancellationToken cancellationToken = default)
		=> IsSysAdminAsync(cancellationToken);

	public ValueTask<bool> CanReadAsync(Guid entraObjectId, Guid clientApplicationGuid, ClaimsPrincipal user, CancellationToken cancellationToken = default)
		=> IsSysAdminAsync(cancellationToken);

	public ValueTask<bool> CanCountAsync(AuthPrincipalClientApplicationFilter filter, ClaimsPrincipal user, CancellationToken cancellationToken = default)
		=> IsSysAdminAsync(cancellationToken);

	public ValueTask<bool> CanListAsync(AuthPrincipalClientApplicationFilter filter, ClaimsPrincipal user, CancellationToken cancellationToken = default)
		=> IsSysAdminAsync(cancellationToken);

	public ValueTask<bool> CanUpdateAsync(Guid entraObjectId, Guid clientApplicationGuid, AuthPrincipalClientApplicationDto dto, ClaimsPrincipal user,
		CancellationToken cancellationToken = default)
		=> IsSysAdminAsync(cancellationToken);

	public ValueTask<bool> CanDeleteAsync(Guid entraObjectId, Guid clientApplicationGuid, ClaimsPrincipal user, CancellationToken cancellationToken = default)
		=> IsSysAdminAsync(cancellationToken);

	private async ValueTask<bool> IsSysAdminAsync(
		CancellationToken cancellationToken)
	{
		CurrentPrincipal? principal = await _scope.GetPrincipalAsync(cancellationToken);
		return principal is not null && principal.RolesValue.HasFlag(Roles.SysAdmin);
	}
}
