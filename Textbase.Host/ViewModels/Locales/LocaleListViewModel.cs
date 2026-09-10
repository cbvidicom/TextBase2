using System.Security.Claims;
using Textbase.Application.Features.Locales;
using Textbase.Domain.Models;
using Textbase.Host.Api.Authorization;
using Textbase.Host.Authorization;
using Textbase.Host.Enumerations;
using Uwn.Blazor.Models.Common;
using Uwn.Blazor.Models.ViewModels.Abstractions.Querying;

namespace Textbase.Host.ViewModels.Locales;

public class LocaleListViewModel(
	ILocaleAuthorization _authorization,
	ICurrentPrincipalAccessor _currentPrincipalAccessor,
	ILocaleQueries localeQueries)
	: DataGridViewModel<Locale, LocaleFilter>(localeQueries)
{
	private readonly Type ModelType = typeof(Locale);

	protected override Task<ViewAuthorizationResult> AuthorizeCreateAsync(
		CancellationToken cancellationToken = default)
		=> Task.FromResult(ViewAuthorizationResult.Denied());

	protected override async Task<ViewAuthorizationResult> AuthorizeReadAsync(
		CancellationToken cancellationToken = default)
	{
		ClaimsPrincipal user = await _currentPrincipalAccessor.GetUserAsync();
		LocaleFilter filter = LocaleFilter.All();
		bool isAuthorized = await _authorization.CanListAsync(filter, user, cancellationToken);

		return isAuthorized
			? ViewAuthorizationResult.Authorized
			: ViewAuthorizationResult.Denied(StaticTexts.GetPrincipalNotAuthorizedText(OpType.Read, ModelType));
	}

	protected override async Task<ViewAuthorizationResult> AuthorizeListAsync(
		LocaleFilter filter,
		CancellationToken cancellationToken = default)
	{
		ClaimsPrincipal user = await _currentPrincipalAccessor.GetUserAsync();
		bool canCount = await _authorization.CanCountAsync(filter, user, cancellationToken);
		bool canList = canCount && await _authorization.CanListAsync(filter, user, cancellationToken);

		return canList
			? ViewAuthorizationResult.Authorized
			: ViewAuthorizationResult.Denied(StaticTexts.GetPrincipalNotAuthorizedText(OpType.List, ModelType));
	}
}
