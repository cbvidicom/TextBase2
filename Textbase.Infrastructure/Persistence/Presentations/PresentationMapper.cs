/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using Uwn.Common.Conversion;

namespace Textbase.Infrastructure.Persistence.Presentations;

public static class PresentationMapper
{
	public static DM.Presentation ToPresentation(this CM.PresentationDto dto)
		=> ObjectMapper.MapTo<DM.Presentation>(dto);

	public static PresentationEntity ToPresentationEntity(this CM.PresentationDto dto)
		=> ObjectMapper.MapTo<PresentationEntity>(dto);

	public static PresentationEntity ToPresentationEntity(this DM.Presentation domain)
		=> ObjectMapper.MapTo<PresentationEntity>(domain);
}
