using System.Security.Claims;
using Textbase.Application.Features.AuthPrincipals;
using Textbase.Domain.Enumerations;
using Textbase.Domain.Models;
using Textbase.Host.Api.Authorization;
using Textbase.Host.Authorization;
using Textbase.Host.Enumerations;
using Textbase.Infrastructure.Persistence.AuthPrincipals;
using Uwn.Blazor.Enumerations.Radzen;
using Uwn.Blazor.Models.Common;
using Uwn.Blazor.Models.ViewModels.Abstractions.Querying;

namespace Textbase.Host.ViewModels.AuthPrincipals;

public class AuthPrincipalListViewModel(
	IAuthPrincipalAuthorization _authorization,
	ICurrentPrincipalAccessor _currentPrincipalAccessor,
	IAuthPrincipalQueries authPrincipalQueries,
	IAuthPrincipalEntityFactory _authPrincipalEntityFactory)
	: DataGridViewModel<AuthPrincipal, AuthPrincipalFilter>(authPrincipalQueries)
{
	private readonly Type ModelType = typeof(AuthPrincipal);

	protected override async Task<ViewAuthorizationResult> AuthorizeCreateAsync(
		CancellationToken cancellationToken = default)
	{
		AuthPrincipal principal = _authPrincipalEntityFactory.Create(Guid.Empty, (int)Roles.None, (int)PrincipalStatus.Pending);
		ClaimsPrincipal user = await _currentPrincipalAccessor.GetUserAsync();
		bool isAuthorized = await _authorization.CanCreateAsync(principal, user, cancellationToken);

		return isAuthorized
			? ViewAuthorizationResult.Authorized
			: ViewAuthorizationResult.Denied(StaticTexts.GetPrincipalNotAuthorizedText(OpType.Create, ModelType));
	}

	protected override async Task<ViewAuthorizationResult> AuthorizeReadAsync(
		CancellationToken cancellationToken = default)
	{
		ClaimsPrincipal user = await _currentPrincipalAccessor.GetUserAsync();
		AuthPrincipalFilter filter = AuthPrincipalFilter.All();
		bool isAuthorized = await _authorization.CanListAsync(filter, user, cancellationToken);

		return isAuthorized
			? ViewAuthorizationResult.Authorized
			: ViewAuthorizationResult.Denied(StaticTexts.GetPrincipalNotAuthorizedText(OpType.Read, ModelType));
	}

	protected override async Task<ViewAuthorizationResult> AuthorizeListAsync(
		AuthPrincipalFilter filter,
		CancellationToken cancellationToken = default)
	{
		ClaimsPrincipal user = await _currentPrincipalAccessor.GetUserAsync();
		bool canCount = await _authorization.CanCountAsync(filter, user, cancellationToken);
		bool canList = canCount && await _authorization.CanListAsync(filter, user, cancellationToken);

		return canList
			? ViewAuthorizationResult.Authorized
			: ViewAuthorizationResult.Denied(StaticTexts.GetPrincipalNotAuthorizedText(OpType.List, ModelType));
	}

	public override DataGridRowStyle GetItemStyle(
		AuthPrincipal item)
		=> item.IsActive ? DataGridRowStyle.Base : DataGridRowStyle.Danger;
}
