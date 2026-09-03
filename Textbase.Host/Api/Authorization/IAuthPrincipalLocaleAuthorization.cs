/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using System.Security.Claims;
using Textbase.Application.Features.AuthPrincipalLocales;
using Textbase.Contracts.Models;

namespace Textbase.Host.Api.Authorization;

public interface IAuthPrincipalLocaleAuthorization
{
	ValueTask<bool> CanCreateAsync(AuthPrincipalLocaleDto dto, ClaimsPrincipal user, CancellationToken cancellationToken = default);
	ValueTask<bool> CanReadAsync(Guid entraObjectId, string localeKey, ClaimsPrincipal user, CancellationToken cancellationToken = default);
	ValueTask<bool> CanCountAsync(AuthPrincipalLocaleFilter filter, ClaimsPrincipal user, CancellationToken cancellationToken = default);
	ValueTask<bool> CanListAsync(AuthPrincipalLocaleFilter filter, ClaimsPrincipal user, CancellationToken cancellationToken = default);
	ValueTask<bool> CanUpdateAsync(Guid entraObjectId, string localeKey, AuthPrincipalLocaleDto dto, ClaimsPrincipal user, CancellationToken cancellationToken = default);
	ValueTask<bool> CanDeleteAsync(Guid entraObjectId, string localeKey, ClaimsPrincipal user, CancellationToken cancellationToken = default);

	
}