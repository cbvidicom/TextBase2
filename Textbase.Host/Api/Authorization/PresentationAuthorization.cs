using System.Security.Claims;
using Textbase.Application.Features.Presentations;
using Textbase.Contracts.Models;
using Textbase.Domain.Enumerations;
using Textbase.Host.Authorization;

namespace Textbase.Host.Api.Authorization;

public sealed class PresentationAuthorization(AuthorizationScope _scope)
	: IPresentationAuthorization
{
	public ValueTask<bool> CanCreateAsync(PresentationDto dto, ClaimsPrincipal user, CancellationToken cancellationToken = default)
		=> IsSysAdminAsync(cancellationToken);

	public ValueTask<bool> CanReadAsync(string presentationKey, ClaimsPrincipal user, CancellationToken cancellationToken = default)
		=> ValueTask.FromResult(true);

	public ValueTask<bool> CanCountAsync(PresentationFilter filter, ClaimsPrincipal user, CancellationToken cancellationToken = default)
		=> ValueTask.FromResult(true);

	public ValueTask<bool> CanListAsync(PresentationFilter filter, ClaimsPrincipal user, CancellationToken cancellationToken = default)
		=> ValueTask.FromResult(true);

	public ValueTask<bool> CanUpdateAsync(string presentationKey, PresentationDto dto, ClaimsPrincipal user, CancellationToken cancellationToken = default)
		=> IsSysAdminAsync(cancellationToken);

	public ValueTask<bool> CanDeleteAsync(string presentationKey, ClaimsPrincipal user, CancellationToken cancellationToken = default)
		=> IsSysAdminAsync(cancellationToken);

	private async ValueTask<bool> IsSysAdminAsync(
		CancellationToken cancellationToken)
	{
		CurrentPrincipal? principal = await _scope.GetPrincipalAsync(cancellationToken);
		return principal is not null && principal.RolesValue.HasFlag(Roles.SysAdmin);
	}
}
