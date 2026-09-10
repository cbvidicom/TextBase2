using System.Text.Json;
using Microsoft.Extensions.Options;
using Microsoft.JSInterop;
using Textbase.Application.Features.Locales;
using Textbase.Application.Features.Translations;
using Textbase.Domain.Models;
using Textbase.Host.Authorization;
using Uwn.Blazor.Models.Common;

namespace Textbase.Host.ViewModels.MissingTranslations;

public sealed class MissingTranslationListViewModel(
	ICurrentPrincipalAccessor _currentPrincipalAccessor,
	IJSRuntime _jsRuntime,
	ILocaleQueries _localeQueries,
	IOptions<QueryingOptions> _queryingOptions,
	ITranslationServerQueries _translationQueries)
{
	private readonly HashSet<string> _ignoredLocaleKeys = new(StringComparer.OrdinalIgnoreCase);
	private IReadOnlyList<MissingTranslation> _allItems = [];
	private Guid? _entraObjectId;
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
			_entraObjectId = null;
			_allItems = [];
			Items = [];
			return;
		}

		_entraObjectId = principal.EntraObjectId;

		IReadOnlyList<Locale> locales = await _localeQueries.ListAllItemsAsync(cancellationToken);
		_parentLocaleKeys = locales.ToDictionary(locale => locale.LocaleKey, locale => locale.ParentLocaleKey, StringComparer.OrdinalIgnoreCase);

		IReadOnlyCollection<Guid>? clientApplicationGuids = principal.HasApplicationRestrictions ? principal.ClientApplicationGuids : null;
		IReadOnlyCollection<string>? localeKeys = principal.HasLocaleRestrictions ? principal.LocaleKeys : null;
		_allItems = await _translationQueries.ListMissingTranslationsAsync(clientApplicationGuids, localeKeys, cancellationToken);

		ApplyIgnoredLocales();
	}

	public async Task LoadIgnoredLocalesAsync(
		CancellationToken cancellationToken = default)
	{
		if (_entraObjectId is null)
		{
			return;
		}

		string? json = await _jsRuntime.InvokeAsync<string?>("localStorage.getItem", cancellationToken, GetStorageKey(_entraObjectId.Value));
		_ignoredLocaleKeys.Clear();

		if (!String.IsNullOrWhiteSpace(json))
		{
			try
			{
				string[]? localeKeys = JsonSerializer.Deserialize<string[]>(json);
				if (localeKeys is not null)
				{
					foreach (string localeKey in localeKeys)
					{
						if (_parentLocaleKeys.ContainsKey(localeKey))
						{
							_ignoredLocaleKeys.Add(localeKey);
						}
					}
				}
			}
			catch (JsonException)
			{
				// Ignore invalid browser data and replace it on the next change.
			}
		}

		ApplyIgnoredLocales();
	}

	public async Task IgnoreLocaleAsync(
		string localeKey,
		CancellationToken cancellationToken = default)
	{
		_ignoredLocaleKeys.RemoveWhere(ignoredLocaleKey => IsDescendantOf(ignoredLocaleKey, localeKey));
		_ignoredLocaleKeys.Add(localeKey);
		ApplyIgnoredLocales();
		await SaveIgnoredLocalesAsync(cancellationToken);
	}

	public async Task UnignoreLocaleAsync(
		string localeKey,
		CancellationToken cancellationToken = default)
	{
		_ignoredLocaleKeys.Remove(localeKey);
		ApplyIgnoredLocales();
		await SaveIgnoredLocalesAsync(cancellationToken);
	}

	private void ApplyIgnoredLocales()
		=> Items = [.. _allItems.Where(item => !_ignoredLocaleKeys.Any(ignoredLocaleKey => IsDescendantOf(item.LocaleKey, ignoredLocaleKey)))];

	private static string GetStorageKey(
		Guid entraObjectId)
		=> $"TextBase2.MissingTranslations.IgnoredLocales.{entraObjectId:D}";

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

	private async Task SaveIgnoredLocalesAsync(
		CancellationToken cancellationToken)
	{
		if (_entraObjectId is null)
		{
			return;
		}

		string json = JsonSerializer.Serialize(IgnoredLocaleKeys);
		await _jsRuntime.InvokeVoidAsync("localStorage.setItem", cancellationToken, GetStorageKey(_entraObjectId.Value), json);
	}
}
