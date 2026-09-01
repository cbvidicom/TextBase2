/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using EM = Textbase.Infrastructure.Persistence.ClientApplicationLocales;
using Textbase.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Uwn.Common.Querying;
using Uwn.EntityFrameworkCore.Infrastructure;
using Uwn.EntityFrameworkCore.Querying;

namespace Textbase.Application.Features.ClientApplicationLocales;

public sealed partial class ClientApplicationLocaleQueries(
	IDbContextFactory<TextbaseDbContext> dbContextFactory)
	: ModelQueries2<TextbaseDbContext, CM.ClientApplicationLocaleDto, DM.ClientApplicationLocale, EM.ClientApplicationLocaleEntity, ClientApplicationLocaleFilter, Guid, string>(dbContextFactory)
	, IClientApplicationLocaleQueries
	, ICanSort
{
	public override string DefaultSortKey => "clientapplicationguid,localekey";

	protected override IQueryable<EM.ClientApplicationLocaleEntity> ApplyFilter(
		IQueryable<EM.ClientApplicationLocaleEntity> query,
		ClientApplicationLocaleFilter filter)
		=> query
			.ApplyGuidFilter(e => e.ClientApplicationGuid, filter.ClientApplicationGuid)
			.ApplyStringFilter(e => e.LocaleKey, filter.LocaleKey)
			;
			
	public Task<DM.ClientApplicationLocale?> ReadByClientApplicationGuidAsync(
		Guid key,
		CancellationToken cancellationToken = default)
		=> ReadByUniqueIndexAsync(e => e.ClientApplicationGuid, key, cancellationToken);
	
}