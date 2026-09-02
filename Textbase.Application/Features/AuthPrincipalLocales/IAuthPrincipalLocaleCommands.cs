/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using Uwn.EntityFrameworkCore.Infrastructure;

namespace Textbase.Application.Features.AuthPrincipalLocales;

public partial interface IAuthPrincipalLocaleCommands
	: IModelCommands<CM.AuthPrincipalLocaleDto, DM.AuthPrincipalLocale>
	, IModelCommands2<Guid, string>
{

}

public interface IAuthPrincipalLocaleServerCommands
	: IAuthPrincipalLocaleCommands
	, IModelCommandsKeyInfo<CM.AuthPrincipalLocaleDto>
{

}
