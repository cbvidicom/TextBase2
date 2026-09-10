using Textbase.Application.Features.Formalities;
using Textbase.Application.Features.Locales;
using Textbase.Application.Features.Presentations;
using Textbase.Application.Features.TextResources;
using Textbase.Application.Features.Translations;
using Textbase.Host.Api.Authorization;
using Textbase.Host.Authorization;

namespace Textbase.Host.ViewModels.Translations;

public sealed class TranslationEditorViewModel(
	AuthorizationScope _authorizationScope,
	IFormalityQueries _formalityQueries,
	ILocaleQueries _localeQueries,
	IPresentationQueries _presentationQueries,
	ITextResourceQueries _textResourceQueries,
	ITranslationEntityFactory _translationEntityFactory)
{
	private IReadOnlyList<Locale> _locales = [];

	public IReadOnlyList<string> LocaleKeys { get; private set; } = [];
	public IReadOnlyList<string> TextKeys { get; private set; } = [];
	public IReadOnlyList<string> FormalityKeys { get; private set; } = [];
	public IReadOnlyList<string> PresentationKeys { get; private set; } = [];

	public async Task<Translation> CreateNewItemAsync(
		CurrentPrincipal principal,
		string? initialLocaleKey,
		string? initialTextKey,
		CancellationToken cancellationToken = default)
	{
		await LoadKeyValuesAsync(principal, cancellationToken);

		string localeKey = !String.IsNullOrWhiteSpace(initialLocaleKey) && LocaleKeys.Contains(initialLocaleKey, StringComparer.OrdinalIgnoreCase) ? initialLocaleKey : String.Empty;
		string textKey = !String.IsNullOrWhiteSpace(initialTextKey) && TextKeys.Contains(initialTextKey, StringComparer.OrdinalIgnoreCase) ? initialTextKey : String.Empty;
		string formalityKey = FormalityKeys.FirstOrDefault(key => String.Equals(key, "Default", StringComparison.OrdinalIgnoreCase)) ?? FormalityKeys.FirstOrDefault() ?? String.Empty;
		string presentationKey = PresentationKeys.FirstOrDefault(key => String.Equals(key, "Default", StringComparison.OrdinalIgnoreCase)) ?? PresentationKeys.FirstOrDefault() ?? String.Empty;

		return _translationEntityFactory.Create(localeKey, textKey, formalityKey, presentationKey, String.Empty);
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

	public void SelectParentLocale(
		Translation item)
	{
		string? parentLocaleKey = GetParentLocaleKey(item.LocaleKey);
		if (!String.IsNullOrWhiteSpace(parentLocaleKey))
		{
			item.LocaleKey = parentLocaleKey;
		}
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
}
