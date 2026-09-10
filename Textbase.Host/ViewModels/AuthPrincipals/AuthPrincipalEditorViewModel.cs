using System.Security.Claims;
using Textbase.Application.Features.AuthPrincipalClientApplications;
using Textbase.Application.Features.AuthPrincipalLocales;
using Textbase.Application.Features.AuthPrincipals;
using Textbase.Application.Features.ClientApplications;
using Textbase.Application.Features.Locales;
using Textbase.Contracts.Models;
using Textbase.Domain.Models;
using Textbase.Host.Api.Authorization;
using Textbase.Host.Authorization;
using Textbase.Host.Enumerations;
using Textbase.Infrastructure.Persistence.AuthPrincipalClientApplications;
using Textbase.Infrastructure.Persistence.AuthPrincipalLocales;
using Uwn.Blazor.Models.Common;
using Uwn.Blazor.Models.ViewModels.Abstractions.Querying;

namespace Textbase.Host.ViewModels.AuthPrincipals;

public class AuthPrincipalEditorViewModel(
	IAuthPrincipalAuthorization _authorization,
	ICurrentPrincipalAccessor _currentPrincipalAccessor,
	IAuthPrincipalQueries authPrincipalQueries,
	IAuthPrincipalCommands authPrincipalCommands,
	IAuthPrincipalClientApplicationQueries _authPrincipalClientApplicationQueries,
	IAuthPrincipalClientApplicationCommands _authPrincipalClientApplicationCommands,
	IAuthPrincipalClientApplicationEntityFactory _authPrincipalClientApplicationEntityFactory,
	IAuthPrincipalLocaleQueries _authPrincipalLocaleQueries,
	IAuthPrincipalLocaleCommands _authPrincipalLocaleCommands,
	IAuthPrincipalLocaleEntityFactory _authPrincipalLocaleEntityFactory,
	IClientApplicationQueries _clientApplicationQueries,
	ILocaleQueries _localeQueries)
	: GuidEditorViewModel<AuthPrincipalDto, AuthPrincipal>(
		authPrincipalQueries,
		authPrincipalCommands,
		authPrincipalCommands)
{
	private readonly Type ModelType = typeof(AuthPrincipal);

	public IReadOnlyList<ClientApplication> ClientApplications { get; private set; } = [];
	public IReadOnlyList<AuthPrincipalClientApplication> PrincipalClientApplications { get; private set; } = [];
	public IReadOnlyList<Locale> Locales { get; private set; } = [];
	public IReadOnlyList<AuthPrincipalLocale> PrincipalLocales { get; private set; } = [];

	public Task SetSelectedTabIndexAsync(
		int index)
		=> index switch
		{
			1 => LoadClientApplicationsAsync(),
			2 => LoadLocalesAsync(),
			_ => Task.CompletedTask
		};

	public async Task AddClientApplicationAsync(
		Guid clientApplicationGuid)
	{
		if (PrincipalClientApplications.Any(item => item.ClientApplicationGuid == clientApplicationGuid))
		{
			return;
		}

		AuthPrincipalClientApplication item = _authPrincipalClientApplicationEntityFactory.Create(Item.EntraObjectId, clientApplicationGuid);
		if (await _authPrincipalClientApplicationCommands.TryCreateAsync(item))
		{
			await LoadClientApplicationsAsync();
		}
	}

	public async Task RemoveClientApplicationAsync(
		Guid clientApplicationGuid)
	{
		bool deleted = await _authPrincipalClientApplicationCommands.TryDeleteAsync(Item.EntraObjectId, clientApplicationGuid);
		if (deleted)
		{
			PrincipalClientApplications = [.. PrincipalClientApplications.Where(item => item.ClientApplicationGuid != clientApplicationGuid)];
		}
	}

	public async Task AddLocaleAsync(
		string localeKey)
	{
		if (PrincipalLocales.Any(item => item.LocaleKey == localeKey))
		{
			return;
		}

		AuthPrincipalLocale item = _authPrincipalLocaleEntityFactory.Create(Item.EntraObjectId, localeKey);
		if (await _authPrincipalLocaleCommands.TryCreateAsync(item))
		{
			await LoadLocalesAsync();
		}
	}

	public async Task RemoveLocaleAsync(
		string localeKey)
	{
		bool deleted = await _authPrincipalLocaleCommands.TryDeleteAsync(Item.EntraObjectId, localeKey);
		if (deleted)
		{
			PrincipalLocales = [.. PrincipalLocales.Where(item => item.LocaleKey != localeKey)];
		}
	}

	protected override async Task<ViewAuthorizationResult> AuthorizeReadAsync(
		object key,
		CancellationToken cancellationToken = default)
	{
		if (key is not Guid entraObjectId)
		{
			return ViewAuthorizationResult.Denied();
		}

		ClaimsPrincipal user = await _currentPrincipalAccessor.GetUserAsync();
		bool isAuthorized = await _authorization.CanReadAsync(entraObjectId, user, cancellationToken);

		return isAuthorized
			? ViewAuthorizationResult.Authorized
			: ViewAuthorizationResult.Denied(StaticTexts.GetPrincipalNotAuthorizedText(OpType.Read, ModelType));
	}

	protected override async Task<ViewAuthorizationResult> AuthorizeUpdateAsync(
		AuthPrincipal item,
		CancellationToken cancellationToken = default)
	{
		ClaimsPrincipal user = await _currentPrincipalAccessor.GetUserAsync();
		bool isAuthorized = await _authorization.CanUpdateAsync(item.EntraObjectId, item, user, cancellationToken);

		return isAuthorized
			? ViewAuthorizationResult.Authorized
			: ViewAuthorizationResult.Denied(StaticTexts.GetPrincipalNotAuthorizedText(OpType.Update, ModelType));
	}

	protected override async Task<ViewAuthorizationResult> AuthorizeDeleteAsync(
		AuthPrincipal item,
		CancellationToken cancellationToken = default)
	{
		ClaimsPrincipal user = await _currentPrincipalAccessor.GetUserAsync();
		bool isAuthorized = await _authorization.CanDeleteAsync(item.EntraObjectId, user, cancellationToken);

		return isAuthorized
			? ViewAuthorizationResult.Authorized
			: ViewAuthorizationResult.Denied(StaticTexts.GetPrincipalNotAuthorizedText(OpType.Delete, ModelType));
	}

	private async Task LoadClientApplicationsAsync()
	{
		AuthPrincipalClientApplicationFilter filter = AuthPrincipalClientApplicationFilter.All();
		filter.EntraObjectId = Item.EntraObjectId;
		PrincipalClientApplications = await _authPrincipalClientApplicationQueries.ListItemsAsync(filter);

		if (!ClientApplications.Any())
		{
			ClientApplications = await _clientApplicationQueries.ListAllItemsAsync();
		}
	}

	private async Task LoadLocalesAsync()
	{
		AuthPrincipalLocaleFilter filter = AuthPrincipalLocaleFilter.All();
		filter.EntraObjectId = Item.EntraObjectId;
		PrincipalLocales = await _authPrincipalLocaleQueries.ListItemsAsync(filter);

		if (!Locales.Any())
		{
			Locales = await _localeQueries.ListAllItemsAsync();
		}
	}
}
