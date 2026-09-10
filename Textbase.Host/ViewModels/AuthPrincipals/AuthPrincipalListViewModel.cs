using Radzen;
using System.Security.Claims;
using Textbase.Application.Features.AuthPrincipals;
using Textbase.Domain.Models;
using Textbase.Host.Api.Authorization;
using Textbase.Host.Authorization;
using Textbase.Host.Enumerations;
using Uwn.Blazor.Enumerations.Radzen;
using Uwn.Blazor.Models.Common;
using Uwn.Blazor.Models.ViewModels.Abstractions.Querying;

namespace Textbase.Host.ViewModels.AuthPrincipals;

public class AuthPrincipalListViewModel(
	IAuthPrincipalAuthorization _authorization,
	ICurrentPrincipalAccessor _currentPrincipalAccessor,
	IAuthPrincipalQueries authPrincipalQueries,
	IAuthPrincipalServerQueries _authPrincipalServerQueries)
	: DataGridViewModel<AuthPrincipal, AuthPrincipalFilter>(authPrincipalQueries)
{
	private readonly Type ModelType = typeof(AuthPrincipal);

	private IReadOnlyDictionary<Guid, AuthPrincipalReferenceCounts>? _referenceCounts;

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

	protected override async Task AfterLoadDataAsync(
		LoadDataArgs args)
	{
		_referenceCounts = Data is null
			? null
			: await _authPrincipalServerQueries.GetReferenceCountsAsync([.. Data.Select(principal => principal.EntraObjectId)]);
	}

	public override DataGridRowStyle GetItemStyle(
		AuthPrincipal item)
		=> item.IsActive ? DataGridRowStyle.Base : DataGridRowStyle.Danger;

	public int GetClientApplicationCount(
		Guid entraObjectId)
	{
		if (_referenceCounts is null)
			return 0;

		return _referenceCounts.TryGetValue(entraObjectId, out AuthPrincipalReferenceCounts? value)
			? value.ClientApplicationCount
			: 0;
	}

	public int GetLocaleCount(
		Guid entraObjectId)
	{
		if (_referenceCounts is null)
			return 0;

		return _referenceCounts.TryGetValue(entraObjectId, out AuthPrincipalReferenceCounts? value)
			? value.LocaleCount
			: 0;
	}
}
