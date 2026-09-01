/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using Uwn.EntityFrameworkCore.Infrastructure;

namespace Textbase.Application.Features.TextResources;

public partial interface ITextResourceCommands
	: IModelCommands<CM.TextResourceDto, DM.TextResource>
	, IModelCommands1<string>
{

}

public interface ITextResourceServerCommands
	: ITextResourceCommands
	, IModelCommandsKeyInfo<CM.TextResourceDto>
{

}
