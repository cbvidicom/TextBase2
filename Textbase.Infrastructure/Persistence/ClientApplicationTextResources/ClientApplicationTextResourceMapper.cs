/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using Uwn.Common.Conversion;

namespace Textbase.Infrastructure.Persistence.ClientApplicationTextResources;

public static class ClientApplicationTextResourceMapper
{
	public static DM.ClientApplicationTextResource ToClientApplicationTextResource(this CM.ClientApplicationTextResourceDto dto)
		=> ObjectMapper.MapTo<DM.ClientApplicationTextResource>(dto);

	public static ClientApplicationTextResourceEntity ToClientApplicationTextResourceEntity(this CM.ClientApplicationTextResourceDto dto)
		=> ObjectMapper.MapTo<ClientApplicationTextResourceEntity>(dto);

	public static ClientApplicationTextResourceEntity ToClientApplicationTextResourceEntity(this DM.ClientApplicationTextResource domain)
		=> ObjectMapper.MapTo<ClientApplicationTextResourceEntity>(domain);
}
