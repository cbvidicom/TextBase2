/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using Uwn.EntityFrameworkCore.Infrastructure;

namespace Textbase.Infrastructure.Persistence.ClientApplicationLocales;

public partial interface IClientApplicationLocaleEntityFactory
	: IEntityFactory<CM.ClientApplicationLocaleDto, DM.ClientApplicationLocale, ClientApplicationLocaleEntity>
{
	ClientApplicationLocaleEntity Create(Guid clientApplicationGuid);
	ClientApplicationLocaleEntity Create(Guid clientApplicationGuid, string localeKey);
	ClientApplicationLocaleEntity Create(Guid clientApplicationGuid, string localeKey, bool isDefault);
}