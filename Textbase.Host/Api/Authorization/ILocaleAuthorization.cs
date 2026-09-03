/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using System.Security.Claims;
using Textbase.Application.Features.Locales;
using Textbase.Contracts.Models;

namespace Textbase.Host.Api.Authorization;

public interface ILocaleAuthorization
{
	ValueTask<bool> CanCreateAsync(LocaleDto dto, ClaimsPrincipal user, CancellationToken cancellationToken = default);
	ValueTask<bool> CanReadAsync(string localeKey, ClaimsPrincipal user, CancellationToken cancellationToken = default);
	ValueTask<bool> CanCountAsync(LocaleFilter filter, ClaimsPrincipal user, CancellationToken cancellationToken = default);
	ValueTask<bool> CanListAsync(LocaleFilter filter, ClaimsPrincipal user, CancellationToken cancellationToken = default);
	ValueTask<bool> CanUpdateAsync(string localeKey, LocaleDto dto, ClaimsPrincipal user, CancellationToken cancellationToken = default);
	ValueTask<bool> CanDeleteAsync(string localeKey, ClaimsPrincipal user, CancellationToken cancellationToken = default);

	
}