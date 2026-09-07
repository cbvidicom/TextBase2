using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using Textbase.Application.Features.ClientApplications;
using Textbase.Contracts.Models;
using Textbase.Domain.Models;
using Textbase.Host.Api.Authorization;
using Textbase.Infrastructure.Persistence.ClientApplications;
using Uwn.Blazor.Models.ViewModels.Abstractions.Querying;
using Uwn.Common.Conversion;

namespace Textbase.Host.ViewModels.ClientApplications;

public class ClientApplicationEditorViewModel(
	IClientApplicationAuthorization _authorization,
	AuthenticationStateProvider _authenticationStateProvider,
	IClientApplicationQueries clientApplicationQueries,
	IClientApplicationCommands clientApplicationCommands,
	IClientApplicationEntityFactory _clientApplicationEntityFactory)
	: GuidEditorViewModel<ClientApplicationDto, ClientApplication>(
		clientApplicationQueries,
		clientApplicationCommands,
		clientApplicationCommands)
{
	public bool CanWrite { get; private set; }

	protected override async Task<ClientApplication> CreateNewItemAsync(
		CancellationToken cancellationToken = default)
	{
		ClientApplication clientApplication = _clientApplicationEntityFactory.Create(Guid.CreateVersion7());
		clientApplication.IsActive = true;

		ClientApplicationDto dto = ObjectMapper.MapTo<ClientApplicationDto>(clientApplication);
		ClaimsPrincipal user = await GetUserAsync();
		CanWrite = await _authorization.CanCreateAsync(dto, user, cancellationToken);
		if (!CanWrite)
			throw new UnauthorizedAccessException("The current principal is not authorized to create client applications.");

		return clientApplication;
	}

	protected override async Task<ClientApplication> ReadItemAsync(
		object key,
		CancellationToken cancellationToken)
	{
		Guid clientApplicationGuid = (Guid)key;
		ClaimsPrincipal user = await GetUserAsync();
		if (!await _authorization.CanReadAsync(clientApplicationGuid, user, cancellationToken))
			throw new UnauthorizedAccessException("The current principal is not authorized to read this client application.");

		ClientApplication clientApplication = await base.ReadItemAsync(key, cancellationToken);
		ClientApplicationDto dto = ObjectMapper.MapTo<ClientApplicationDto>(clientApplication);
		CanWrite = await _authorization.CanUpdateAsync(clientApplicationGuid, dto, user, cancellationToken);

		return clientApplication;
	}

	protected override async Task BeforeCreateCommandAsync(
		CancellationToken cancellationToken)
	{
		await base.BeforeCreateCommandAsync(cancellationToken);

		ClientApplicationDto dto = ObjectMapper.MapTo<ClientApplicationDto>(Item);
		ClaimsPrincipal user = await GetUserAsync();
		if (!await _authorization.CanCreateAsync(dto, user, cancellationToken))
			throw new UnauthorizedAccessException("The current principal is not authorized to create client applications.");
	}

	protected override async Task BeforeUpdateCommandAsync(
		CancellationToken cancellationToken)
	{
		await base.BeforeUpdateCommandAsync(cancellationToken);

		ClientApplicationDto dto = ObjectMapper.MapTo<ClientApplicationDto>(Item);
		ClaimsPrincipal user = await GetUserAsync();
		if (!await _authorization.CanUpdateAsync(Item.ClientApplicationGuid, dto, user, cancellationToken))
			throw new UnauthorizedAccessException("The current principal is not authorized to update this client application.");
	}

	protected override async Task BeforeDeleteCommandAsync(
		CancellationToken cancellationToken)
	{
		await base.BeforeDeleteCommandAsync(cancellationToken);

		ClaimsPrincipal user = await GetUserAsync();
		if (!await _authorization.CanDeleteAsync(Item.ClientApplicationGuid, user, cancellationToken))
			throw new UnauthorizedAccessException("The current principal is not authorized to delete this client application.");
	}

	private async Task<ClaimsPrincipal> GetUserAsync()
		=> (await _authenticationStateProvider.GetAuthenticationStateAsync()).User;
}
