using Radzen;
using System.Security.Claims;
using Textbase.Application.Features.Formalities;
using Textbase.Domain.Models;
using Textbase.Host.Api.Authorization;
using Textbase.Host.Authorization;
using Textbase.Host.Enumerations;
using Textbase.Infrastructure.Persistence.Formalities;
using Uwn.Blazor.Models.Common;
using Uwn.Blazor.Models.ViewModels.Abstractions.Querying;

namespace Textbase.Host.ViewModels.Formalities;

public class FormalityListViewModel(
	IFormalityAuthorization _authorization,
	ICurrentPrincipalAccessor _currentPrincipalAccessor,
	IFormalityServerQueries formalityQueries,
	IFormalityEntityFactory _formalityEntityFactory)
	: DataGridViewModel<Formality, FormalityFilter>(formalityQueries)
{
	private readonly Type ModelType = typeof(Formality);

	private IReadOnlyDictionary<string, int>? _referenceCounts;

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
		CancellationToken cancellationToken = default)
	{
		ClaimsPrincipal user = await _currentPrincipalAccessor.GetUserAsync();
		FormalityFilter filter = FormalityFilter.All();
		bool isAuthorized = await _authorization.CanListAsync(filter, user, cancellationToken);

		return isAuthorized
			? ViewAuthorizationResult.Authorized
			: ViewAuthorizationResult.Denied(StaticTexts.GetPrincipalNotAuthorizedText(OpType.Read, ModelType));
	}

	protected override async Task<ViewAuthorizationResult> AuthorizeListAsync(
		FormalityFilter filter,
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
		foreach (string key in Data.Select(f => f.FormalityKey))
			referenceCounts[key] = await formalityQueries.GetReferenceCountAsync(key);

		_referenceCounts = referenceCounts;
	}

	public int GetReferenceCount(
		string formalityKey)
	{
		if (_referenceCounts is null)
			return 0;

		return _referenceCounts.TryGetValue(formalityKey, out int value)
			? value
			: 0;
	}
}
