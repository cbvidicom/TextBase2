/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using Uwn.Common.Conversion;

namespace Textbase.Infrastructure.Persistence.AuthPrincipalLocales;

public static class AuthPrincipalLocaleMapper
{
	public static DM.AuthPrincipalLocale ToAuthPrincipalLocale(this CM.AuthPrincipalLocaleDto dto)
		=> ObjectMapper.MapTo<DM.AuthPrincipalLocale>(dto);

	public static AuthPrincipalLocaleEntity ToAuthPrincipalLocaleEntity(this CM.AuthPrincipalLocaleDto dto)
		=> ObjectMapper.MapTo<AuthPrincipalLocaleEntity>(dto);

	public static AuthPrincipalLocaleEntity ToAuthPrincipalLocaleEntity(this DM.AuthPrincipalLocale domain)
		=> ObjectMapper.MapTo<AuthPrincipalLocaleEntity>(domain);
}
