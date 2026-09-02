/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using DM = Textbase.Domain.Models;
using Textbase.Infrastructure.Persistence.ClientApplications;
using Textbase.Infrastructure.Persistence.AuthPrincipals;

namespace Textbase.Infrastructure.Persistence.AuthPrincipalClientApplications;

public partial class AuthPrincipalClientApplicationEntity
	: DM.AuthPrincipalClientApplication
{
	public virtual ClientApplicationEntity? ClientApplication { get; set; }
	public virtual AuthPrincipalEntity? AuthPrincipal { get; set; }
}
