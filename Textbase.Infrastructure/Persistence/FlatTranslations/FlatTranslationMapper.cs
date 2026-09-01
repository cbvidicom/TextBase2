/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using Uwn.Common.Conversion;

namespace Textbase.Infrastructure.Persistence.FlatTranslations;

public static class FlatTranslationMapper
{
	public static DM.FlatTranslation ToFlatTranslation(this CM.FlatTranslationDto dto)
		=> ObjectMapper.MapTo<DM.FlatTranslation>(dto);

	public static FlatTranslationEntity ToFlatTranslationEntity(this CM.FlatTranslationDto dto)
		=> ObjectMapper.MapTo<FlatTranslationEntity>(dto);

	public static FlatTranslationEntity ToFlatTranslationEntity(this DM.FlatTranslation domain)
		=> ObjectMapper.MapTo<FlatTranslationEntity>(domain);
}
