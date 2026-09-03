/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using System.Security.Claims;
using Textbase.Application.Features.TextResources;
using Textbase.Contracts.Models;

namespace Textbase.Host.Api.Authorization;

public sealed class DefaultTextResourceAuthorization
	: ITextResourceAuthorization
{
	public ValueTask<bool> CanCreateAsync(
		TextResourceDto dto,
		ClaimsPrincipal user,
		CancellationToken cancellationToken = default)
		=> ValueTask.FromResult(true);

	public ValueTask<bool> CanReadAsync(
		string textKey,
		ClaimsPrincipal user,
		CancellationToken cancellationToken = default)
		=> ValueTask.FromResult(true);

	public ValueTask<bool> CanCountAsync(
		TextResourceFilter filter,
		ClaimsPrincipal user,
		CancellationToken cancellationToken = default)
		=> ValueTask.FromResult(true);

	public ValueTask<bool> CanListAsync(
		TextResourceFilter filter,
		ClaimsPrincipal user,
		CancellationToken cancellationToken = default)
		=> ValueTask.FromResult(true);

	public ValueTask<bool> CanUpdateAsync(
		string textKey,
		TextResourceDto dto,
		ClaimsPrincipal user,
		CancellationToken cancellationToken = default)
		=> ValueTask.FromResult(true);

	public ValueTask<bool> CanDeleteAsync(
		string textKey,
		ClaimsPrincipal user,
		CancellationToken cancellationToken = default)
		=> ValueTask.FromResult(true);

	
}