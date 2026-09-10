using System.Security.Claims;
using Textbase.Application.Features.TextResources;
using Textbase.Contracts.Models;
using Textbase.Domain.Models;
using Textbase.Host.Api.Authorization;
using Textbase.Host.Authorization;
using Textbase.Host.Enumerations;
using Textbase.Infrastructure.Persistence.TextResources;
using Uwn.Blazor.Models.Common;
using Uwn.Blazor.Models.ViewModels.Abstractions.Querying;

namespace Textbase.Host.ViewModels.TextResources;

public class TextResourceEditorViewModel(
	ITextResourceAuthorization _authorization,
	ICurrentPrincipalAccessor _currentPrincipalAccessor,
	ITextResourceQueries textResourceQueries,
	ITextResourceCommands textResourceCommands,
	ITextResourceEntityFactory _textResourceEntityFactory)
	: StringEditorViewModel<TextResourceDto, TextResource>(
		textResourceQueries,
		textResourceCommands,
		textResourceCommands)
{
	private readonly Type ModelType = typeof(TextResource);

	protected override string KeyPropertyName => nameof(TextResource.TextKey);

	protected override async Task<ViewAuthorizationResult> AuthorizeCreateAsync(
		CancellationToken cancellationToken = default)
	{
		TextResource textResource = _textResourceEntityFactory.Create(String.Empty);
		ClaimsPrincipal user = await _currentPrincipalAccessor.GetUserAsync();
		bool isAuthorized = await _authorization.CanCreateAsync(textResource, user, cancellationToken);

		return isAuthorized
			? ViewAuthorizationResult.Authorized
			: ViewAuthorizationResult.Denied(StaticTexts.GetPrincipalNotAuthorizedText(OpType.Create, ModelType));
	}

	protected override async Task<ViewAuthorizationResult> AuthorizeReadAsync(
		object key,
		CancellationToken cancellationToken = default)
	{
		if (key is not string textKey)
			return ViewAuthorizationResult.Denied();

		ClaimsPrincipal user = await _currentPrincipalAccessor.GetUserAsync();
		bool isAuthorized = await _authorization.CanReadAsync(textKey, user, cancellationToken);

		return isAuthorized
			? ViewAuthorizationResult.Authorized
			: ViewAuthorizationResult.Denied(StaticTexts.GetPrincipalNotAuthorizedText(OpType.Read, ModelType));
	}

	protected override async Task<ViewAuthorizationResult> AuthorizeUpdateAsync(
		TextResource item,
		CancellationToken cancellationToken = default)
	{
		ClaimsPrincipal user = await _currentPrincipalAccessor.GetUserAsync();
		bool isAuthorized = await _authorization.CanUpdateAsync(item.TextKey, item, user, cancellationToken);

		return isAuthorized
			? ViewAuthorizationResult.Authorized
			: ViewAuthorizationResult.Denied(StaticTexts.GetPrincipalNotAuthorizedText(OpType.Update, ModelType));
	}

	protected override async Task<ViewAuthorizationResult> AuthorizeDeleteAsync(
		TextResource item,
		CancellationToken cancellationToken = default)
	{
		ClaimsPrincipal user = await _currentPrincipalAccessor.GetUserAsync();
		bool isAuthorized = await _authorization.CanDeleteAsync(item.TextKey, user, cancellationToken);

		return isAuthorized
			? ViewAuthorizationResult.Authorized
			: ViewAuthorizationResult.Denied(StaticTexts.GetPrincipalNotAuthorizedText(OpType.Delete, ModelType));
	}

	protected override Task<TextResource> CreateNewItemAsync(
		CancellationToken cancellationToken = default)
	{
		TextResource textResource = _textResourceEntityFactory.Create(String.Empty);
		return Task.FromResult(textResource);
	}
}
