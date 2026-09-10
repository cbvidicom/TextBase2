using Radzen;
using System.Security.Claims;
using Textbase.Application.Features.TextResources;
using Textbase.Domain.Models;
using Textbase.Host.Api.Authorization;
using Textbase.Host.Authorization;
using Textbase.Host.Enumerations;
using Textbase.Infrastructure.Persistence.TextResources;
using Uwn.Blazor.Models.Common;
using Uwn.Blazor.Models.ViewModels.Abstractions.Querying;

namespace Textbase.Host.ViewModels.TextResources;

public class TextResourceListViewModel(
	ITextResourceAuthorization _authorization,
	ICurrentPrincipalAccessor _currentPrincipalAccessor,
	ITextResourceServerQueries textResourceQueries,
	ITextResourceEntityFactory _textResourceEntityFactory)
	: DataGridViewModel<TextResource, TextResourceFilter>(textResourceQueries)
{
	private readonly Type ModelType = typeof(TextResource);

	private IReadOnlyDictionary<string, TextResourceReferenceCounts>? _referenceCounts;

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
		CancellationToken cancellationToken = default)
	{
		ClaimsPrincipal user = await _currentPrincipalAccessor.GetUserAsync();
		TextResourceFilter filter = TextResourceFilter.All();
		bool isAuthorized = await _authorization.CanListAsync(filter, user, cancellationToken);

		return isAuthorized
			? ViewAuthorizationResult.Authorized
			: ViewAuthorizationResult.Denied(StaticTexts.GetPrincipalNotAuthorizedText(OpType.Read, ModelType));
	}

	protected override async Task<ViewAuthorizationResult> AuthorizeListAsync(
		TextResourceFilter filter,
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
			: await textResourceQueries.GetReferenceCountsAsync([.. Data.Select(textResource => textResource.TextKey)]);
	}

	public int GetClientApplicationCount(
		string textKey)
		=> GetReferenceCounts(textKey)?.ClientApplicationCount ?? 0;

	public int GetLocaleCount(
		string textKey)
		=> GetReferenceCounts(textKey)?.LocaleCount ?? 0;

	public int GetTranslationCount(
		string textKey)
		=> GetReferenceCounts(textKey)?.TranslationCount ?? 0;

	private TextResourceReferenceCounts? GetReferenceCounts(
		string textKey)
	{
		if (_referenceCounts is null)
			return null;

		return _referenceCounts.TryGetValue(textKey, out TextResourceReferenceCounts? value)
			? value
			: null;
	}
}
