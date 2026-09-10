using Microsoft.Extensions.Options;
using Textbase.Application.Features.Locales;
using Textbase.Application.Features.Translations;
using Textbase.Domain.Models;
using Textbase.Host.Authorization;
using Uwn.Blazor.Models.Common;

namespace Textbase.Host.ViewModels.MissingTranslations;

public sealed class MissingTranslationListViewModel(
	ICurrentPrincipalAccessor _currentPrincipalAccessor,
	ILocaleQueries _localeQueries,
	IOptions<QueryingOptions> _queryingOptions,
	ITranslationServerQueries _translationQueries)
{
	private readonly HashSet<string> _ignoredLocaleKeys = new(StringComparer.OrdinalIgnoreCase);
	private IReadOnlyList<MissingTranslation> _allItems = [];
	private IReadOnlyDictionary<string, string?> _parentLocaleKeys = new Dictionary<string, string?>(StringComparer.OrdinalIgnoreCase);

	public IReadOnlyList<MissingTranslation> Items { get; private set; } = [];
	public IReadOnlyList<string> IgnoredLocaleKeys => [.. _ignoredLocaleKeys.Order(StringComparer.OrdinalIgnoreCase)];
	public int PageSize => _queryingOptions.Value.DefaultPageSize;

	public async Task LoadAsync(
		CancellationToken cancellationToken = default)
	{
		CurrentPrincipal? principal = await _currentPrincipalAccessor.GetAsync(cancellationToken);
		if (principal is null)
		{
			_allItems = [];
			Items = [];
			return;
		}

		IReadOnlyList<Locale> locales = await _localeQueries.ListAllItemsAsync(cancellationToken);
		_parentLocaleKeys = locales.ToDictionary(locale => locale.LocaleKey, locale => locale.ParentLocaleKey, StringComparer.OrdinalIgnoreCase);

		IReadOnlyCollection<Guid>? clientApplicationGuids = principal.HasApplicationRestrictions ? principal.ClientApplicationGuids : null;
		IReadOnlyCollection<string>? localeKeys = principal.HasLocaleRestrictions ? principal.LocaleKeys : null;
		_allItems = await _translationQueries.ListMissingTranslationsAsync(clientApplicationGuids, localeKeys, cancellationToken);

		ApplyIgnoredLocales();
	}

	public void IgnoreLocale(
		string localeKey)
	{
		_ignoredLocaleKeys.RemoveWhere(ignoredLocaleKey => IsDescendantOf(ignoredLocaleKey, localeKey));
		_ignoredLocaleKeys.Add(localeKey);
		ApplyIgnoredLocales();
	}

	public void UnignoreLocale(
		string localeKey)
	{
		_ignoredLocaleKeys.Remove(localeKey);
		ApplyIgnoredLocales();
	}

	private void ApplyIgnoredLocales()
		=> Items = [.. _allItems.Where(item => !_ignoredLocaleKeys.Any(ignoredLocaleKey => IsDescendantOf(item.LocaleKey, ignoredLocaleKey)))];

	private bool IsDescendantOf(
		string localeKey,
		string ancestorLocaleKey)
	{
		string? currentLocaleKey = localeKey;
		HashSet<string> visitedLocaleKeys = new(StringComparer.OrdinalIgnoreCase);

		while (!String.IsNullOrWhiteSpace(currentLocaleKey) && visitedLocaleKeys.Add(currentLocaleKey))
		{
			if (String.Equals(currentLocaleKey, ancestorLocaleKey, StringComparison.OrdinalIgnoreCase))
			{
				return true;
			}

			if (!_parentLocaleKeys.TryGetValue(currentLocaleKey, out currentLocaleKey))
			{
				return false;
			}
		}

		return false;
	}
}
