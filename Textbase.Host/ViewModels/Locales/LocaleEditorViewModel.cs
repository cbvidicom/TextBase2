using System.Security.Claims;
using Textbase.Application.Features.Locales;
using Textbase.Contracts.Models;
using Textbase.Domain.Models;
using Textbase.Host.Api.Authorization;
using Textbase.Host.Authorization;
using Textbase.Host.Enumerations;
using Textbase.Infrastructure.Persistence.Locales;
using Uwn.Blazor.Models.Common;
using Uwn.Blazor.Models.ViewModels.Abstractions.Querying;

namespace Textbase.Host.ViewModels.Locales;

public class LocaleEditorViewModel(
	ILocaleAuthorization _authorization,
	ICurrentPrincipalAccessor _currentPrincipalAccessor,
	ILocaleQueries localeQueries,
	ILocaleCommands localeCommands,
	ILocaleEntityFactory _localeEntityFactory)
	: StringEditorViewModel<LocaleDto, Locale>(
		localeQueries,
		localeCommands,
		localeCommands)
{
	private readonly Type ModelType = typeof(Locale);

	protected override string KeyPropertyName => nameof(Locale.LocaleKey);

	protected override Task<ViewAuthorizationResult> AuthorizeCreateAsync(
		CancellationToken cancellationToken = default)
		=> Task.FromResult(ViewAuthorizationResult.Denied());

	protected override async Task<ViewAuthorizationResult> AuthorizeReadAsync(
		object key,
		CancellationToken cancellationToken = default)
	{
		if (key is not string localeKey)
		{
			return ViewAuthorizationResult.Denied();
		}

		ClaimsPrincipal user = await _currentPrincipalAccessor.GetUserAsync();
		bool isAuthorized = await _authorization.CanReadAsync(localeKey, user, cancellationToken);

		return isAuthorized
			? ViewAuthorizationResult.Authorized
			: ViewAuthorizationResult.Denied(StaticTexts.GetPrincipalNotAuthorizedText(OpType.Read, ModelType));
	}

	protected override Task<ViewAuthorizationResult> AuthorizeUpdateAsync(
		Locale item,
		CancellationToken cancellationToken = default)
		=> Task.FromResult(ViewAuthorizationResult.Denied());

	protected override Task<ViewAuthorizationResult> AuthorizeDeleteAsync(
		Locale item,
		CancellationToken cancellationToken = default)
		=> Task.FromResult(ViewAuthorizationResult.Denied());

	protected override Task<Locale> CreateNewItemAsync(
		CancellationToken cancellationToken = default)
	{
		Locale locale = _localeEntityFactory.Create(String.Empty);
		return Task.FromResult(locale);
	}
}
