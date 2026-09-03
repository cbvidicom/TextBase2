/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using System.Security.Claims;
using Textbase.Application.Features.Formalities;
using Textbase.Contracts.Models;

namespace Textbase.Host.Api.Authorization;

public interface IFormalityAuthorization
{
	ValueTask<bool> CanCreateAsync(FormalityDto dto, ClaimsPrincipal user, CancellationToken cancellationToken = default);
	ValueTask<bool> CanReadAsync(string formalityKey, ClaimsPrincipal user, CancellationToken cancellationToken = default);
	ValueTask<bool> CanCountAsync(FormalityFilter filter, ClaimsPrincipal user, CancellationToken cancellationToken = default);
	ValueTask<bool> CanListAsync(FormalityFilter filter, ClaimsPrincipal user, CancellationToken cancellationToken = default);
	ValueTask<bool> CanUpdateAsync(string formalityKey, FormalityDto dto, ClaimsPrincipal user, CancellationToken cancellationToken = default);
	ValueTask<bool> CanDeleteAsync(string formalityKey, ClaimsPrincipal user, CancellationToken cancellationToken = default);

	
}