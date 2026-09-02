/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using Uwn.Common.Conversion;

namespace Textbase.Infrastructure.Persistence.AuthPrincipals;

public static class AuthPrincipalMapper
{
	public static DM.AuthPrincipal ToAuthPrincipal(this CM.AuthPrincipalDto dto)
		=> ObjectMapper.MapTo<DM.AuthPrincipal>(dto);

	public static AuthPrincipalEntity ToAuthPrincipalEntity(this CM.AuthPrincipalDto dto)
		=> ObjectMapper.MapTo<AuthPrincipalEntity>(dto);

	public static AuthPrincipalEntity ToAuthPrincipalEntity(this DM.AuthPrincipal domain)
		=> ObjectMapper.MapTo<AuthPrincipalEntity>(domain);
}
