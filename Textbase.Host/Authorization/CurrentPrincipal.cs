using Textbase.Domain.Enumerations;

namespace Textbase.Host.Authorization;

public sealed record CurrentPrincipal(
	Guid EntraObjectId,
	Roles RolesValue,
	string? DisplayName,
	string? EmailAddress,
	IReadOnlyList<Guid> ClientApplicationGuids,
	IReadOnlyList<string> LocaleKeys)
{
	public bool HasApplicationRestrictions =>
		!RolesValue.HasFlag(Roles.Sysadmin) && ClientApplicationGuids.Count > 0;

	public bool HasLocaleRestrictions =>
		!RolesValue.HasFlag(Roles.Sysadmin) && LocaleKeys.Count > 0;
}
