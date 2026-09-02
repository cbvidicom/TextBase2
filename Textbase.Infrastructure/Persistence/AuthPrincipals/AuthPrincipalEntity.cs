/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using DM = Textbase.Domain.Models;
using Textbase.Infrastructure.Persistence.AuthPrincipalClientApplications;
using Textbase.Infrastructure.Persistence.AuthPrincipalLocales;

namespace Textbase.Infrastructure.Persistence.AuthPrincipals;

public partial class AuthPrincipalEntity
	: DM.AuthPrincipal
{
	public virtual ICollection<AuthPrincipalClientApplicationEntity> AuthPrincipalClientApplications { get; set; } = [];
	public virtual ICollection<AuthPrincipalLocaleEntity> AuthPrincipalLocales { get; set; } = [];
}
