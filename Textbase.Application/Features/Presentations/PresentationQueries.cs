/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using EM = Textbase.Infrastructure.Persistence.Presentations;
using Textbase.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Uwn.Common.Querying;
using Uwn.EntityFrameworkCore.Infrastructure;
using Uwn.EntityFrameworkCore.Querying;

namespace Textbase.Application.Features.Presentations;

public sealed partial class PresentationQueries(
	IDbContextFactory<TextbaseDbContext> dbContextFactory)
	: ModelQueries1<TextbaseDbContext, CM.PresentationDto, DM.Presentation, EM.PresentationEntity, PresentationFilter, string>(dbContextFactory)
	, IPresentationQueries
	, ICanSort
{
	public override string DefaultSortKey => "presentationkey";

	protected override IQueryable<EM.PresentationEntity> ApplyFilter(
		IQueryable<EM.PresentationEntity> query,
		PresentationFilter filter)
		=> query
			.ApplyStringFilter(e => e.PresentationKey, filter.PresentationKey)
			.ApplyStringFilter(e => e.Description, filter.Description)
			;
			
}