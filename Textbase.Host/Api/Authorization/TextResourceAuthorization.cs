using System.Security.Claims;
using Textbase.Application.Features.TextResources;
using Textbase.Contracts.Models;
using Textbase.Domain.Enumerations;
using Textbase.Host.Authorization;
using Uwn.EntityFrameworkCore.Querying;

namespace Textbase.Host.Api.Authorization;

public sealed class TextResourceAuthorization(
	AuthorizationScope scope)
	: AuthorizationBase(scope)
	, ITextResourceAuthorization
{
	public async ValueTask<bool> CanCreateAsync(
		TextResourceDto dto,
		ClaimsPrincipal user,
		CancellationToken cancellationToken = default)
	{
		CurrentPrincipal? principal = await _scope.GetPrincipalAsync(cancellationToken);
		return principal is not null &&
			AuthorizationScope.HasRole(principal, Roles.Translator) &&
			(!principal.HasApplicationRestrictions && !principal.HasLocaleRestrictions);
	}

	public async ValueTask<bool> CanReadAsync(
		string textKey,
		ClaimsPrincipal user,
		CancellationToken cancellationToken = default)
		=> await CanAccessAsync(textKey, cancellationToken);

	public async ValueTask<bool> CanCountAsync(
		TextResourceFilter filter,
		ClaimsPrincipal user,
		CancellationToken cancellationToken = default)
		=> await CanAccessListAsync(filter, cancellationToken);

	public async ValueTask<bool> CanListAsync(
		TextResourceFilter filter,
		ClaimsPrincipal user,
		CancellationToken cancellationToken = default)
		=> await CanAccessListAsync(filter, cancellationToken);

	public async ValueTask<bool> CanUpdateAsync(
		string textKey,
		TextResourceDto dto,
		ClaimsPrincipal user,
		CancellationToken cancellationToken = default)
		=> await CanAccessAsync(textKey, cancellationToken) && await CanAccessAsync(dto.TextKey, cancellationToken);

	public async ValueTask<bool> CanDeleteAsync(
		string textKey,
		ClaimsPrincipal user,
		CancellationToken cancellationToken = default)
		=> await CanAccessAsync(textKey, cancellationToken);

	private async ValueTask<bool> CanAccessAsync(
		string textKey,
		CancellationToken cancellationToken)
	{
		CurrentPrincipal? principal = await _scope.GetPrincipalAsync(cancellationToken);
		return principal is not null &&
			AuthorizationScope.HasRole(principal, Roles.Translator) &&
			await _scope.CanAccessTextAsync(principal, textKey, cancellationToken);
	}

	private async ValueTask<bool> CanAccessListAsync(
		TextResourceFilter filter,
		CancellationToken cancellationToken)
	{
		CurrentPrincipal? principal = await _scope.GetPrincipalAsync(cancellationToken);
		if (principal is null ||
			!AuthorizationScope.HasRole(principal, Roles.Translator))
			return false;

		if (!principal.HasApplicationRestrictions &&
			!principal.HasLocaleRestrictions)
			return true;

		IReadOnlyCollection<string> permittedTextKeys = await _scope.GetPermittedTextKeysAsync(principal, cancellationToken);
		if (!AuthorizationScope.TryRestrictStrings(filter.TextKey, permittedTextKeys, out StringFilter? restrictedFilter))
			return false;

		filter.TextKey = restrictedFilter;

		return true;
	}
}
