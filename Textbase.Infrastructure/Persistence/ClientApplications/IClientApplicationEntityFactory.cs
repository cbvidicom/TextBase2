/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using Uwn.EntityFrameworkCore.Infrastructure;

namespace Textbase.Infrastructure.Persistence.ClientApplications;

public partial interface IClientApplicationEntityFactory
	: IEntityFactory<CM.ClientApplicationDto, DM.ClientApplication, ClientApplicationEntity>
{
	ClientApplicationEntity Create(Guid clientApplicationGuid);
	ClientApplicationEntity Create(Guid clientApplicationGuid, string name);
	ClientApplicationEntity Create(Guid clientApplicationGuid, string name, bool isActive);
}