using System.Security.Claims;
using Textbase.Application.Features.Formalities;
using Textbase.Contracts.Models;
using Textbase.Domain.Enumerations;
using Textbase.Host.Authorization;

namespace Textbase.Host.Api.Authorization;

public sealed class FormalityAuthorization(AuthorizationScope _scope)
	: IFormalityAuthorization
{
	public ValueTask<bool> CanCreateAsync(FormalityDto dto, ClaimsPrincipal user, CancellationToken cancellationToken = default)
		=> IsSysAdminAsync(cancellationToken);

	public ValueTask<bool> CanReadAsync(string formalityKey, ClaimsPrincipal user, CancellationToken cancellationToken = default)
		=> ValueTask.FromResult(true);

	public ValueTask<bool> CanCountAsync(FormalityFilter filter, ClaimsPrincipal user, CancellationToken cancellationToken = default)
		=> ValueTask.FromResult(true);

	public ValueTask<bool> CanListAsync(FormalityFilter filter, ClaimsPrincipal user, CancellationToken cancellationToken = default)
		=> ValueTask.FromResult(true);

	public ValueTask<bool> CanUpdateAsync(string formalityKey, FormalityDto dto, ClaimsPrincipal user, CancellationToken cancellationToken = default)
		=> IsSysAdminAsync(cancellationToken);

	public ValueTask<bool> CanDeleteAsync(string formalityKey, ClaimsPrincipal user, CancellationToken cancellationToken = default)
		=> IsSysAdminAsync(cancellationToken);

	private async ValueTask<bool> IsSysAdminAsync(
		CancellationToken cancellationToken)
	{
		CurrentPrincipal? principal = await _scope.GetPrincipalAsync(cancellationToken);
		return principal is not null && principal.RolesValue.HasFlag(Roles.SysAdmin);
	}
}
