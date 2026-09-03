/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using System.Security.Claims;
using Textbase.Application.Features.AuthPrincipals;
using Textbase.Contracts.Models;

namespace Textbase.Host.Api.Authorization;

public interface IAuthPrincipalAuthorization
{
	ValueTask<bool> CanCreateAsync(AuthPrincipalDto dto, ClaimsPrincipal user, CancellationToken cancellationToken = default);
	ValueTask<bool> CanReadAsync(Guid entraObjectId, ClaimsPrincipal user, CancellationToken cancellationToken = default);
	ValueTask<bool> CanCountAsync(AuthPrincipalFilter filter, ClaimsPrincipal user, CancellationToken cancellationToken = default);
	ValueTask<bool> CanListAsync(AuthPrincipalFilter filter, ClaimsPrincipal user, CancellationToken cancellationToken = default);
	ValueTask<bool> CanUpdateAsync(Guid entraObjectId, AuthPrincipalDto dto, ClaimsPrincipal user, CancellationToken cancellationToken = default);
	ValueTask<bool> CanDeleteAsync(Guid entraObjectId, ClaimsPrincipal user, CancellationToken cancellationToken = default);

	
}