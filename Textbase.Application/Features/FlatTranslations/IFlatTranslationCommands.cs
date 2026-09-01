/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using Uwn.EntityFrameworkCore.Infrastructure;

namespace Textbase.Application.Features.FlatTranslations;

public partial interface IFlatTranslationCommands
	: IModelCommands<CM.FlatTranslationDto, DM.FlatTranslation>
	, IModelCommands4<string, string, string, string>
{

}

public interface IFlatTranslationServerCommands
	: IFlatTranslationCommands
	, IModelCommandsKeyInfo<CM.FlatTranslationDto>
{

}
