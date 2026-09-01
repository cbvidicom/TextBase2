/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using Uwn.EntityFrameworkCore.Infrastructure;

namespace Textbase.Infrastructure.Persistence.ClientApplications;

public sealed partial class ClientApplicationEntityFactory
	: EntityFactory<CM.ClientApplicationDto, DM.ClientApplication, ClientApplicationEntity>
	, IClientApplicationEntityFactory
{
	public override ClientApplicationEntity Create() => Create(default!, default!, default!);

	public ClientApplicationEntity Create(Guid clientApplicationGuid) => Create(clientApplicationGuid, default!, default!);
	public ClientApplicationEntity Create(Guid clientApplicationGuid, string name) => Create(clientApplicationGuid, name, default!);
	public ClientApplicationEntity Create(Guid clientApplicationGuid, string name, bool isActive)
	=> new() { ClientApplicationGuid = clientApplicationGuid, Name = name, IsActive = isActive };
}
