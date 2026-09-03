/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using System.Security.Claims;
using Textbase.Application.Features.ClientApplicationLocales;
using Textbase.Contracts.Models;

namespace Textbase.Host.Api.Authorization;

public interface IClientApplicationLocaleAuthorization
{
	ValueTask<bool> CanCreateAsync(ClientApplicationLocaleDto dto, ClaimsPrincipal user, CancellationToken cancellationToken = default);
	ValueTask<bool> CanReadAsync(Guid clientApplicationGuid, string localeKey, ClaimsPrincipal user, CancellationToken cancellationToken = default);
	ValueTask<bool> CanCountAsync(ClientApplicationLocaleFilter filter, ClaimsPrincipal user, CancellationToken cancellationToken = default);
	ValueTask<bool> CanListAsync(ClientApplicationLocaleFilter filter, ClaimsPrincipal user, CancellationToken cancellationToken = default);
	ValueTask<bool> CanUpdateAsync(Guid clientApplicationGuid, string localeKey, ClientApplicationLocaleDto dto, ClaimsPrincipal user, CancellationToken cancellationToken = default);
	ValueTask<bool> CanDeleteAsync(Guid clientApplicationGuid, string localeKey, ClaimsPrincipal user, CancellationToken cancellationToken = default);

	
	ValueTask<bool> CanReadByClientApplicationGuidAsync(Guid key, ClaimsPrincipal user, CancellationToken cancellationToken = default);
	
}