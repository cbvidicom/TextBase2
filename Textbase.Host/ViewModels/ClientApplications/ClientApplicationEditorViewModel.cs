using System.Security.Claims;
using Textbase.Application.Features.ClientApplications;
using Textbase.Contracts.Models;
using Textbase.Domain.Models;
using Textbase.Host.Api.Authorization;
using Textbase.Host.Authorization;
using Textbase.Host.Enumerations;
using Textbase.Infrastructure.Persistence.ClientApplications;
using Uwn.Blazor.Models.Common;
using Uwn.Blazor.Models.ViewModels.Abstractions.Querying;

namespace Textbase.Host.ViewModels.ClientApplications;

public class ClientApplicationEditorViewModel(
	IClientApplicationAuthorization _authorization,
	ICurrentUserAccessor _currentUserAccessor,
	IClientApplicationQueries clientApplicationQueries,
	IClientApplicationCommands clientApplicationCommands,
	IClientApplicationEntityFactory _clientApplicationEntityFactory)
	: GuidEditorViewModel<ClientApplicationDto, ClientApplication>(
		clientApplicationQueries,
		clientApplicationCommands,
		clientApplicationCommands)
{
	private readonly Type ModelType = typeof(ClientApplication);

	protected override async Task<ViewAuthorizationResult> AuthorizeCreateAsync(
		CancellationToken cancellationToken = default)
	{
		ClientApplication clientApplication = _clientApplicationEntityFactory.Create(Guid.CreateVersion7());
		clientApplication.IsActive = true;
		ClaimsPrincipal user = await _currentUserAccessor.GetAsync();
		bool isAuthorized = await _authorization.CanCreateAsync(clientApplication, user, cancellationToken);

		return isAuthorized
			? ViewAuthorizationResult.Authorized
			: ViewAuthorizationResult.Denied(StaticTexts.GetPrincipalNotAuthorizedText(OpType.Create, ModelType));
	}

	protected override async Task<ViewAuthorizationResult> AuthorizeReadAsync(
		object key,
		CancellationToken cancellationToken = default)
	{
		if (key is not Guid clientApplicationGuid)
			return ViewAuthorizationResult.Denied();

		ClaimsPrincipal user = await _currentUserAccessor.GetAsync();
		bool isAuthorized = await _authorization.CanReadAsync(clientApplicationGuid, user, cancellationToken);

		return isAuthorized
			? ViewAuthorizationResult.Authorized
			: ViewAuthorizationResult.Denied(StaticTexts.GetPrincipalNotAuthorizedText(OpType.Read, ModelType));
	}

	protected override async Task<ViewAuthorizationResult> AuthorizeUpdateAsync(
		ClientApplication item,
		CancellationToken cancellationToken = default)
	{
		ClaimsPrincipal user = await _currentUserAccessor.GetAsync();
		bool isAuthorized = await _authorization.CanUpdateAsync(item.ClientApplicationGuid, item, user, cancellationToken);

		return isAuthorized
			? ViewAuthorizationResult.Authorized
			: ViewAuthorizationResult.Denied(StaticTexts.GetPrincipalNotAuthorizedText(OpType.Update, ModelType));
	}

	protected override async Task<ViewAuthorizationResult> AuthorizeDeleteAsync(
		ClientApplication item,
		CancellationToken cancellationToken = default)
	{
		ClaimsPrincipal user = await _currentUserAccessor.GetAsync();
		bool isAuthorized = await _authorization.CanDeleteAsync(item.ClientApplicationGuid, user, cancellationToken);

		return isAuthorized
			? ViewAuthorizationResult.Authorized
			: ViewAuthorizationResult.Denied(StaticTexts.GetPrincipalNotAuthorizedText(OpType.Delete, ModelType));
	}

	protected override Task<ClientApplication> CreateNewItemAsync(
		CancellationToken cancellationToken = default)
	{
		ClientApplication clientApplication = _clientApplicationEntityFactory.Create(Guid.CreateVersion7());
		clientApplication.IsActive = true;

		return Task.FromResult(clientApplication);
	}
}
