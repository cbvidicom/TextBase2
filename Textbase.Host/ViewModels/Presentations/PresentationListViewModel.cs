using Radzen;
using System.Security.Claims;
using Textbase.Application.Features.Presentations;
using Textbase.Domain.Models;
using Textbase.Host.Api.Authorization;
using Textbase.Host.Authorization;
using Textbase.Host.Enumerations;
using Textbase.Infrastructure.Persistence.Presentations;
using Uwn.Blazor.Models.Common;
using Uwn.Blazor.Models.ViewModels.Abstractions.Querying;

namespace Textbase.Host.ViewModels.Presentations;

public class PresentationListViewModel(
	IPresentationAuthorization _authorization,
	ICurrentPrincipalAccessor _currentPrincipalAccessor,
	IPresentationServerQueries presentationQueries,
	IPresentationEntityFactory _presentationEntityFactory)
	: DataGridViewModel<Presentation, PresentationFilter>(presentationQueries)
{
	private readonly Type ModelType = typeof(Presentation);

	private IReadOnlyDictionary<string, int>? _referenceCounts;

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
		CancellationToken cancellationToken = default)
	{
		ClaimsPrincipal user = await _currentPrincipalAccessor.GetUserAsync();
		PresentationFilter filter = PresentationFilter.All();
		bool isAuthorized = await _authorization.CanListAsync(filter, user, cancellationToken);

		return isAuthorized
			? ViewAuthorizationResult.Authorized
			: ViewAuthorizationResult.Denied(StaticTexts.GetPrincipalNotAuthorizedText(OpType.Read, ModelType));
	}

	protected override async Task<ViewAuthorizationResult> AuthorizeListAsync(
		PresentationFilter filter,
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
		if (Data is null)
		{
			_referenceCounts = null;
			return;
		}

		Dictionary<string, int> referenceCounts = [];
		foreach (string key in Data.Select(p => p.PresentationKey))
			referenceCounts[key] = await presentationQueries.GetReferenceCountAsync(key);

		_referenceCounts = referenceCounts;
	}

	public int GetReferenceCount(
		string presentationKey)
	{
		if (_referenceCounts is null)
			return 0;

		return _referenceCounts.TryGetValue(presentationKey, out int value)
			? value
			: 0;
	}
}
