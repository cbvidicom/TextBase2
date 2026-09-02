/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using Uwn.EntityFrameworkCore.Infrastructure;

namespace Textbase.Infrastructure.Persistence.AuthPrincipals;

public sealed partial class AuthPrincipalEntityFactory
	: EntityFactory<CM.AuthPrincipalDto, DM.AuthPrincipal, AuthPrincipalEntity>
	, IAuthPrincipalEntityFactory
{
	public override AuthPrincipalEntity Create() => Create(default!, default!, default!);

	public AuthPrincipalEntity Create(Guid entraObjectId) => Create(entraObjectId, default!, default!);
	public AuthPrincipalEntity Create(Guid entraObjectId, int role) => Create(entraObjectId, role, default!);
	public AuthPrincipalEntity Create(Guid entraObjectId, int role, bool isActive)
	=> new() { EntraObjectId = entraObjectId, Role = role, IsActive = isActive };
}
