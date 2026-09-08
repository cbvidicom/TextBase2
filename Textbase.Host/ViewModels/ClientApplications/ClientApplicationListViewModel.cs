using Radzen;
using System.Security.Claims;
using Textbase.Application.Features.ClientApplications;
using Textbase.Domain.Models;
using Textbase.Host.Api.Authorization;
using Textbase.Host.Authorization;
using Textbase.Host.Enumerations;
using Textbase.Infrastructure.Persistence.ClientApplications;
using Uwn.Blazor.Enumerations.Radzen;
using Uwn.Blazor.Models.Common;
using Uwn.Blazor.Models.ViewModels.Abstractions.Querying;

namespace Textbase.Host.ViewModels.ClientApplications;

public class ClientApplicationListViewModel(
	IClientApplicationAuthorization _authorization,
	ICurrentPrincipalAccessor _currentPrincipalAccessor,
	IClientApplicationQueries clientApplicationQueries,
	IClientApplicationServerQueries _clientApplicationServerQueries,
	IClientApplicationEntityFactory _clientApplicationEntityFactory)
	: DataGridViewModel<ClientApplication, ClientApplicationFilter>(
		clientApplicationQueries)
{
	private readonly Type ModelType = typeof(ClientApplication);

	private IReadOnlyDictionary<Guid, ClientApplicationReferenceCounts>? _referenceCounts;

	protected override async Task<ViewAuthorizationResult> AuthorizeCreateAsync(
		CancellationToken cancellationToken = default)
	{
		ClientApplication clientApplication = _clientApplicationEntityFactory.Create(Guid.CreateVersion7());
		clientApplication.IsActive = true;
		ClaimsPrincipal user = await _currentPrincipalAccessor.GetUserAsync();
		bool isAuthorized = await _authorization.CanCreateAsync(clientApplication, user, cancellationToken);

		return isAuthorized
			? ViewAuthorizationResult.Authorized
			: ViewAuthorizationResult.Denied(StaticTexts.GetPrincipalNotAuthorizedText(OpType.Create, ModelType));
	}

	protected override async Task<ViewAuthorizationResult> AuthorizeReadAsync(
		CancellationToken cancellationToken = default)
	{
		ClaimsPrincipal user = await _currentPrincipalAccessor.GetUserAsync();
		ClientApplicationFilter filter = ClientApplicationFilter.All();
		bool isAuthorized = await _authorization.CanListAsync(filter, user, cancellationToken);

		return isAuthorized
			? ViewAuthorizationResult.Authorized
			: ViewAuthorizationResult.Denied(StaticTexts.GetPrincipalNotAuthorizedText(OpType.Read, ModelType));
	}

	protected override async Task<ViewAuthorizationResult> AuthorizeListAsync(
		ClientApplicationFilter filter,
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
			: await _clientApplicationServerQueries.GetReferenceCountsAsync([.. Data.Select(clientApplication => clientApplication.ClientApplicationGuid)]);
	}

	public override DataGridRowStyle GetItemStyle(
		ClientApplication item)
		=> item.IsActive ? DataGridRowStyle.Base : DataGridRowStyle.Danger;

	public int GetLocaleCount(
		Guid clientApplicationGuid)
	{
		if (_referenceCounts is null)
			return 0;

		return _referenceCounts.TryGetValue(clientApplicationGuid, out ClientApplicationReferenceCounts? value)
			? value.LocaleCount
			: 0;
	}

	public int GetTextResourceCount(
		Guid clientApplicationGuid)
	{
		if (_referenceCounts is null)
			return 0;

		return _referenceCounts.TryGetValue(clientApplicationGuid, out ClientApplicationReferenceCounts? value)
			? value.TextResourceCount
			: 0;
	}
}
