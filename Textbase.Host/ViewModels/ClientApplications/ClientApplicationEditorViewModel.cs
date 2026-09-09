using System.Security.Claims;
using Textbase.Application.Features.ClientApplicationLocales;
using Textbase.Application.Features.ClientApplications;
using Textbase.Application.Features.ClientApplicationTextResources;
using Textbase.Application.Features.Locales;
using Textbase.Application.Features.TextResources;
using Textbase.Contracts.Models;
using Textbase.Domain.Models;
using Textbase.Host.Api.Authorization;
using Textbase.Host.Authorization;
using Textbase.Host.Enumerations;
using Textbase.Infrastructure.Persistence.ClientApplicationLocales;
using Textbase.Infrastructure.Persistence.ClientApplications;
using Textbase.Infrastructure.Persistence.ClientApplicationTextResources;
using Uwn.Blazor.Models.Common;
using Uwn.Blazor.Models.ViewModels.Abstractions.Querying;

namespace Textbase.Host.ViewModels.ClientApplications;

public class ClientApplicationEditorViewModel(
	IClientApplicationAuthorization _authorization,
	ICurrentPrincipalAccessor _currentPrincipalAccessor,
	IClientApplicationQueries clientApplicationQueries,
	IClientApplicationCommands clientApplicationCommands,
	IClientApplicationEntityFactory _clientApplicationEntityFactory,
	IClientApplicationLocaleQueries _clientApplicationLocaleQueries,
	IClientApplicationLocaleCommands _clientApplicationLocaleCommands,
	IClientApplicationLocaleEntityFactory _clientApplicationLocaleEntityFactory,
	IClientApplicationTextResourceQueries _clientApplicationTextResourceQueries,
	IClientApplicationTextResourceCommands _clientApplicationTextResourceCommands,
	IClientApplicationTextResourceEntityFactory _clientApplicationTextResourceEntityFactory,
	ILocaleQueries _localeQueries,
	ITextResourceQueries _textResourceQueries)
	: GuidEditorViewModel<ClientApplicationDto, ClientApplication>(
		clientApplicationQueries,
		clientApplicationCommands,
		clientApplicationCommands)
{
	private readonly Type ModelType = typeof(ClientApplication);

	public IReadOnlyList<Locale> Locales { get; private set; } = [];
	public IReadOnlyList<ClientApplicationLocale> ClientApplicationLocales { get; private set; } = [];
	public IReadOnlyList<TextResource> TextResources { get; private set; } = [];
	public IReadOnlyList<ClientApplicationTextResource> ClientApplicationTextResources { get; private set; } = [];

	//

	public Task SetSelectedTabIndexAsync(
		int index)
		=> index switch
		{
			1 => LoadClientApplicationLocalesAsync(),
			2 => LoadClientApplicationTextResourcesAsync(),
			_ => Task.CompletedTask
		};

	public async Task SetDefaultLocale(
		string localeKey)
	{
		ClientApplicationLocale? currentDefault = ClientApplicationLocales.SingleOrDefault(cal => cal.IsDefault);
		ClientApplicationLocale? newDefault = ClientApplicationLocales.SingleOrDefault(cal => cal.LocaleKey == localeKey);

		if (newDefault is null ||
			currentDefault == newDefault)
			return;

		bool currentWasRemoved = false;
		if (currentDefault is not null)
		{
			currentDefault.IsDefault = false;
			currentWasRemoved = await _clientApplicationLocaleCommands.TryUpdateAsync(currentDefault);
		}
		else
			currentWasRemoved = true;

		if (currentWasRemoved)
		{
			newDefault.IsDefault = true;
			_ = await _clientApplicationLocaleCommands.TryUpdateAsync(newDefault);
		}
	}

	public async Task AddLocale(
		string localeKey)
	{
		if (ClientApplicationLocales.Any(cal => cal.LocaleKey == localeKey))
			return;

		ClientApplicationLocale cal = _clientApplicationLocaleEntityFactory.Create(Item.ClientApplicationGuid, localeKey, false);

		if (await _clientApplicationLocaleCommands.TryCreateAsync(cal))
			await LoadClientApplicationLocalesAsync();
	}

	public async Task RemoveLocale(
		string localeKey)
	{
		ClientApplicationLocale? item = ClientApplicationLocales.SingleOrDefault(cal => cal.LocaleKey == localeKey);

		if (item is null)
			return;

		bool deleted = await _clientApplicationLocaleCommands.TryDeleteAsync(item.ClientApplicationGuid, item.LocaleKey);

		if (deleted)
			ClientApplicationLocales = [.. ClientApplicationLocales.Where(cal => cal.LocaleKey != localeKey)];
	}

	public async Task AddTextResource(
		string textKey)
	{
		if (ClientApplicationTextResources.Any(cat => cat.TextKey == textKey))
			return;

		ClientApplicationTextResource cat = _clientApplicationTextResourceEntityFactory.Create(Item.ClientApplicationGuid, textKey);

		if (await _clientApplicationTextResourceCommands.TryCreateAsync(cat))
			await LoadClientApplicationTextResourcesAsync();
	}

	public async Task RemoveTextResource(
		string textKey)
	{
		ClientApplicationTextResource? item = ClientApplicationTextResources.SingleOrDefault(cat => cat.TextKey == textKey);

		if (item is null)
			return;

		bool deleted = await _clientApplicationTextResourceCommands.TryDeleteAsync(item.ClientApplicationGuid, item.TextKey);

		if (deleted)
			ClientApplicationTextResources = [.. ClientApplicationTextResources.Where(cat => cat.TextKey != textKey)];
	}

	//

	protected override async Task<ViewAuthorizationResult> AuthorizeCreateAsync(
		CancellationToken cancellationToken = default)
	{
		ClientApplication clientApplication = _clientApplicationEntityFactory.Create(Guid.CreateVersion7());
		clientApplication.IsActive = true;
		ClaimsPrincipal user = await _currentPrincipalAccessor.GetUserAsync();
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

		ClaimsPrincipal user = await _currentPrincipalAccessor.GetUserAsync();
		bool isAuthorized = await _authorization.CanReadAsync(clientApplicationGuid, user, cancellationToken);

		return isAuthorized
			? ViewAuthorizationResult.Authorized
			: ViewAuthorizationResult.Denied(StaticTexts.GetPrincipalNotAuthorizedText(OpType.Read, ModelType));
	}

	protected override async Task<ViewAuthorizationResult> AuthorizeUpdateAsync(
		ClientApplication item,
		CancellationToken cancellationToken = default)
	{
		ClaimsPrincipal user = await _currentPrincipalAccessor.GetUserAsync();
		bool isAuthorized = await _authorization.CanUpdateAsync(item.ClientApplicationGuid, item, user, cancellationToken);

		return isAuthorized
			? ViewAuthorizationResult.Authorized
			: ViewAuthorizationResult.Denied(StaticTexts.GetPrincipalNotAuthorizedText(OpType.Update, ModelType));
	}

	protected override async Task<ViewAuthorizationResult> AuthorizeDeleteAsync(
		ClientApplication item,
		CancellationToken cancellationToken = default)
	{
		ClaimsPrincipal user = await _currentPrincipalAccessor.GetUserAsync();
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

	//

	protected async Task LoadClientApplicationLocalesAsync()
	{
		ClientApplicationLocaleFilter calFilter = ClientApplicationLocaleFilter.All();
		calFilter.ClientApplicationGuid = Item.ClientApplicationGuid;

		ClientApplicationLocales = await _clientApplicationLocaleQueries.ListItemsAsync(calFilter);

		if (Locales.Any())
			return;

		// Filter removed - we will need *all* Locales for the "Add Locale"-dropdown anyway
		//LocaleFilter localeFilter = LocaleFilter.All();
		//localeFilter.LocaleKey = FilterFactory.CreateStringFilterFrom(ClientApplicationLocales.Select(cal => cal.LocaleKey));
		//Locales = await _localeQueries.ListItemsAsync(localeFilter);

		Locales = await _localeQueries.ListAllItemsAsync();
	}

	protected async Task LoadClientApplicationTextResourcesAsync()
	{
		ClientApplicationTextResourceFilter catFilter = ClientApplicationTextResourceFilter.All();
		catFilter.ClientApplicationGuid = Item.ClientApplicationGuid;

		ClientApplicationTextResources = await _clientApplicationTextResourceQueries.ListItemsAsync(catFilter);

		if (TextResources.Any())
			return;

		TextResources = await _textResourceQueries.ListAllItemsAsync();
	}
}