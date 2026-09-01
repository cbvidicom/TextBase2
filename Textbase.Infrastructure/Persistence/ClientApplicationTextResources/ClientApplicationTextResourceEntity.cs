/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using DM = Textbase.Domain.Models;
using Textbase.Infrastructure.Persistence.ClientApplications;
using Textbase.Infrastructure.Persistence.TextResources;

namespace Textbase.Infrastructure.Persistence.ClientApplicationTextResources;

public partial class ClientApplicationTextResourceEntity
	: DM.ClientApplicationTextResource
{
	public virtual ClientApplicationEntity? ClientApplication { get; set; }
	public virtual TextResourceEntity? TextResource { get; set; }
}
