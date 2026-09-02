/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using Uwn.Common.Conversion;

namespace Textbase.Infrastructure.Persistence.AuthPrincipalClientApplications;

public static class AuthPrincipalClientApplicationMapper
{
	public static DM.AuthPrincipalClientApplication ToAuthPrincipalClientApplication(this CM.AuthPrincipalClientApplicationDto dto)
		=> ObjectMapper.MapTo<DM.AuthPrincipalClientApplication>(dto);

	public static AuthPrincipalClientApplicationEntity ToAuthPrincipalClientApplicationEntity(this CM.AuthPrincipalClientApplicationDto dto)
		=> ObjectMapper.MapTo<AuthPrincipalClientApplicationEntity>(dto);

	public static AuthPrincipalClientApplicationEntity ToAuthPrincipalClientApplicationEntity(this DM.AuthPrincipalClientApplication domain)
		=> ObjectMapper.MapTo<AuthPrincipalClientApplicationEntity>(domain);
}
