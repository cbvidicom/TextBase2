using System.Security.Claims;
using Textbase.Application.Features.ClientApplicationLocales;
using Textbase.Contracts.Models;
using Textbase.Domain.Enumerations;
using Textbase.Host.Authorization;
using Uwn.EntityFrameworkCore.Querying;

namespace Textbase.Host.Api.Authorization;

public sealed class ClientApplicationLocaleAuthorization(
	AuthorizationScope scope)
	: AuthorizationBase(scope)
	, IClientApplicationLocaleAuthorization
{
	public async ValueTask<bool> CanCreateAsync(
		ClientApplicationLocaleDto dto,
		ClaimsPrincipal user,
		CancellationToken cancellationToken = default)
		=> await CanManageApplicationAsync(dto.ClientApplicationGuid, cancellationToken);

	public async ValueTask<bool> CanReadAsync(
		Guid clientApplicationGuid,
		string localeKey,
		ClaimsPrincipal user,
		CancellationToken cancellationToken = default)
		=> await CanManageApplicationAsync(clientApplicationGuid, cancellationToken);

	public async ValueTask<bool> CanCountAsync(
		ClientApplicationLocaleFilter filter,
		ClaimsPrincipal user,
		CancellationToken cancellationToken = default)
		=> await RestrictListAsync(filter, cancellationToken);

	public async ValueTask<bool> CanListAsync(
		ClientApplicationLocaleFilter filter,
		ClaimsPrincipal user,
		CancellationToken cancellationToken = default)
		=> await RestrictListAsync(filter, cancellationToken);

	public async ValueTask<bool> CanUpdateAsync(
		Guid clientApplicationGuid,
		string localeKey,
		ClientApplicationLocaleDto dto,
		ClaimsPrincipal user,
		CancellationToken cancellationToken = default)
	{
		CurrentPrincipal? principal = await _scope.GetPrincipalAsync(cancellationToken);

		return principal is not null &&
			AuthorizationScope.HasRole(principal, Roles.AppAdmin) &&
			AuthorizationScope.CanAccessApplication(principal, clientApplicationGuid) &&
			AuthorizationScope.CanAccessApplication(principal, dto.ClientApplicationGuid);
	}

	public async ValueTask<bool> CanDeleteAsync(
		Guid clientApplicationGuid,
		string localeKey,
		ClaimsPrincipal user,
		CancellationToken cancellationToken = default)
		=> await CanManageApplicationAsync(clientApplicationGuid, cancellationToken);

	public async ValueTask<bool> CanReadByClientApplicationGuidAsync(
		Guid key,
		ClaimsPrincipal user,
		CancellationToken cancellationToken = default)
		=> await CanManageApplicationAsync(key, cancellationToken);

	private async ValueTask<bool> CanManageApplicationAsync(
		Guid clientApplicationGuid,
		CancellationToken cancellationToken)
	{
		CurrentPrincipal? principal = await _scope.GetPrincipalAsync(cancellationToken);

		return principal is not null &&
			AuthorizationScope.HasRole(principal, Roles.AppAdmin) &&
			AuthorizationScope.CanAccessApplication(principal, clientApplicationGuid);
	}

	private async ValueTask<bool> RestrictListAsync(
		ClientApplicationLocaleFilter filter,
		CancellationToken cancellationToken)
	{
		CurrentPrincipal? principal = await _scope.GetPrincipalAsync(cancellationToken);
		if (principal is null ||
			AuthorizationScope.HasRole(principal, Roles.AppAdmin) ||
			!AuthorizationScope.TryRestrictApplications(principal, filter.ClientApplicationGuid, out GuidFilter? restrictedFilter))
			return false;

		filter.ClientApplicationGuid = restrictedFilter;

		return true;
	}
}
