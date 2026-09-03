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
		if (!CanAccessLocale(principal, localeKey))
			return false;

		if (!principal.HasApplicationRestrictions &&
			!principal.HasLocaleRestrictions)
			return true;

		IReadOnlyList<ClientApplicationTextResource> textResources = await GetApplicationTextResourcesAsync(textKey, cancellationToken);
		IReadOnlyList<ClientApplicationLocale> applicationLocales = await GetApplicationLocalesAsync(cancellationToken);

		return textResources.Any(textResource =>
			CanAccessApplication(principal, textResource.ClientApplicationGuid) &&
			applicationLocales.Any(locale => locale.ClientApplicationGuid == textResource.ClientApplicationGuid &&
				String.Equals(locale.LocaleKey, localeKey, StringComparison.OrdinalIgnoreCase)));
	}

	public static bool TryGetExactValue(
		StringFilter? filter,
		out string value)
	{
		if (filter is not null &&
			filter.Matching == StringMatching.Exact &&
			!String.IsNullOrWhiteSpace(filter.Value))
		{
			value = filter.Value;
			return true;
		}

		value = String.Empty;
		return false;
	}

	private Task<IReadOnlyList<ClientApplicationLocale>> GetApplicationLocalesAsync(
		CancellationToken cancellationToken)
	{
		_applicationLocalesTask ??= _clientApplicationLocaleQueries.ListItemsAsync(ClientApplicationLocaleFilter.All(), cancellationToken);

		return _applicationLocalesTask;
	}

	private Task<IReadOnlyList<ClientApplicationTextResource>> GetApplicationTextResourcesAsync(
		string textKey,
		CancellationToken cancellationToken)
	{
		if (!_applicationTextResourcesTasks.TryGetValue(textKey, out Task<IReadOnlyList<ClientApplicationTextResource>>? task))
		{
			ClientApplicationTextResourceFilter filter = new()
			{
				TextKey = new StringFilter(textKey, StringMatching.Exact)
			};

			task = _clientApplicationTextResourceQueries.ListItemsAsync(filter, cancellationToken);
			_applicationTextResourcesTasks.Add(textKey, task);
		}

		return task;
	}
}
