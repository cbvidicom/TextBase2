using System.Security.Claims;
using Textbase.Application.Features.Translations;
using Textbase.Contracts.Models;
using Textbase.Domain.Enumerations;
using Textbase.Host.Authorization;
using Uwn.EntityFrameworkCore.Querying;

namespace Textbase.Host.Api.Authorization;

public sealed class TranslationAuthorization(
	AuthorizationScope scope)
	: AuthorizationBase(scope)
	, ITranslationAuthorization
{
	public async ValueTask<bool> CanCreateAsync(
		TranslationDto dto,
		ClaimsPrincipal user,
		CancellationToken cancellationToken = default)
		=> await CanAccessAsync(dto.LocaleKey, dto.TextKey, cancellationToken);

	public async ValueTask<bool> CanReadAsync(
		string localeKey,
		string textKey,
		string formalityKey,
		string presentationKey, ClaimsPrincipal user,
		CancellationToken cancellationToken = default)
		=> await CanAccessAsync(localeKey, textKey, cancellationToken);

	public async ValueTask<bool> CanCountAsync(
		TranslationFilter filter,
		ClaimsPrincipal user,
		CancellationToken cancellationToken = default)
		=> await CanAccessListAsync(filter, cancellationToken);

	public async ValueTask<bool> CanListAsync(
		TranslationFilter filter,
		ClaimsPrincipal user,
		CancellationToken cancellationToken = default)
		=> await CanAccessListAsync(filter, cancellationToken);

	public async ValueTask<bool> CanUpdateAsync(
		string localeKey,
		string textKey,
		string formalityKey,
		string presentationKey,
		TranslationDto dto,
		ClaimsPrincipal user, CancellationToken cancellationToken = default)
		=> await CanAccessAsync(localeKey, textKey, cancellationToken) && await CanAccessAsync(dto.LocaleKey, dto.TextKey, cancellationToken);

	public async ValueTask<bool> CanDeleteAsync(
		string localeKey,
		string textKey,
		string formalityKey,
		string presentationKey,
		ClaimsPrincipal user,
		CancellationToken cancellationToken = default)
		=> await CanAccessAsync(localeKey, textKey, cancellationToken);

	private async ValueTask<bool> CanAccessAsync(
		string localeKey,
		string textKey,
		CancellationToken cancellationToken)
	{
		CurrentPrincipal? principal = await _scope.GetPrincipalAsync(cancellationToken);
		return principal is not null &&
			AuthorizationScope.HasRole(principal, Roles.Translator) &&
			await _scope.CanAccessTranslationAsync(principal, localeKey, textKey, cancellationToken);
	}

	private async ValueTask<bool> CanAccessListAsync(
		TranslationFilter filter,
		CancellationToken cancellationToken)
	{
		CurrentPrincipal? principal = await _scope.GetPrincipalAsync(cancellationToken);
		if (principal is null ||
			!AuthorizationScope.HasRole(principal, Roles.Translator))
			return false;

		if (!principal.HasApplicationRestrictions &&
			!principal.HasLocaleRestrictions)
			return true;

		StringFilter? restrictedLocaleFilter = filter.LocaleKey;
		if (principal.HasLocaleRestrictions &&
			!AuthorizationScope.TryRestrictStrings(filter.LocaleKey, principal.LocaleKeys, out restrictedLocaleFilter))
			return false;

		IReadOnlyCollection<string> permittedTextKeys = await _scope.GetPermittedTextKeysAsync(principal, cancellationToken);
		if (!AuthorizationScope.TryRestrictStrings(filter.TextKey, permittedTextKeys, out StringFilter? restrictedTextFilter))
			return false;

		if (principal.HasLocaleRestrictions)
			filter.LocaleKey = restrictedLocaleFilter;

		filter.TextKey = restrictedTextFilter;

		return true;
	}
}
