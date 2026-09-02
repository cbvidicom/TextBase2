/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using EM = Textbase.Infrastructure.Persistence.AuthPrincipals;
using Textbase.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Uwn.Common.Querying;
using Uwn.EntityFrameworkCore.Infrastructure;
using Uwn.EntityFrameworkCore.Querying;

namespace Textbase.Application.Features.AuthPrincipals;

public sealed partial class AuthPrincipalQueries(
	IDbContextFactory<TextbaseDbContext> dbContextFactory)
	: ModelQueries1<TextbaseDbContext, CM.AuthPrincipalDto, DM.AuthPrincipal, EM.AuthPrincipalEntity, AuthPrincipalFilter, Guid>(dbContextFactory)
	, IAuthPrincipalQueries
	, ICanSort
{
	public override string DefaultSortKey => "entraobjectid";

	protected override IQueryable<EM.AuthPrincipalEntity> ApplyFilter(
		IQueryable<EM.AuthPrincipalEntity> query,
		AuthPrincipalFilter filter)
		=> query
			.ApplyGuidFilter(e => e.EntraObjectId, filter.EntraObjectId)
			.ApplyNumericFilter(e => e.Role, filter.Role)
			.ApplyStringFilter(e => e.DisplayName, filter.DisplayName)
			.ApplyStringFilter(e => e.EmailAddress, filter.EmailAddress)
			;
			
}