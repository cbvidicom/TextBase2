/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using Uwn.EntityFrameworkCore.Infrastructure;

namespace Textbase.Application.Features.Formalities;

public partial interface IFormalityCommands
	: IModelCommands<CM.FormalityDto, DM.Formality>
	, IModelCommands1<string>
{

}

public interface IFormalityServerCommands
	: IFormalityCommands
	, IModelCommandsKeyInfo<CM.FormalityDto>
{

}
