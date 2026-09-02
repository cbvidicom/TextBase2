/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using Uwn.EntityFrameworkCore.Infrastructure;

namespace Textbase.Infrastructure.Persistence.AuthPrincipalClientApplications;

public partial interface IAuthPrincipalClientApplicationEntityFactory
	: IEntityFactory<CM.AuthPrincipalClientApplicationDto, DM.AuthPrincipalClientApplication, AuthPrincipalClientApplicationEntity>
{
	AuthPrincipalClientApplicationEntity Create(Guid entraObjectId);
	AuthPrincipalClientApplicationEntity Create(Guid entraObjectId, Guid clientApplicationGuid);
}