/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using System.Security.Claims;
using Textbase.Application.Features.ClientApplications;
using Textbase.Contracts.Models;

namespace Textbase.Host.Api.Authorization;

public interface IClientApplicationAuthorization
{
	ValueTask<bool> CanCreateAsync(ClientApplicationDto dto, ClaimsPrincipal user, CancellationToken cancellationToken = default);
	ValueTask<bool> CanReadAsync(Guid clientApplicationGuid, ClaimsPrincipal user, CancellationToken cancellationToken = default);
	ValueTask<bool> CanCountAsync(ClientApplicationFilter filter, ClaimsPrincipal user, CancellationToken cancellationToken = default);
	ValueTask<bool> CanListAsync(ClientApplicationFilter filter, ClaimsPrincipal user, CancellationToken cancellationToken = default);
	ValueTask<bool> CanUpdateAsync(Guid clientApplicationGuid, ClientApplicationDto dto, ClaimsPrincipal user, CancellationToken cancellationToken = default);
	ValueTask<bool> CanDeleteAsync(Guid clientApplicationGuid, ClaimsPrincipal user, CancellationToken cancellationToken = default);

	
}