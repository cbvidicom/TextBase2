using System.Security.Claims;
using Microsoft.Identity.Web;
using Textbase.Application.Features.AuthPrincipalClientApplications;
using Textbase.Application.Features.AuthPrincipalLocales;
using Textbase.Application.Features.AuthPrincipals;
using Textbase.Contracts.Models;
using Textbase.Domain.Enumerations;
using Uwn.EntityFrameworkCore.Infrastructure;
using DM = Textbase.Domain.Models;

namespace Textbase.Host.Authorization;

public sealed class CurrentPrincipalAccessor(
	IHttpContextAccessor httpContextAccessor,
	IAuthPrincipalQueries authPrincipalQueries,
	IAuthPrincipalCommands authPrincipalCommands,
	IAuthPrincipalClientApplicationQueries authPrincipalClientApplicationQueries,
	IAuthPrincipalLocaleQueries authPrincipalLocaleQueries)
	: ICurrentPrincipalAccessor
{
	private Task<CurrentPrincipal?>? currentPrincipalTask;

	public Task<CurrentPrincipal?> GetAsync(CancellationToken cancellationToken = default)
	{
		currentPrincipalTask ??= LoadAsync(cancellationToken);

		return currentPrincipalTask;
	}

	private async Task<CurrentPrincipal?> LoadAsync(CancellationToken cancellationToken)
	{
		ClaimsPrincipal? user = httpContextAccessor.HttpContext?.User;
		string? objectId = user?.GetObjectId();

		if (user is null || !Guid.TryParse(objectId, out Guid entraObjectId))
		{
			return null;
		}

		DM.AuthPrincipal? principal = await authPrincipalQueries.ReadAsync(entraObjectId, cancellationToken);

		if (principal is null)
		{
			principal = await CreateAsync(user, entraObjectId, cancellationToken);
		}

		if (!principal.IsActive)
		{
			return null;
		}

		AuthPrincipalClientApplicationFilter applicationFilter = new()
		{
			EntraObjectId = entraObjectId
		};

		AuthPrincipalLocaleFilter localeFilter = new()
		{
			EntraObjectId = entraObjectId
		};

		Task<IReadOnlyList<DM.AuthPrincipalClientApplication>> applicationsTask =
			authPrincipalClientApplicationQueries.ListItemsAsync(applicationFilter, cancellationToken);

		Task<IReadOnlyList<DM.AuthPrincipalLocale>> localesTask = authPrincipalLocaleQueries.ListItemsAsync(localeFilter, cancellationToken);

		await Task.WhenAll(applicationsTask, localesTask);

		IReadOnlyList<DM.AuthPrincipalClientApplication> applications = await applicationsTask;
		IReadOnlyList<DM.AuthPrincipalLocale> locales = await localesTask;

		return new CurrentPrincipal(
			principal.EntraObjectId,
			principal.RolesValue,
			principal.DisplayName,
			principal.EmailAddress,
			[.. applications.Select(a => a.ClientApplicationGuid).Order()],
			[.. locales.Select(l => l.LocaleKey).Order(StringComparer.OrdinalIgnoreCase)]);
	}

	private async Task<DM.AuthPrincipal> CreateAsync(
		ClaimsPrincipal user,
		Guid entraObjectId,
		CancellationToken cancellationToken)
	{
		AuthPrincipalDto dto = new()
		{
			EntraObjectId = entraObjectId,
			Role = (int)Roles.None,
			DisplayName = LimitLength(user.FindFirstValue("name") ?? user.Identity?.Name, 128),
			EmailAddress = LimitLength(GetEmailAddress(user), 256),
			IsActive = true
		};

		CreateResult<DM.AuthPrincipal> result = await authPrincipalCommands.CreateAsync(dto, cancellationToken);

		if (result.Succeeded)
		{
			return result.Model;
		}

		return await authPrincipalQueries.ReadAsync(entraObjectId, cancellationToken)
			?? throw new InvalidOperationException("The authenticated principal could not be created.");
	}

	private static string? GetEmailAddress(ClaimsPrincipal user)
	{
		string[] claimTypes = [ClaimTypes.Email, "emails", "email", "preferred_username"];

		foreach (string claimType in claimTypes)
		{
			string? value = user.FindFirstValue(claimType);

			if (!string.IsNullOrWhiteSpace(value))
			{
				return value;
			}
		}

		return null;
	}

	private static string? LimitLength(string? value, int maximumLength)
		=> value is null || value.Length <= maximumLength ? value : value[..maximumLength];
}
