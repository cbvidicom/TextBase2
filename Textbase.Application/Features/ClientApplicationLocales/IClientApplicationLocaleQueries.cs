/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using DM = Textbase.Domain.Models;
using Uwn.EntityFrameworkCore.Infrastructure;

namespace Textbase.Application.Features.ClientApplicationLocales;

public partial interface IClientApplicationLocaleQueries
	: IModelQueries<DM.ClientApplicationLocale, ClientApplicationLocaleFilter>
	, IModelQueries2<DM.ClientApplicationLocale, Guid, string>
{
	Task<DM.ClientApplicationLocale?> ReadByClientApplicationGuidAsync(Guid key, CancellationToken cancellationToken = default);
}