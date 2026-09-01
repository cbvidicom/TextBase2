/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using Uwn.Common.Conversion;

namespace Textbase.Infrastructure.Persistence.Locales;

public static class LocaleMapper
{
	public static DM.Locale ToLocale(this CM.LocaleDto dto)
		=> ObjectMapper.MapTo<DM.Locale>(dto);

	public static LocaleEntity ToLocaleEntity(this CM.LocaleDto dto)
		=> ObjectMapper.MapTo<LocaleEntity>(dto);

	public static LocaleEntity ToLocaleEntity(this DM.Locale domain)
		=> ObjectMapper.MapTo<LocaleEntity>(domain);
}
