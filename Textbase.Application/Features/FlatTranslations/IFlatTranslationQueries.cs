/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using DM = Textbase.Domain.Models;
using Uwn.EntityFrameworkCore.Infrastructure;

namespace Textbase.Application.Features.FlatTranslations;

public partial interface IFlatTranslationQueries
	: IModelQueries<DM.FlatTranslation, FlatTranslationFilter>
	, IModelQueries4<DM.FlatTranslation, string, string, string, string>
{
}