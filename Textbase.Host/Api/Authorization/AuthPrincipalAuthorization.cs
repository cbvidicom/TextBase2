using System.Security.Claims;
using Textbase.Application.Features.AuthPrincipals;
using Textbase.Contracts.Models;
using Textbase.Domain.Enumerations;
using Textbase.Host.Authorization;

namespace Textbase.Host.Api.Authorization;

public sealed class AuthPrincipalAuthorization(
	AuthorizationScope scope)
	: AuthorizationBase(scope)
	, IAuthPrincipalAuthorization
{
	public async ValueTask<bool> CanCreateAsync(AuthPrincipalDto dto,
		ClaimsPrincipal user,
		CancellationToken cancellationToken = default)
		=> await IsSysAdminAsync(cancellationToken);

	public async ValueTask<bool> CanReadAsync(Guid entraObjectId,
		ClaimsPrincipal user,
		CancellationToken cancellationToken = default)
		=> await IsSysAdminAsync(cancellationToken);

	public async ValueTask<bool> CanCountAsync(AuthPrincipalFilter filter,
		ClaimsPrincipal user,
		CancellationToken cancellationToken = default)
		=> await IsSysAdminAsync(cancellationToken);

	public async ValueTask<bool> CanListAsync(AuthPrincipalFilter filter,
		ClaimsPrincipal user,
		CancellationToken cancellationToken = default)
		=> await IsSysAdminAsync(cancellationToken);

	public async ValueTask<bool> CanUpdateAsync(Guid entraObjectId,
		AuthPrincipalDto dto,
		ClaimsPrincipal user,
		CancellationToken cancellationToken = default)
		=> await IsSysAdminAsync(cancellationToken);

	public async ValueTask<bool> CanDeleteAsync(Guid entraObjectId,
		ClaimsPrincipal user,
		CancellationToken cancellationToken = default)
		=> await IsSysAdminAsync(cancellationToken);
}
