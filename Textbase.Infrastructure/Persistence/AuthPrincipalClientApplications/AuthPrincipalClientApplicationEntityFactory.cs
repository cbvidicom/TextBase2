/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using Uwn.EntityFrameworkCore.Infrastructure;

namespace Textbase.Infrastructure.Persistence.AuthPrincipalClientApplications;

public sealed partial class AuthPrincipalClientApplicationEntityFactory
	: EntityFactory<CM.AuthPrincipalClientApplicationDto, DM.AuthPrincipalClientApplication, AuthPrincipalClientApplicationEntity>
	, IAuthPrincipalClientApplicationEntityFactory
{
	public override AuthPrincipalClientApplicationEntity Create() => Create(default!, default!);

	public AuthPrincipalClientApplicationEntity Create(Guid entraObjectId) => Create(entraObjectId, default!);
	public AuthPrincipalClientApplicationEntity Create(Guid entraObjectId, Guid clientApplicationGuid)
	=> new() { EntraObjectId = entraObjectId, ClientApplicationGuid = clientApplicationGuid };
}
