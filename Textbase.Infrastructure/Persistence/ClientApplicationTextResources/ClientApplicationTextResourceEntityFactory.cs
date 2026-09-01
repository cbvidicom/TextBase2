/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using Uwn.EntityFrameworkCore.Infrastructure;

namespace Textbase.Infrastructure.Persistence.ClientApplicationTextResources;

public sealed partial class ClientApplicationTextResourceEntityFactory
	: EntityFactory<CM.ClientApplicationTextResourceDto, DM.ClientApplicationTextResource, ClientApplicationTextResourceEntity>
	, IClientApplicationTextResourceEntityFactory
{
	public override ClientApplicationTextResourceEntity Create() => Create(default!, default!);

	public ClientApplicationTextResourceEntity Create(Guid clientApplicationGuid) => Create(clientApplicationGuid, default!);
	public ClientApplicationTextResourceEntity Create(Guid clientApplicationGuid, string textKey)
	=> new() { ClientApplicationGuid = clientApplicationGuid, TextKey = textKey };
}
