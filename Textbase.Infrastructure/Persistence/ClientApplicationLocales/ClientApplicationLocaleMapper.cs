/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using Uwn.Common.Conversion;

namespace Textbase.Infrastructure.Persistence.ClientApplicationLocales;

public static class ClientApplicationLocaleMapper
{
	public static DM.ClientApplicationLocale ToClientApplicationLocale(this CM.ClientApplicationLocaleDto dto)
		=> ObjectMapper.MapTo<DM.ClientApplicationLocale>(dto);

	public static ClientApplicationLocaleEntity ToClientApplicationLocaleEntity(this CM.ClientApplicationLocaleDto dto)
		=> ObjectMapper.MapTo<ClientApplicationLocaleEntity>(dto);

	public static ClientApplicationLocaleEntity ToClientApplicationLocaleEntity(this DM.ClientApplicationLocale domain)
		=> ObjectMapper.MapTo<ClientApplicationLocaleEntity>(domain);
}
