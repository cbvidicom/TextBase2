/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using Uwn.Common.Conversion;

namespace Textbase.Infrastructure.Persistence.ClientApplications;

public static class ClientApplicationMapper
{
	public static DM.ClientApplication ToClientApplication(this CM.ClientApplicationDto dto)
		=> ObjectMapper.MapTo<DM.ClientApplication>(dto);

	public static ClientApplicationEntity ToClientApplicationEntity(this CM.ClientApplicationDto dto)
		=> ObjectMapper.MapTo<ClientApplicationEntity>(dto);

	public static ClientApplicationEntity ToClientApplicationEntity(this DM.ClientApplication domain)
		=> ObjectMapper.MapTo<ClientApplicationEntity>(domain);
}
