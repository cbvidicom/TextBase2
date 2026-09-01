/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using EM = Textbase.Infrastructure.Persistence.ClientApplications;
using Textbase.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Uwn.Common.Querying;
using Uwn.EntityFrameworkCore.Infrastructure;
using Uwn.EntityFrameworkCore.Querying;

namespace Textbase.Application.Features.ClientApplications;

public sealed partial class ClientApplicationQueries(
	IDbContextFactory<TextbaseDbContext> dbContextFactory)
	: ModelQueries1<TextbaseDbContext, CM.ClientApplicationDto, DM.ClientApplication, EM.ClientApplicationEntity, ClientApplicationFilter, Guid>(dbContextFactory)
	, IClientApplicationQueries
	, ICanSort
{
	public override string DefaultSortKey => "clientapplicationguid";

	protected override IQueryable<EM.ClientApplicationEntity> ApplyFilter(
		IQueryable<EM.ClientApplicationEntity> query,
		ClientApplicationFilter filter)
		=> query
			.ApplyGuidFilter(e => e.ClientApplicationGuid, filter.ClientApplicationGuid)
			.ApplyStringFilter(e => e.Name, filter.Name)
			.ApplyStringFilter(e => e.Description, filter.Description)
			.ApplyStringFilter(e => e.DefaultLanguageTag, filter.DefaultLanguageTag)
			.ApplyStringFilter(e => e.DefaultFormat, filter.DefaultFormat)
			.ApplyStringFilter(e => e.DefaultFileName, filter.DefaultFileName)
			;
			
}