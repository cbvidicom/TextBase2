/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using System.Security.Claims;
using Textbase.Application.Features.Presentations;
using Textbase.Contracts.Models;

namespace Textbase.Host.Api.Authorization;

public sealed class DefaultPresentationAuthorization
	: IPresentationAuthorization
{
	public ValueTask<bool> CanCreateAsync(
		PresentationDto dto,
		ClaimsPrincipal user,
		CancellationToken cancellationToken = default)
		=> ValueTask.FromResult(true);

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
		=> ValueTask.FromResult(true);

	public ValueTask<bool> CanDeleteAsync(
		string presentationKey,
		ClaimsPrincipal user,
		CancellationToken cancellationToken = default)
		=> ValueTask.FromResult(true);

	
}