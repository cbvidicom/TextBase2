/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using DM = Textbase.Domain.Models;
using Uwn.EntityFrameworkCore.Infrastructure;

namespace Textbase.Application.Features.Formalities;

public partial interface IFormalityQueries
	: IModelQueries<DM.Formality, FormalityFilter>
	, IModelQueries1<DM.Formality, string>
{
}