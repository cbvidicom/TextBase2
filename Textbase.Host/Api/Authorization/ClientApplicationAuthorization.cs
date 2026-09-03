using System.Security.Claims;
using Textbase.Application.Features.ClientApplications;
using Textbase.Contracts.Models;
using Textbase.Domain.Enumerations;
using Textbase.Host.Authorization;
using Uwn.EntityFrameworkCore.Querying;

namespace Textbase.Host.Api.Authorization;

public sealed class ClientApplicationAuthorization(AuthorizationScope _scope)
	: IClientApplicationAuthorization
{
	public async ValueTask<bool> CanCreateAsync(ClientApplicationDto dto, ClaimsPrincipal user, CancellationToken cancellationToken = default)
		=> await HasRoleAsync(Roles.SysAdmin, cancellationToken);

	public async ValueTask<bool> CanReadAsync(
		Guid clientApplicationGuid,
		ClaimsPrincipal user,
		CancellationToken cancellationToken = default)
	{
		CurrentPrincipal? principal = await _scope.GetPrincipalAsync(cancellationToken);
		return principal is not null && AuthorizationScope.HasRole(principal, Roles.AppAdmin) &&
			AuthorizationScope.CanAccessApplication(principal, clientApplicationGuid);
	}

	public async ValueTask<bool> CanCountAsync(ClientApplicationFilter filter, ClaimsPrincipal user, CancellationToken cancellationToken = default)
		=> await RestrictListAsync(filter, cancellationToken);

	public async ValueTask<bool> CanListAsync(ClientApplicationFilter filter, ClaimsPrincipal user, CancellationToken cancellationToken = default)
		=> await RestrictListAsync(filter, cancellationToken);

	public async ValueTask<bool> CanUpdateAsync(
		Guid clientApplicationGuid,
		ClientApplicationDto dto,
		ClaimsPrincipal user,
		CancellationToken cancellationToken = default)
		=> await HasRoleAsync(Roles.SysAdmin, cancellationToken);

	public async ValueTask<bool> CanDeleteAsync(Guid clientApplicationGuid, ClaimsPrincipal user, CancellationToken cancellationToken = default)
		=> await HasRoleAsync(Roles.SysAdmin, cancellationToken);

	private async ValueTask<bool> RestrictListAsync(
		ClientApplicationFilter filter,
		CancellationToken cancellationToken)
	{
		CurrentPrincipal? principal = await _scope.GetPrincipalAsync(cancellationToken);
		if (principal is null || !AuthorizationScope.HasRole(principal, Roles.AppAdmin))
		{
			return false;
		}

		if (!AuthorizationScope.TryRestrictApplications(principal, filter.ClientApplicationGuid, out GuidFilter? restrictedFilter))
		{
			return false;
		}

		filter.ClientApplicationGuid = restrictedFilter;
		return true;
	}

	private async ValueTask<bool> HasRoleAsync(
		Roles roles,
		CancellationToken cancellationToken)
	{
		CurrentPrincipal? principal = await _scope.GetPrincipalAsync(cancellationToken);
		return principal is not null && AuthorizationScope.HasRole(principal, roles);
	}
}
