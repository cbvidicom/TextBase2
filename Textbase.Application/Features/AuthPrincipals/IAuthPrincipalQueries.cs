/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using DM = Textbase.Domain.Models;
using Uwn.EntityFrameworkCore.Infrastructure;

namespace Textbase.Application.Features.AuthPrincipals;

public partial interface IAuthPrincipalQueries
	: IModelQueries<DM.AuthPrincipal, AuthPrincipalFilter>
	, IModelQueries1<DM.AuthPrincipal, Guid>
{
}