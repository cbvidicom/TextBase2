/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using Uwn.EntityFrameworkCore.Infrastructure;

namespace Textbase.Application.Features.Locales;

public partial interface ILocaleCommands
	: IModelCommands<CM.LocaleDto, DM.Locale>
	, IModelCommands1<string>
{

}

public interface ILocaleServerCommands
	: ILocaleCommands
	, IModelCommandsKeyInfo<CM.LocaleDto>
{

}
