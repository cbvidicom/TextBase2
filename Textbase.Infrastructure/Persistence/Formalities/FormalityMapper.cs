/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using Uwn.Common.Conversion;

namespace Textbase.Infrastructure.Persistence.Formalities;

public static class FormalityMapper
{
	public static DM.Formality ToFormality(this CM.FormalityDto dto)
		=> ObjectMapper.MapTo<DM.Formality>(dto);

	public static FormalityEntity ToFormalityEntity(this CM.FormalityDto dto)
		=> ObjectMapper.MapTo<FormalityEntity>(dto);

	public static FormalityEntity ToFormalityEntity(this DM.Formality domain)
		=> ObjectMapper.MapTo<FormalityEntity>(domain);
}
