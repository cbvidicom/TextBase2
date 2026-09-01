/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using Uwn.EntityFrameworkCore.Infrastructure;

namespace Textbase.Application.Features.ClientApplicationTextResources;

public partial interface IClientApplicationTextResourceCommands
	: IModelCommands<CM.ClientApplicationTextResourceDto, DM.ClientApplicationTextResource>
	, IModelCommands2<Guid, string>
{

}

public interface IClientApplicationTextResourceServerCommands
	: IClientApplicationTextResourceCommands
	, IModelCommandsKeyInfo<CM.ClientApplicationTextResourceDto>
{

}
