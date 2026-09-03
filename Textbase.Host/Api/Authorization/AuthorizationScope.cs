using Textbase.Application.Features.ClientApplicationLocales;
using Textbase.Application.Features.ClientApplicationTextResources;
using Textbase.Domain.Enumerations;
using Textbase.Domain.Models;
using Textbase.Host.Authorization;
using Uwn.EntityFrameworkCore.Querying;

namespace Textbase.Host.Api.Authorization;

public sealed class AuthorizationScope(
	IClientApplicationLocaleQueries _clientApplicationLocaleQueries,
	IClientApplicationTextResourceQueries _clientApplicationTextResourceQueries,
	ICurrentPrincipalAccessor _currentPrincipalAccessor)
{
	private Task<IReadOnlyList<ClientApplicationLocale>>? _applicationLocalesTask;
	private Task<IReadOnlyList<ClientApplicationTextResource>>? _applicationTextResourcesTask;

	private readonly Dictionary<string, Task<IReadOnlyList<ClientApplicationTextResource>>> _applicationTextResourcesTasks = new(StringComparer.OrdinalIgnoreCase);

	public Task<CurrentPrincipal?> GetPrincipalAsync(
		CancellationToken cancellationToken)
		=> _currentPrincipalAccessor.GetAsync(cancellationToken);

	public static bool HasRole(
		CurrentPrincipal principal,
		Roles roles)
		=> principal.RolesValue.HasFlag(Roles.SysAdmin) || (principal.RolesValue & roles) != Roles.None;

	public static bool CanAccessApplication(
		CurrentPrincipal principal,
		Guid clientApplicationGuid)
		=> !principal.HasApplicationRestrictions || principal.ClientApplicationGuids.Contains(clientApplicationGuid);

	public static bool CanAccessLocale(
		CurrentPrincipal principal,
		string localeKey)
		=> !principal.HasLocaleRestrictions || principal.LocaleKeys.Contains(localeKey, StringComparer.OrdinalIgnoreCase);

	public static bool TryRestrictApplications(
		CurrentPrincipal principal,
		GuidFilter? filter,
		out GuidFilter? restrictedFilter)
	{
		if (!principal.HasApplicationRestrictions)
		{
			restrictedFilter = filter;
			return true;
		}

		IReadOnlyCollection<Guid> requestedGuids = filter?.Value is Guid value
			? [value]
			: filter?.AnyOf ?? principal.ClientApplicationGuids;

		Guid[] permittedGuids = [.. requestedGuids.Intersect(principal.ClientApplicationGuids)];
		if (permittedGuids.Length == 0)
		{
			restrictedFilter = null;
			return false;
		}

		restrictedFilter = new GuidFilter { AnyOf = permittedGuids };

		return true;
	}

	public static bool TryRestrictStrings(
		StringFilter? filter,
		IReadOnlyCollection<string> permittedValues,
		out StringFilter? restrictedFilter)
	{
		if (!String.IsNullOrEmpty(filter?.Value) && filter.AnyOf?.Count > 0)
		{
			restrictedFilter = null;
			return false;
		}

		StringComparer comparer = filter?.IgnoreCase == false ? StringComparer.Ordinal : StringComparer.OrdinalIgnoreCase;
		IEnumerable<string> restrictedValues = permittedValues;

		if (filter?.AnyOf?.Count > 0)
			restrictedValues = restrictedValues.Intersect(filter.AnyOf, comparer);
		else if (!String.IsNullOrEmpty(filter?.Value))
			restrictedValues = restrictedValues.Where(value => Matches(value, filter));

		string[] values = [.. restrictedValues.Distinct(comparer).Order(comparer)];
		if (values.Length == 0)
		{
			restrictedFilter = null;
			return false;
		}

		restrictedFilter = new StringFilter
		{
			AnyOf = values,
			IgnoreCase = filter?.IgnoreCase ?? true
		};

		return true;
	}

	public async Task<IReadOnlyCollection<string>> GetPermittedTextKeysAsync(
		CurrentPrincipal principal,
		CancellationToken cancellationToken)
	{
		IReadOnlyList<ClientApplicationTextResource> textResources = await GetApplicationTextResourcesAsync(cancellationToken);
		IEnumerable<ClientApplicationTextResource> permittedTextResources = textResources;

		if (principal.HasApplicationRestrictions)
			permittedTextResources = permittedTextResources.Where(textResource => principal.ClientApplicationGuids.Contains(textResource.ClientApplicationGuid));

		if (principal.HasLocaleRestrictions)
		{
			IReadOnlyList<ClientApplicationLocale> applicationLocales = await GetApplicationLocalesAsync(cancellationToken);
			Guid[] applicationGuids = [.. applicationLocales
				.Where(locale => principal.LocaleKeys.Contains(locale.LocaleKey, StringComparer.OrdinalIgnoreCase))
				.Select(locale => locale.ClientApplicationGuid)
				.Distinct()];

			permittedTextResources = permittedTextResources.Where(textResource => applicationGuids.Contains(textResource.ClientApplicationGuid));
		}

		return [.. permittedTextResources.Select(textResource => textResource.TextKey).Distinct(StringComparer.OrdinalIgnoreCase)];
	}

	public async ValueTask<bool> CanAccessTextAsync(
		CurrentPrincipal principal,
		string textKey,
		CancellationToken cancellationToken)
	{
		if (!principal.HasApplicationRestrictions &&
			!principal.HasLocaleRestrictions)
			return true;

		IReadOnlyList<ClientApplicationTextResource> textResources = await GetApplicationTextResourcesAsync(textKey, cancellationToken);
		if (textResources.Count == 0)
			return false;

		IReadOnlyList<ClientApplicationLocale> applicationLocales = await GetApplicationLocalesAsync(cancellationToken);

		return textResources.Any(textResource =>
			CanAccessApplication(principal, textResource.ClientApplicationGuid) &&
			(!principal.HasLocaleRestrictions || applicationLocales.Any(locale =>
				locale.ClientApplicationGuid == textResource.ClientApplicationGuid && CanAccessLocale(principal, locale.LocaleKey))));
	}

	public async ValueTask<bool> CanAccessTranslationAsync(
		CurrentPrincipal principal,
		string localeKey,
		string textKey,
		CancellationToken cancellationToken)
	{
		return CanAccessLocale(principal, localeKey) &&
			await CanAccessTextAsync(principal, textKey, cancellationToken);
	}

	private Task<IReadOnlyList<ClientApplicationLocale>> GetApplicationLocalesAsync(
		CancellationToken cancellationToken)
	{
		_applicationLocalesTask ??= _clientApplicationLocaleQueries.ListItemsAsync(ClientApplicationLocaleFilter.All(), cancellationToken);

		return _applicationLocalesTask;
	}

	private Task<IReadOnlyList<ClientApplicationTextResource>> GetApplicationTextResourcesAsync(
		CancellationToken cancellationToken)
	{
		_applicationTextResourcesTask ??= _clientApplicationTextResourceQueries.ListItemsAsync(ClientApplicationTextResourceFilter.All(), cancellationToken);

		return _applicationTextResourcesTask;
	}

	private Task<IReadOnlyList<ClientApplicationTextResource>> GetApplicationTextResourcesAsync(
		string textKey,
		CancellationToken cancellationToken)
	{
		if (!_applicationTextResourcesTasks.TryGetValue(textKey, out Task<IReadOnlyList<ClientApplicationTextResource>>? task))
		{
			ClientApplicationTextResourceFilter filter = new()
			{
				TextKey = FilterFactory.CreateStringFilterFrom(textKey, StringMatching.Exact)
			};

			task = _clientApplicationTextResourceQueries.ListItemsAsync(filter, cancellationToken);
			_applicationTextResourcesTasks.Add(textKey, task);
		}

		return task;
	}

	private static bool Matches(
		string value,
		StringFilter filter)
	{
		StringComparison comparison = filter.IgnoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;

		return filter.Matching switch
		{
			StringMatching.Exact => String.Equals(value, filter.Value, comparison),
			StringMatching.StartsWith => value.StartsWith(filter.Value!, comparison),
			StringMatching.EndsWith => value.EndsWith(filter.Value!, comparison),
			StringMatching.Contains => value.Contains(filter.Value!, comparison),
			_ => false
		};
	}
}
