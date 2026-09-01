/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using Uwn.EntityFrameworkCore.Infrastructure;

namespace Textbase.Application.Features.Presentations;

public partial interface IPresentationCommands
	: IModelCommands<CM.PresentationDto, DM.Presentation>
	, IModelCommands1<string>
{

}

public interface IPresentationServerCommands
	: IPresentationCommands
	, IModelCommandsKeyInfo<CM.PresentationDto>
{

}
