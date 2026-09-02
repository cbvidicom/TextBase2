/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using DM = Textbase.Domain.Models;
using Uwn.EntityFrameworkCore.Infrastructure;

namespace Textbase.Application.Features.AuthPrincipalClientApplications;

public partial interface IAuthPrincipalClientApplicationQueries
	: IModelQueries<DM.AuthPrincipalClientApplication, AuthPrincipalClientApplicationFilter>
	, IModelQueries2<DM.AuthPrincipalClientApplication, Guid, Guid>
{
}