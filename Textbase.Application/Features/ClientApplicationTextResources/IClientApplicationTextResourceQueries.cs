/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using DM = Textbase.Domain.Models;
using Uwn.EntityFrameworkCore.Infrastructure;

namespace Textbase.Application.Features.ClientApplicationTextResources;

public partial interface IClientApplicationTextResourceQueries
	: IModelQueries<DM.ClientApplicationTextResource, ClientApplicationTextResourceFilter>
	, IModelQueries2<DM.ClientApplicationTextResource, Guid, string>
{
}