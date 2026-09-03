using System.Security.Claims;
using Textbase.Application.Features.Presentations;
using Textbase.Contracts.Models;

namespace Textbase.Host.Api.Authorization;

public sealed class PresentationAuthorization(
	AuthorizationScope scope)
	: AuthorizationBase(scope)
	, IPresentationAuthorization
{
	public ValueTask<bool> CanCreateAsync(
		PresentationDto dto,
		ClaimsPrincipal user,
		CancellationToken cancellationToken = default)
		=> IsSysAdminAsync(cancellationToken);

	public ValueTask<bool> CanReadAsync(
		string presentationKey,
		ClaimsPrincipal user,
		CancellationToken cancellationToken = default)
		=> ValueTask.FromResult(true);

	public ValueTask<bool> CanCountAsync(
		PresentationFilter filter,
		ClaimsPrincipal user,
		CancellationToken cancellationToken = default)
		=> ValueTask.FromResult(true);

	public ValueTask<bool> CanListAsync(
		PresentationFilter filter,
		ClaimsPrincipal user,
		CancellationToken cancellationToken = default)
		=> ValueTask.FromResult(true);

	public ValueTask<bool> CanUpdateAsync(
		string presentationKey,
		PresentationDto dto,
		ClaimsPrincipal user,
		CancellationToken cancellationToken = default)
		=> IsSysAdminAsync(cancellationToken);

	public ValueTask<bool> CanDeleteAsync(
		string presentationKey,
		ClaimsPrincipal user,
		CancellationToken cancellationToken = default)
		=> IsSysAdminAsync(cancellationToken);
}
