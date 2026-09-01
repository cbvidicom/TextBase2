/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using Uwn.EntityFrameworkCore.Infrastructure;

namespace Textbase.Application.Features.Translations;

public partial interface ITranslationCommands
	: IModelCommands<CM.TranslationDto, DM.Translation>
	, IModelCommands4<string, string, string, string>
{

}

public interface ITranslationServerCommands
	: ITranslationCommands
	, IModelCommandsKeyInfo<CM.TranslationDto>
{

}
