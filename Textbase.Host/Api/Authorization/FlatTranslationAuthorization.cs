using System.Security.Claims;
using Textbase.Application.Features.FlatTranslations;
using Textbase.Contracts.Models;
using Textbase.Domain.Enumerations;
using Textbase.Host.Authorization;

namespace Textbase.Host.Api.Authorization;

public sealed class FlatTranslationAuthorization(
	AuthorizationScope scope)
	: AuthorizationBase(scope)
	, IFlatTranslationAuthorization
{
	public ValueTask<bool> CanCreateAsync(
		FlatTranslationDto dto,
		ClaimsPrincipal user,
		CancellationToken cancellationToken = default)
		=> ValueTask.FromResult(false);

	public async ValueTask<bool> CanReadAsync(
		string localeKey,
		string textKey,
		string formalityKey,
		string presentationKey,
		ClaimsPrincipal user,
		CancellationToken cancellationToken = default)
		=> await CanAccessAsync(localeKey, textKey, cancellationToken);

	public async ValueTask<bool> CanCountAsync(
		FlatTranslationFilter filter,
		ClaimsPrincipal user, CancellationToken cancellationToken = default)
		=> await CanAccessListAsync(filter, cancellationToken);

	public async ValueTask<bool> CanListAsync(
		FlatTranslationFilter filter,
		ClaimsPrincipal user,
		CancellationToken cancellationToken = default)
		=> await CanAccessListAsync(filter, cancellationToken);

	public ValueTask<bool> CanUpdateAsync(
		string localeKey,
		string textKey,
		string formalityKey,
		string presentationKey,
		FlatTranslationDto dto,
		ClaimsPrincipal user, CancellationToken cancellationToken = default)
		=> ValueTask.FromResult(false);

	public ValueTask<bool> CanDeleteAsync(
		string localeKey,
		string textKey,
		string formalityKey,
		string presentationKey,
		ClaimsPrincipal user,
		CancellationToken cancellationToken = default)
		=> ValueTask.FromResult(false);

	private async ValueTask<bool> CanAccessAsync(
		string localeKey,
		string textKey,
		CancellationToken cancellationToken)
	{
		CurrentPrincipal? principal = await _scope.GetPrincipalAsync(cancellationToken);

		return principal is not null &&
			AuthorizationScope.HasRole(principal, Roles.Consumer) &&
			await _scope.CanAccessTranslationAsync(principal, localeKey, textKey, cancellationToken);
	}

	private async ValueTask<bool> CanAccessListAsync(
		FlatTranslationFilter filter,
		CancellationToken cancellationToken)
	{
		CurrentPrincipal? principal = await _scope.GetPrincipalAsync(cancellationToken);
		if (principal is null ||
			!AuthorizationScope.HasRole(principal, Roles.Consumer))
			return false;

		if (!principal.HasApplicationRestrictions &&
			!principal.HasLocaleRestrictions)
			return true;

		return AuthorizationScope.TryGetExactValue(filter.LocaleKey, out string localeKey) &&
			AuthorizationScope.TryGetExactValue(filter.TextKey, out string textKey) &&
			await _scope.CanAccessTranslationAsync(principal, localeKey, textKey, cancellationToken);
	}
}
