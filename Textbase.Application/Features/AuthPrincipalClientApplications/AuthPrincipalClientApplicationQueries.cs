/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using EM = Textbase.Infrastructure.Persistence.AuthPrincipalClientApplications;
using Textbase.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Uwn.Common.Querying;
using Uwn.EntityFrameworkCore.Infrastructure;
using Uwn.EntityFrameworkCore.Querying;

namespace Textbase.Application.Features.AuthPrincipalClientApplications;

public sealed partial class AuthPrincipalClientApplicationQueries(
	IDbContextFactory<TextbaseDbContext> dbContextFactory)
	: ModelQueries2<TextbaseDbContext, CM.AuthPrincipalClientApplicationDto, DM.AuthPrincipalClientApplication, EM.AuthPrincipalClientApplicationEntity, AuthPrincipalClientApplicationFilter, Guid, Guid>(dbContextFactory)
	, IAuthPrincipalClientApplicationQueries
	, ICanSort
{
	public override string DefaultSortKey => "entraobjectid,clientapplicationguid";

	protected override IQueryable<EM.AuthPrincipalClientApplicationEntity> ApplyFilter(
		IQueryable<EM.AuthPrincipalClientApplicationEntity> query,
		AuthPrincipalClientApplicationFilter filter)
		=> query
			.ApplyGuidFilter(e => e.EntraObjectId, filter.EntraObjectId)
			.ApplyGuidFilter(e => e.ClientApplicationGuid, filter.ClientApplicationGuid)
			;
			
}