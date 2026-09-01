/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using DM = Textbase.Domain.Models;
using Uwn.EntityFrameworkCore.Infrastructure;

namespace Textbase.Application.Features.Translations;

public partial interface ITranslationQueries
	: IModelQueries<DM.Translation, TranslationFilter>
	, IModelQueries4<DM.Translation, string, string, string, string>
{
}