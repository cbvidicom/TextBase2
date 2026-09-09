using System.Security.Claims;
using Textbase.Application.Features.Presentations;
using Textbase.Contracts.Models;
using Textbase.Domain.Models;
using Textbase.Host.Api.Authorization;
using Textbase.Host.Authorization;
using Textbase.Host.Enumerations;
using Textbase.Host.ViewModels.Common;
using Textbase.Infrastructure.Persistence.Presentations;
using Uwn.Blazor.Models.Common;

namespace Textbase.Host.ViewModels.Presentations;

public class PresentationEditorViewModel(
	IPresentationAuthorization _authorization,
	ICurrentPrincipalAccessor _currentPrincipalAccessor,
	IPresentationServerQueries presentationQueries,
	IPresentationCommands presentationCommands,
	IPresentationEntityFactory _presentationEntityFactory)
	: TextbaseStringEditorViewModel<PresentationDto, Presentation>(
		presentationQueries,
		presentationCommands,
		presentationCommands)
{
	private readonly Type ModelType = typeof(Presentation);

	protected override string KeyPropertyName => nameof(Presentation.PresentationKey);

	//
	protected override async Task<ViewAuthorizationResult> AuthorizeCreateAsync(
		CancellationToken cancellationToken = default)
	{
		Presentation presentation = _presentationEntityFactory.Create(String.Empty);
		ClaimsPrincipal user = await _currentPrincipalAccessor.GetUserAsync();
		bool isAuthorized = await _authorization.CanCreateAsync(presentation, user, cancellationToken);

		return isAuthorized
			? ViewAuthorizationResult.Authorized
			: ViewAuthorizationResult.Denied(StaticTexts.GetPrincipalNotAuthorizedText(OpType.Create, ModelType));
	}

	protected override async Task<ViewAuthorizationResult> AuthorizeReadAsync(
		object key,
		CancellationToken cancellationToken = default)
	{
		if (key is not string presentationKey)
			return ViewAuthorizationResult.Denied();

		ClaimsPrincipal user = await _currentPrincipalAccessor.GetUserAsync();
		bool isAuthorized = await _authorization.CanReadAsync(presentationKey, user, cancellationToken);

		return isAuthorized
			? ViewAuthorizationResult.Authorized
			: ViewAuthorizationResult.Denied(StaticTexts.GetPrincipalNotAuthorizedText(OpType.Read, ModelType));
	}

	protected override async Task<ViewAuthorizationResult> AuthorizeUpdateAsync(
		Presentation item,
		CancellationToken cancellationToken = default)
	{
		ClaimsPrincipal user = await _currentPrincipalAccessor.GetUserAsync();
		bool isAuthorized = await _authorization.CanUpdateAsync(item.PresentationKey, item, user, cancellationToken);

		return isAuthorized
			? ViewAuthorizationResult.Authorized
			: ViewAuthorizationResult.Denied(StaticTexts.GetPrincipalNotAuthorizedText(OpType.Update, ModelType));
	}

	protected override async Task<ViewAuthorizationResult> AuthorizeDeleteAsync(
		Presentation item,
		CancellationToken cancellationToken = default)
	{
		ClaimsPrincipal user = await _currentPrincipalAccessor.GetUserAsync();
		bool isAuthorized = await _authorization.CanDeleteAsync(item.PresentationKey, user, cancellationToken);
		if (!isAuthorized)
			return ViewAuthorizationResult.Denied(StaticTexts.GetPrincipalNotAuthorizedText(OpType.Delete, ModelType));

		int referenceCount = await presentationQueries.GetReferenceCountAsync(item.PresentationKey, cancellationToken);
		return referenceCount == 0
			? ViewAuthorizationResult.Authorized
			: ViewAuthorizationResult.Denied($"Presentation '{item.PresentationKey}' is referenced by {referenceCount} translation(s) and cannot be deleted.");
	}

	protected override Task<Presentation> CreateNewItemAsync(
		CancellationToken cancellationToken = default)
	{
		Presentation presentation = _presentationEntityFactory.Create(String.Empty);
		return Task.FromResult(presentation);
	}
}
