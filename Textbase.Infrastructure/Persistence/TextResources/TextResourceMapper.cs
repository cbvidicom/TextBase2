/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using Uwn.Common.Conversion;

namespace Textbase.Infrastructure.Persistence.TextResources;

public static class TextResourceMapper
{
	public static DM.TextResource ToTextResource(this CM.TextResourceDto dto)
		=> ObjectMapper.MapTo<DM.TextResource>(dto);

	public static TextResourceEntity ToTextResourceEntity(this CM.TextResourceDto dto)
		=> ObjectMapper.MapTo<TextResourceEntity>(dto);

	public static TextResourceEntity ToTextResourceEntity(this DM.TextResource domain)
		=> ObjectMapper.MapTo<TextResourceEntity>(domain);
}
