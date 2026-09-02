/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using Uwn.EntityFrameworkCore.Infrastructure;

namespace Textbase.Application.Features.AuthPrincipals;

public partial interface IAuthPrincipalCommands
	: IModelCommands<CM.AuthPrincipalDto, DM.AuthPrincipal>
	, IModelCommands1<Guid>
{

}

public interface IAuthPrincipalServerCommands
	: IAuthPrincipalCommands
	, IModelCommandsKeyInfo<CM.AuthPrincipalDto>
{

}
