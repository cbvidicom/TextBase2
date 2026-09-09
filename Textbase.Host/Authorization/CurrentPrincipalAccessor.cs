using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Identity.Web;
using System.Security.Claims;
using Textbase.Application.Features.AuthPrincipalClientApplications;
using Textbase.Application.Features.AuthPrincipalLocales;
using Textbase.Application.Features.AuthPrincipals;
using Textbase.Domain.Models;

namespace Textbase.Host.Authorization;

public sealed class CurrentPrincipalAccessor(
	AuthenticationStateProvider _authenticationStateProvider,
	IAuthPrincipalClientApplicationQueries _authPrincipalClientApplicationQueries,
	IAuthPrincipalLocaleQueries _authPrincipalLocaleQueries,
	IAuthPrincipalQueries _authPrincipalQueries,
	IHttpContextAccessor _httpContextAccessor)
	: ICurrentPrincipalAccessor
{
	private Task<CurrentPrincipal?>? _currentPrincipalTask;

	//

	public Task<CurrentPrincipal?> GetAsync(
		CancellationToken cancellationToken = default)
	{
		_currentPrincipalTask ??= LoadAsync(cancellationToken);

		return _currentPrincipalTask;
	}

	public async Task<ClaimsPrincipal> GetUserAsync()
	{
		ClaimsPrincipal? httpUser = _httpContextAccessor.HttpContext?.User;

		if (httpUser?.Identity?.IsAuthenticated == true)
			return httpUser;

		AuthenticationState authenticationState = await _authenticationStateProvider.GetAuthenticationStateAsync();

		return authenticationState.User;
	}

	//

	private async Task<CurrentPrincipal?> LoadAsync(
		CancellationToken cancellationToken)
	{
		ClaimsPrincipal user = await GetUserAsync();
		string? objectId = user.GetObjectId();

		if (user.Identity?.IsAuthenticated != true ||
			!Guid.TryParse(objectId, out Guid entraObjectId))
			return null;

		AuthPrincipal? principal = await _authPrincipalQueries.ReadAsync(entraObjectId, cancellationToken);

		if (principal is null || !principal.IsActive)
			return null;

		AuthPrincipalClientApplicationFilter applicationFilter = new()
		{
			EntraObjectId = entraObjectId
		};

		AuthPrincipalLocaleFilter localeFilter = new()
		{
			EntraObjectId = entraObjectId
		};

		Task<IReadOnlyList<AuthPrincipalClientApplication>> applicationsTask =
			_authPrincipalClientApplicationQueries.ListItemsAsync(applicationFilter, cancellationToken);

		Task<IReadOnlyList<AuthPrincipalLocale>> localesTask = _authPrincipalLocaleQueries.ListItemsAsync(localeFilter, cancellationToken);

		await Task.WhenAll(applicationsTask, localesTask);

		IReadOnlyList<AuthPrincipalClientApplication> applications = await applicationsTask;
		IReadOnlyList<AuthPrincipalLocale> locales = await localesTask;

		return new CurrentPrincipal(
			principal.EntraObjectId,
			principal.RolesValue,
			principal.DisplayName,
			principal.EmailAddress,
			[.. applications.Select(a => a.ClientApplicationGuid).Order()],
			[.. locales.Select(l => l.LocaleKey).Order(StringComparer.OrdinalIgnoreCase)]);
	}
}
