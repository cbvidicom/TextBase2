/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using Uwn.EntityFrameworkCore.Infrastructure;

namespace Textbase.Application.Features.ClientApplications;

public partial interface IClientApplicationCommands
	: IModelCommands<CM.ClientApplicationDto, DM.ClientApplication>
	, IModelCommands1<Guid>
{

}

public interface IClientApplicationServerCommands
	: IClientApplicationCommands
	, IModelCommandsKeyInfo<CM.ClientApplicationDto>
{

}
