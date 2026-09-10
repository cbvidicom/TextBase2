namespace Textbase.Application.Features.AuthPrincipals;

public sealed record AuthPrincipalReferenceCounts(
	Guid EntraObjectId,
	int ClientApplicationCount,
	int LocaleCount);
