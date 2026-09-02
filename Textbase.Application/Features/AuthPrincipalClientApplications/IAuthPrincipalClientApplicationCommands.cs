/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using Uwn.EntityFrameworkCore.Infrastructure;

namespace Textbase.Application.Features.AuthPrincipalClientApplications;

public partial interface IAuthPrincipalClientApplicationCommands
	: IModelCommands<CM.AuthPrincipalClientApplicationDto, DM.AuthPrincipalClientApplication>
	, IModelCommands2<Guid, Guid>
{

}

public interface IAuthPrincipalClientApplicationServerCommands
	: IAuthPrincipalClientApplicationCommands
	, IModelCommandsKeyInfo<CM.AuthPrincipalClientApplicationDto>
{

}
