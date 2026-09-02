/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using Uwn.EntityFrameworkCore.Infrastructure;

namespace Textbase.Infrastructure.Persistence.AuthPrincipalLocales;

public partial interface IAuthPrincipalLocaleEntityFactory
	: IEntityFactory<CM.AuthPrincipalLocaleDto, DM.AuthPrincipalLocale, AuthPrincipalLocaleEntity>
{
	AuthPrincipalLocaleEntity Create(Guid entraObjectId);
	AuthPrincipalLocaleEntity Create(Guid entraObjectId, string localeKey);
}