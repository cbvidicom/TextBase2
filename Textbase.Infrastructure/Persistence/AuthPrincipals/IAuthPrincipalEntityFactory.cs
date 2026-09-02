/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using Uwn.EntityFrameworkCore.Infrastructure;

namespace Textbase.Infrastructure.Persistence.AuthPrincipals;

public partial interface IAuthPrincipalEntityFactory
	: IEntityFactory<CM.AuthPrincipalDto, DM.AuthPrincipal, AuthPrincipalEntity>
{
	AuthPrincipalEntity Create(Guid entraObjectId);
	AuthPrincipalEntity Create(Guid entraObjectId, int role);
	AuthPrincipalEntity Create(Guid entraObjectId, int role, bool isActive);
}