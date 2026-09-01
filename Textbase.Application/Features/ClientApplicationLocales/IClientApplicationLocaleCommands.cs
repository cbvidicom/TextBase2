/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using Uwn.EntityFrameworkCore.Infrastructure;

namespace Textbase.Application.Features.ClientApplicationLocales;

public partial interface IClientApplicationLocaleCommands
	: IModelCommands<CM.ClientApplicationLocaleDto, DM.ClientApplicationLocale>
	, IModelCommands2<Guid, string>
{

}

public interface IClientApplicationLocaleServerCommands
	: IClientApplicationLocaleCommands
	, IModelCommandsKeyInfo<CM.ClientApplicationLocaleDto>
{

}
