using Microsoft.Identity.Web;
using System.Security.Claims;
using Textbase.Application.Features.AuthPrincipalClientApplications;
using Textbase.Application.Features.AuthPrincipalLocales;
using Textbase.Application.Features.AuthPrincipals;
using Textbase.Domain.Enumerations;
using Textbase.Domain.Models;
using Textbase.Infrastructure.Persistence.AuthPrincipals;

namespace Textbase.Host.Authorization;

public sealed class CurrentPrincipalAccessor(
	IAuthPrincipalClientApplicationQueries _authPrincipalClientApplicationQueries,
	IAuthPrincipalCommands _authPrincipalCommands,
	IAuthPrincipalEntityFactory _authPrincipalEntityFactory,
	IAuthPrincipalLocaleQueries _authPrincipalLocaleQueries,
	IAuthPrincipalQueries _authPrincipalQueries,
	IHttpContextAccessor _httpContextAccessor)
	: ICurrentPrincipalAccessor
{
	private Task<CurrentPrincipal?>? currentPrincipalTask;

	//

	public Task<CurrentPrincipal?> GetAsync(
		CancellationToken cancellationToken = default)
	{
		currentPrincipalTask ??= LoadAsync(cancellationToken);

		return currentPrincipalTask;
	}

	//

	private async Task<CurrentPrincipal?> LoadAsync(
		CancellationToken cancellationToken)
	{
		ClaimsPrincipal? user = _httpContextAccessor.HttpContext?.User;
		string? objectId = user?.GetObjectId();

		if (user is null ||
			!Guid.TryParse(objectId, out Guid entraObjectId))
			return null;

		AuthPrincipal? principal = await _authPrincipalQueries.ReadAsync(entraObjectId, cancellationToken);

		principal ??= await CreateAsync(user, entraObjectId, cancellationToken);

		if (!principal.IsActive)
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

	private async Task<AuthPrincipal> CreateAsync(
		ClaimsPrincipal user,
		Guid entraObjectId,
		CancellationToken cancellationToken)
	{
		AuthPrincipal principal = _authPrincipalEntityFactory.Create(entraObjectId, (int)Roles.None, true);
		principal.DisplayName = LimitLength(user.FindFirstValue("name") ?? user.Identity?.Name, 128);
		principal.EmailAddress = LimitLength(GetEmailAddress(user), 256);

		if (await _authPrincipalCommands.TryCreateAsync(principal, cancellationToken))
			return principal;

		return await _authPrincipalQueries.ReadAsync(entraObjectId, cancellationToken)
			?? throw new InvalidOperationException("The authenticated principal could not be created.");
	}

	private static string? GetEmailAddress(
		ClaimsPrincipal user)
	{
		string[] claimTypes = [ClaimTypes.Email, "emails", "email", "preferred_username"];

		foreach (string claimType in claimTypes)
		{
			string? value = user.FindFirstValue(claimType);

			if (!String.IsNullOrWhiteSpace(value))
				return value;
		}

		return null;
	}

	private static string? LimitLength(
		string? value,
		int maximumLength)
		=> value is null || value.Length <= maximumLength
		? value
		: value[..maximumLength];
}
