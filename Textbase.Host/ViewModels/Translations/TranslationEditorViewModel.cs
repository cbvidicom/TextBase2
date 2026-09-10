using System.Security.Claims;
using Textbase.Application.Features.Formalities;
using Textbase.Application.Features.Locales;
using Textbase.Application.Features.Presentations;
using Textbase.Application.Features.TextResources;
using Textbase.Application.Features.Translations;
using Textbase.Domain.Enumerations;
using Textbase.Domain.Models;
using Textbase.Host.Api.Authorization;
using Textbase.Host.Authorization;
using Textbase.Host.Enumerations;
using Textbase.Infrastructure.Persistence.Translations;

namespace Textbase.Host.ViewModels.Translations;

public sealed class TranslationEditorViewModel(
	AuthorizationScope _authorizationScope,
	ICurrentPrincipalAccessor _currentPrincipalAccessor,
	IFormalityQueries _formalityQueries,
	ILocaleQueries _localeQueries,
	IPresentationQueries _presentationQueries,
	ITextResourceQueries _textResourceQueries,
	ITranslationAuthorization _translationAuthorization,
	ITranslationCommands _translationCommands,
	ITranslationEntityFactory _translationEntityFactory,
	ITranslationQueries _translationQueries)
{
	private IReadOnlyList<Locale> _locales = [];

	public Translation? Item { get; private set; }
	public IReadOnlyList<string> LocaleKeys { get; private set; } = [];
	public IReadOnlyList<string> TextKeys { get; private set; } = [];
	public IReadOnlyList<string> FormalityKeys { get; private set; } = [];
	public IReadOnlyList<string> PresentationKeys { get; private set; } = [];
	public bool HasAccess { get; private set; }
	public bool CanSave { get; private set; }
	public bool CanDelete { get; private set; }
	public bool IsNewItem { get; private set; }
	public bool IsReadOnly => !CanSave;
	public string? AccessDeniedMessage { get; private set; }

	public async Task InitializeAsync(
		string? localeKey,
		string? textKey,
		string? formalityKey,
		string? presentationKey,
		CancellationToken cancellationToken = default)
	{
		Item = null;
		CanSave = false;
		CanDelete = false;
		AccessDeniedMessage = null;

		bool hasLocaleKey = !String.IsNullOrWhiteSpace(localeKey);
		bool hasTextKey = !String.IsNullOrWhiteSpace(textKey);
		bool hasFormalityKey = !String.IsNullOrWhiteSpace(formalityKey);
		bool hasPresentationKey = !String.IsNullOrWhiteSpace(presentationKey);
		bool isCreateRoute = !hasFormalityKey && !hasPresentationKey && hasLocaleKey == hasTextKey;
		bool isEditRoute = hasLocaleKey && hasTextKey && hasFormalityKey && hasPresentationKey;

		if (!isCreateRoute && !isEditRoute)
		{
			HasAccess = false;
			IsNewItem = false;
			AccessDeniedMessage = "Invalid Translation key.";
			return;
		}

		IsNewItem = isCreateRoute;
		if (IsNewItem)
		{
			await InitializeNewItemAsync(localeKey, textKey, cancellationToken);
			return;
		}

		await InitializeExistingItemAsync(localeKey!, textKey!, formalityKey!, presentationKey!, cancellationToken);
	}

	public string? GetParentLocaleKey(
		string? localeKey)
	{
		if (String.IsNullOrWhiteSpace(localeKey))
		{
			return null;
		}

		string? parentLocaleKey = _locales.FirstOrDefault(locale => String.Equals(locale.LocaleKey, localeKey, StringComparison.OrdinalIgnoreCase))?.ParentLocaleKey;
		return parentLocaleKey is not null && LocaleKeys.Contains(parentLocaleKey, StringComparer.OrdinalIgnoreCase) ? parentLocaleKey : null;
	}

	public void SelectParentLocale()
	{
		if (Item is null)
		{
			return;
		}

		string? parentLocaleKey = GetParentLocaleKey(Item.LocaleKey);
		if (!String.IsNullOrWhiteSpace(parentLocaleKey))
		{
			Item.LocaleKey = parentLocaleKey;
		}
	}

	public async Task CreateNewItemAsync(
		CancellationToken cancellationToken = default)
	{
		IsNewItem = true;
		await InitializeNewItemAsync(null, null, cancellationToken);
	}

	public async Task<bool> SaveAsync(
		CancellationToken cancellationToken = default)
	{
		if (Item is null || !HasValidKey(Item))
		{
			return false;
		}

		ClaimsPrincipal user = await _currentPrincipalAccessor.GetUserAsync();
		bool succeeded;

		if (IsNewItem)
		{
			if (!await _translationAuthorization.CanCreateAsync(Item, user, cancellationToken))
			{
				throw new UnauthorizedAccessException(StaticTexts.GetPrincipalNotAuthorizedText(OpType.Create, typeof(Translation)));
			}

			succeeded = await _translationCommands.TryCreateAsync(Item);
		}
		else
		{
			if (!await _translationAuthorization.CanUpdateAsync(Item.LocaleKey, Item.TextKey, Item.FormalityKey, Item.PresentationKey, Item, user, cancellationToken))
			{
				throw new UnauthorizedAccessException(StaticTexts.GetPrincipalNotAuthorizedText(OpType.Update, typeof(Translation)));
			}

			succeeded = await _translationCommands.TryUpdateAsync(Item);
		}

		if (!succeeded)
		{
			throw new InvalidOperationException("Saving Translation failed.");
		}

		return true;
	}

	public async Task RefreshDeleteAuthorizationAsync(
		CancellationToken cancellationToken = default)
	{
		if (Item is null)
		{
			CanDelete = false;
			return;
		}

		ClaimsPrincipal user = await _currentPrincipalAccessor.GetUserAsync();
		CanDelete = await _translationAuthorization.CanDeleteAsync(Item.LocaleKey, Item.TextKey, Item.FormalityKey, Item.PresentationKey, user, cancellationToken);
	}

	public async Task<bool> DeleteAsync(
		CancellationToken cancellationToken = default)
	{
		if (Item is null || IsNewItem)
		{
			return false;
		}

		ClaimsPrincipal user = await _currentPrincipalAccessor.GetUserAsync();
		if (!await _translationAuthorization.CanDeleteAsync(Item.LocaleKey, Item.TextKey, Item.FormalityKey, Item.PresentationKey, user, cancellationToken))
		{
			CanDelete = false;
			return false;
		}

		bool succeeded = await _translationCommands.TryDeleteAsync(Item.LocaleKey, Item.TextKey, Item.FormalityKey, Item.PresentationKey);
		if (!succeeded)
		{
			throw new InvalidOperationException("Deleting Translation failed.");
		}

		return true;
	}

	private async Task InitializeNewItemAsync(
		string? initialLocaleKey,
		string? initialTextKey,
		CancellationToken cancellationToken)
	{
		CurrentPrincipal? principal = await _currentPrincipalAccessor.GetAsync(cancellationToken);
		HasAccess = principal is not null && _authorizationScope.HasRole(principal, Roles.Translator);
		if (!HasAccess)
		{
			AccessDeniedMessage = StaticTexts.GetPrincipalNotAuthorizedText(OpType.Create, typeof(Translation));
			return;
		}

		await LoadKeyValuesAsync(principal!, cancellationToken);

		string localeKey = !String.IsNullOrWhiteSpace(initialLocaleKey) && LocaleKeys.Contains(initialLocaleKey, StringComparer.OrdinalIgnoreCase) ? initialLocaleKey : String.Empty;
		string textKey = !String.IsNullOrWhiteSpace(initialTextKey) && TextKeys.Contains(initialTextKey, StringComparer.OrdinalIgnoreCase) ? initialTextKey : String.Empty;
		string formalityKey = FormalityKeys.FirstOrDefault(key => String.Equals(key, "Default", StringComparison.OrdinalIgnoreCase)) ?? FormalityKeys.FirstOrDefault() ?? String.Empty;
		string presentationKey = PresentationKeys.FirstOrDefault(key => String.Equals(key, "Default", StringComparison.OrdinalIgnoreCase)) ?? PresentationKeys.FirstOrDefault() ?? String.Empty;

		Item = _translationEntityFactory.Create(localeKey, textKey, formalityKey, presentationKey, String.Empty);
		CanSave = true;
		CanDelete = false;
		AccessDeniedMessage = null;
	}

	private async Task InitializeExistingItemAsync(
		string localeKey,
		string textKey,
		string formalityKey,
		string presentationKey,
		CancellationToken cancellationToken)
	{
		ClaimsPrincipal user = await _currentPrincipalAccessor.GetUserAsync();
		HasAccess = await _translationAuthorization.CanReadAsync(localeKey, textKey, formalityKey, presentationKey, user, cancellationToken);
		if (!HasAccess)
		{
			AccessDeniedMessage = StaticTexts.GetPrincipalNotAuthorizedText(OpType.Read, typeof(Translation));
			return;
		}

		Item = await _translationQueries.ReadAsync(localeKey, textKey, formalityKey, presentationKey);
		if (Item is null)
		{
			throw new KeyNotFoundException("Translation not found.");
		}

		CanSave = await _translationAuthorization.CanUpdateAsync(localeKey, textKey, formalityKey, presentationKey, Item, user, cancellationToken);
		CanDelete = await _translationAuthorization.CanDeleteAsync(localeKey, textKey, formalityKey, presentationKey, user, cancellationToken);
	}

	private async Task LoadKeyValuesAsync(
		CurrentPrincipal principal,
		CancellationToken cancellationToken)
	{
		IReadOnlyList<Locale> locales = await _localeQueries.ListAllItemsAsync(cancellationToken);
		IReadOnlyList<TextResource> textResources = await _textResourceQueries.ListAllItemsAsync(cancellationToken);

		if (principal.HasLocaleRestrictions)
		{
			locales = [.. locales.Where(locale => principal.LocaleKeys.Contains(locale.LocaleKey, StringComparer.OrdinalIgnoreCase))];
		}

		if (principal.HasApplicationRestrictions || principal.HasLocaleRestrictions)
		{
			IReadOnlyCollection<string> permittedTextKeys = await _authorizationScope.GetPermittedTextKeysAsync(principal, cancellationToken);
			textResources = [.. textResources.Where(textResource => permittedTextKeys.Contains(textResource.TextKey, StringComparer.OrdinalIgnoreCase))];
		}

		_locales = locales;
		LocaleKeys = [.. locales.Select(locale => locale.LocaleKey).Order(StringComparer.OrdinalIgnoreCase)];
		TextKeys = [.. textResources.Select(textResource => textResource.TextKey).Order(StringComparer.OrdinalIgnoreCase)];
		FormalityKeys = [.. (await _formalityQueries.ListAllItemsAsync(cancellationToken)).Select(formality => formality.FormalityKey).Order(StringComparer.OrdinalIgnoreCase)];
		PresentationKeys = [.. (await _presentationQueries.ListAllItemsAsync(cancellationToken)).Select(presentation => presentation.PresentationKey).Order(StringComparer.OrdinalIgnoreCase)];
	}

	private static bool HasValidKey(
		Translation translation)
		=> !String.IsNullOrWhiteSpace(translation.LocaleKey) &&
			!String.IsNullOrWhiteSpace(translation.TextKey) &&
			!String.IsNullOrWhiteSpace(translation.FormalityKey) &&
			!String.IsNullOrWhiteSpace(translation.PresentationKey);
}
