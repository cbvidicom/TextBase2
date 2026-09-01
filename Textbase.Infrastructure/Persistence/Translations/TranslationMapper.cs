/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using Uwn.Common.Conversion;

namespace Textbase.Infrastructure.Persistence.Translations;

public static class TranslationMapper
{
	public static DM.Translation ToTranslation(this CM.TranslationDto dto)
		=> ObjectMapper.MapTo<DM.Translation>(dto);

	public static TranslationEntity ToTranslationEntity(this CM.TranslationDto dto)
		=> ObjectMapper.MapTo<TranslationEntity>(dto);

	public static TranslationEntity ToTranslationEntity(this DM.Translation domain)
		=> ObjectMapper.MapTo<TranslationEntity>(domain);
}
