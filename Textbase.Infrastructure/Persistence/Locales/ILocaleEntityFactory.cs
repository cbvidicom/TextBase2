/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using Uwn.EntityFrameworkCore.Infrastructure;

namespace Textbase.Infrastructure.Persistence.Locales;

public partial interface ILocaleEntityFactory
	: IEntityFactory<CM.LocaleDto, DM.Locale, LocaleEntity>
{
	LocaleEntity Create(string localeKey);
	LocaleEntity Create(string localeKey, string nativeName);
	LocaleEntity Create(string localeKey, string nativeName, string englishName);
}