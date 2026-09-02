/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using EM = Textbase.Infrastructure.Persistence.AuthPrincipalLocales;
using Textbase.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Uwn.Common.Querying;
using Uwn.EntityFrameworkCore.Infrastructure;
using Uwn.EntityFrameworkCore.Querying;

namespace Textbase.Application.Features.AuthPrincipalLocales;

public sealed partial class AuthPrincipalLocaleQueries(
	IDbContextFactory<TextbaseDbContext> dbContextFactory)
	: ModelQueries2<TextbaseDbContext, CM.AuthPrincipalLocaleDto, DM.AuthPrincipalLocale, EM.AuthPrincipalLocaleEntity, AuthPrincipalLocaleFilter, Guid, string>(dbContextFactory)
	, IAuthPrincipalLocaleQueries
	, ICanSort
{
	public override string DefaultSortKey => "entraobjectid,localekey";

	protected override IQueryable<EM.AuthPrincipalLocaleEntity> ApplyFilter(
		IQueryable<EM.AuthPrincipalLocaleEntity> query,
		AuthPrincipalLocaleFilter filter)
		=> query
			.ApplyGuidFilter(e => e.EntraObjectId, filter.EntraObjectId)
			.ApplyStringFilter(e => e.LocaleKey, filter.LocaleKey)
			;
			
}