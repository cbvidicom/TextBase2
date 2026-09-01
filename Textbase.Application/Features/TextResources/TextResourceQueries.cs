/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using EM = Textbase.Infrastructure.Persistence.TextResources;
using Textbase.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Uwn.Common.Querying;
using Uwn.EntityFrameworkCore.Infrastructure;
using Uwn.EntityFrameworkCore.Querying;

namespace Textbase.Application.Features.TextResources;

public sealed partial class TextResourceQueries(
	IDbContextFactory<TextbaseDbContext> dbContextFactory)
	: ModelQueries1<TextbaseDbContext, CM.TextResourceDto, DM.TextResource, EM.TextResourceEntity, TextResourceFilter, string>(dbContextFactory)
	, ITextResourceQueries
	, ICanSort
{
	public override string DefaultSortKey => "textkey";

	protected override IQueryable<EM.TextResourceEntity> ApplyFilter(
		IQueryable<EM.TextResourceEntity> query,
		TextResourceFilter filter)
		=> query
			.ApplyStringFilter(e => e.TextKey, filter.TextKey)
			.ApplyStringFilter(e => e.Description, filter.Description)
			;
			
}