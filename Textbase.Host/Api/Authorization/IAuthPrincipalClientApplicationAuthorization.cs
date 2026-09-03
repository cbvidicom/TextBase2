/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using System.Security.Claims;
using Textbase.Application.Features.AuthPrincipalClientApplications;
using Textbase.Contracts.Models;

namespace Textbase.Host.Api.Authorization;

public interface IAuthPrincipalClientApplicationAuthorization
{
	ValueTask<bool> CanCreateAsync(AuthPrincipalClientApplicationDto dto, ClaimsPrincipal user, CancellationToken cancellationToken = default);
	ValueTask<bool> CanReadAsync(Guid entraObjectId, Guid clientApplicationGuid, ClaimsPrincipal user, CancellationToken cancellationToken = default);
	ValueTask<bool> CanCountAsync(AuthPrincipalClientApplicationFilter filter, ClaimsPrincipal user, CancellationToken cancellationToken = default);
	ValueTask<bool> CanListAsync(AuthPrincipalClientApplicationFilter filter, ClaimsPrincipal user, CancellationToken cancellationToken = default);
	ValueTask<bool> CanUpdateAsync(Guid entraObjectId, Guid clientApplicationGuid, AuthPrincipalClientApplicationDto dto, ClaimsPrincipal user, CancellationToken cancellationToken = default);
	ValueTask<bool> CanDeleteAsync(Guid entraObjectId, Guid clientApplicationGuid, ClaimsPrincipal user, CancellationToken cancellationToken = default);

	
}