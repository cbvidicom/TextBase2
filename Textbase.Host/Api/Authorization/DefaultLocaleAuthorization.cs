/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using System.Security.Claims;
using Textbase.Application.Features.Locales;
using Textbase.Contracts.Models;

namespace Textbase.Host.Api.Authorization;

public sealed class DefaultLocaleAuthorization
	: ILocaleAuthorization
{
	public ValueTask<bool> CanCreateAsync(
		LocaleDto dto,
		ClaimsPrincipal user,
		CancellationToken cancellationToken = default)
		=> ValueTask.FromResult(true);

	public ValueTask<bool> CanReadAsync(
		string localeKey,
		ClaimsPrincipal user,
		CancellationToken cancellationToken = default)
		=> ValueTask.FromResult(true);

	public ValueTask<bool> CanCountAsync(
		LocaleFilter filter,
		ClaimsPrincipal user,
		CancellationToken cancellationToken = default)
		=> ValueTask.FromResult(true);

	public ValueTask<bool> CanListAsync(
		LocaleFilter filter,
		ClaimsPrincipal user,
		CancellationToken cancellationToken = default)
		=> ValueTask.FromResult(true);

	public ValueTask<bool> CanUpdateAsync(
		string localeKey,
		LocaleDto dto,
		ClaimsPrincipal user,
		CancellationToken cancellationToken = default)
		=> ValueTask.FromResult(true);

	public ValueTask<bool> CanDeleteAsync(
		string localeKey,
		ClaimsPrincipal user,
		CancellationToken cancellationToken = default)
		=> ValueTask.FromResult(true);

	
}