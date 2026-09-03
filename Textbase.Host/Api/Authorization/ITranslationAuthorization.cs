/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using System.Security.Claims;
using Textbase.Application.Features.Translations;
using Textbase.Contracts.Models;

namespace Textbase.Host.Api.Authorization;

public interface ITranslationAuthorization
{
	ValueTask<bool> CanCreateAsync(TranslationDto dto, ClaimsPrincipal user, CancellationToken cancellationToken = default);
	ValueTask<bool> CanReadAsync(string localeKey, string textKey, string formalityKey, string presentationKey, ClaimsPrincipal user, CancellationToken cancellationToken = default);
	ValueTask<bool> CanCountAsync(TranslationFilter filter, ClaimsPrincipal user, CancellationToken cancellationToken = default);
	ValueTask<bool> CanListAsync(TranslationFilter filter, ClaimsPrincipal user, CancellationToken cancellationToken = default);
	ValueTask<bool> CanUpdateAsync(string localeKey, string textKey, string formalityKey, string presentationKey, TranslationDto dto, ClaimsPrincipal user, CancellationToken cancellationToken = default);
	ValueTask<bool> CanDeleteAsync(string localeKey, string textKey, string formalityKey, string presentationKey, ClaimsPrincipal user, CancellationToken cancellationToken = default);

	
}