/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using EM = Textbase.Infrastructure.Persistence.ClientApplicationTextResources;
using Textbase.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Uwn.Common.Querying;
using Uwn.EntityFrameworkCore.Infrastructure;
using Uwn.EntityFrameworkCore.Querying;

namespace Textbase.Application.Features.ClientApplicationTextResources;

public sealed partial class ClientApplicationTextResourceQueries(
	IDbContextFactory<TextbaseDbContext> dbContextFactory)
	: ModelQueries2<TextbaseDbContext, CM.ClientApplicationTextResourceDto, DM.ClientApplicationTextResource, EM.ClientApplicationTextResourceEntity, ClientApplicationTextResourceFilter, Guid, string>(dbContextFactory)
	, IClientApplicationTextResourceQueries
	, ICanSort
{
	public override string DefaultSortKey => "clientapplicationguid,textkey";

	protected override IQueryable<EM.ClientApplicationTextResourceEntity> ApplyFilter(
		IQueryable<EM.ClientApplicationTextResourceEntity> query,
		ClientApplicationTextResourceFilter filter)
		=> query
			.ApplyGuidFilter(e => e.ClientApplicationGuid, filter.ClientApplicationGuid)
			.ApplyStringFilter(e => e.TextKey, filter.TextKey)
			.ApplyStringFilter(e => e.ReferenceId, filter.ReferenceId)
			;
			
}