/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using DM = Textbase.Domain.Models;
using Uwn.EntityFrameworkCore.Infrastructure;

namespace Textbase.Application.Features.TextResources;

public partial interface ITextResourceQueries
	: IModelQueries<DM.TextResource, TextResourceFilter>
	, IModelQueries1<DM.TextResource, string>
{
}