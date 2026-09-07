using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using Textbase.Application.Features.ClientApplications;
using Textbase.Domain.Models;
using Textbase.Host.Api.Authorization;
using Uwn.Blazor.Enumerations.Radzen;
using Uwn.Blazor.Models.ViewModels.Abstractions.Querying;

namespace Textbase.Host.ViewModels.ClientApplications;

public class ClientApplicationListViewModel(
	IClientApplicationAuthorization _authorization,
	AuthenticationStateProvider _authenticationStateProvider,
	IClientApplicationQueries clientApplicationQueries,
	IClientApplicationServerQueries _clientApplicationServerQueries)
	: DataGridViewModel<ClientApplication, ClientApplicationFilter>(
		clientApplicationQueries)
{
	private IReadOnlyDictionary<Guid, ClientApplicationReferenceCounts>? _referenceCounts;

	protected override async Task ConfigureFilterAsync(
		ClientApplicationFilter filter)
	{
		await base.ConfigureFilterAsync(filter);

		AuthenticationState authenticationState = await _authenticationStateProvider.GetAuthenticationStateAsync();
		ClaimsPrincipal user = authenticationState.User;
		if (!await _authorization.CanCountAsync(filter, user) || !await _authorization.CanListAsync(filter, user))
			throw new UnauthorizedAccessException("The current principal is not authorized to list client applications.");
	}

	protected override async Task AfterLoadDataAsync()
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
