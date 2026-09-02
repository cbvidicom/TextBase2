/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using DM = Textbase.Domain.Models;
using Textbase.Infrastructure.Persistence.AuthPrincipalClientApplications;
using Textbase.Infrastructure.Persistence.ClientApplicationLocales;
using Textbase.Infrastructure.Persistence.ClientApplicationTextResources;

namespace Textbase.Infrastructure.Persistence.ClientApplications;

public partial class ClientApplicationEntity
	: DM.ClientApplication
{
	public virtual ICollection<AuthPrincipalClientApplicationEntity> AuthPrincipalClientApplications { get; set; } = [];
	public virtual ICollection<ClientApplicationLocaleEntity> ClientApplicationLocales { get; set; } = [];
	public virtual ICollection<ClientApplicationTextResourceEntity> ClientApplicationTextResources { get; set; } = [];
}
