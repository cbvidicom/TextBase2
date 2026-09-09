using System.Security.Claims;
using Textbase.Application.Features.Formalities;
using Textbase.Contracts.Models;
using Textbase.Domain.Models;
using Textbase.Host.Api.Authorization;
using Textbase.Host.Authorization;
using Textbase.Host.Enumerations;
using Textbase.Host.ViewModels.Common;
using Textbase.Infrastructure.Persistence.Formalities;
using Uwn.Blazor.Models.Common;

namespace Textbase.Host.ViewModels.Formalities;

public class FormalityEditorViewModel(
	IFormalityAuthorization _authorization,
	ICurrentPrincipalAccessor _currentPrincipalAccessor,
	IFormalityServerQueries formalityQueries,
	IFormalityCommands formalityCommands,
	IFormalityEntityFactory _formalityEntityFactory)
	: TextbaseStringEditorViewModel<FormalityDto, Formality>(
		formalityQueries,
		formalityCommands,
		formalityCommands)
{
	private readonly Type ModelType = typeof(Formality);

	protected override string KeyPropertyName => nameof(Formality.FormalityKey);

	protected override async Task<ViewAuthorizationResult> AuthorizeCreateAsync(
		CancellationToken cancellationToken = default)
	{
		Formality formality = _formalityEntityFactory.Create(String.Empty);
		ClaimsPrincipal user = await _currentPrincipalAccessor.GetUserAsync();
		bool isAuthorized = await _authorization.CanCreateAsync(formality, user, cancellationToken);

		return isAuthorized
			? ViewAuthorizationResult.Authorized
			: ViewAuthorizationResult.Denied(StaticTexts.GetPrincipalNotAuthorizedText(OpType.Create, ModelType));
	}

	protected override async Task<ViewAuthorizationResult> AuthorizeReadAsync(
		object key,
		CancellationToken cancellationToken = default)
	{
		if (key is not string formalityKey)
			return ViewAuthorizationResult.Denied();

		ClaimsPrincipal user = await _currentPrincipalAccessor.GetUserAsync();
		bool isAuthorized = await _authorization.CanReadAsync(formalityKey, user, cancellationToken);

		return isAuthorized
			? ViewAuthorizationResult.Authorized
			: ViewAuthorizationResult.Denied(StaticTexts.GetPrincipalNotAuthorizedText(OpType.Read, ModelType));
	}

	protected override async Task<ViewAuthorizationResult> AuthorizeUpdateAsync(
		Formality item,
		CancellationToken cancellationToken = default)
	{
		ClaimsPrincipal user = await _currentPrincipalAccessor.GetUserAsync();
		bool isAuthorized = await _authorization.CanUpdateAsync(item.FormalityKey, item, user, cancellationToken);

		return isAuthorized
			? ViewAuthorizationResult.Authorized
			: ViewAuthorizationResult.Denied(StaticTexts.GetPrincipalNotAuthorizedText(OpType.Update, ModelType));
	}

	protected override async Task<ViewAuthorizationResult> AuthorizeDeleteAsync(
		Formality item,
		CancellationToken cancellationToken = default)
	{
		ClaimsPrincipal user = await _currentPrincipalAccessor.GetUserAsync();
		bool isAuthorized = await _authorization.CanDeleteAsync(item.FormalityKey, user, cancellationToken);
		if (!isAuthorized)
			return ViewAuthorizationResult.Denied(StaticTexts.GetPrincipalNotAuthorizedText(OpType.Delete, ModelType));

		int referenceCount = await formalityQueries.GetReferenceCountAsync(item.FormalityKey, cancellationToken);
		return referenceCount == 0
			? ViewAuthorizationResult.Authorized
			: ViewAuthorizationResult.Denied($"Formality '{item.FormalityKey}' is referenced by {referenceCount} translation(s) and cannot be deleted.");
	}

	protected override Task<Formality> CreateNewItemAsync(
		CancellationToken cancellationToken = default)
	{
		Formality formality = _formalityEntityFactory.Create(String.Empty);
		return Task.FromResult(formality);
	}
}
