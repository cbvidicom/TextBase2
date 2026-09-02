/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using Uwn.EntityFrameworkCore.Infrastructure;

namespace Textbase.Infrastructure.Persistence.AuthPrincipalLocales;

public sealed partial class AuthPrincipalLocaleEntityFactory
	: EntityFactory<CM.AuthPrincipalLocaleDto, DM.AuthPrincipalLocale, AuthPrincipalLocaleEntity>
	, IAuthPrincipalLocaleEntityFactory
{
	public override AuthPrincipalLocaleEntity Create() => Create(default!, default!);

	public AuthPrincipalLocaleEntity Create(Guid entraObjectId) => Create(entraObjectId, default!);
	public AuthPrincipalLocaleEntity Create(Guid entraObjectId, string localeKey)
	=> new() { EntraObjectId = entraObjectId, LocaleKey = localeKey };
}
