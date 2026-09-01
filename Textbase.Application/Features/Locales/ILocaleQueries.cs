/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using DM = Textbase.Domain.Models;
using Uwn.EntityFrameworkCore.Infrastructure;

namespace Textbase.Application.Features.Locales;

public partial interface ILocaleQueries
	: IModelQueries<DM.Locale, LocaleFilter>
	, IModelQueries1<DM.Locale, string>
{
}