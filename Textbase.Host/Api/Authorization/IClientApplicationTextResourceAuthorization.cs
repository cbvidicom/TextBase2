/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using System.Security.Claims;
using Textbase.Application.Features.ClientApplicationTextResources;
using Textbase.Contracts.Models;

namespace Textbase.Host.Api.Authorization;

public interface IClientApplicationTextResourceAuthorization
{
	ValueTask<bool> CanCreateAsync(ClientApplicationTextResourceDto dto, ClaimsPrincipal user, CancellationToken cancellationToken = default);
	ValueTask<bool> CanReadAsync(Guid clientApplicationGuid, string textKey, ClaimsPrincipal user, CancellationToken cancellationToken = default);
	ValueTask<bool> CanCountAsync(ClientApplicationTextResourceFilter filter, ClaimsPrincipal user, CancellationToken cancellationToken = default);
	ValueTask<bool> CanListAsync(ClientApplicationTextResourceFilter filter, ClaimsPrincipal user, CancellationToken cancellationToken = default);
	ValueTask<bool> CanUpdateAsync(Guid clientApplicationGuid, string textKey, ClientApplicationTextResourceDto dto, ClaimsPrincipal user, CancellationToken cancellationToken = default);
	ValueTask<bool> CanDeleteAsync(Guid clientApplicationGuid, string textKey, ClaimsPrincipal user, CancellationToken cancellationToken = default);

	
}