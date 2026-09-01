/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using EM = Textbase.Infrastructure.Persistence.Formalities;
using Textbase.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Uwn.Common.Querying;
using Uwn.EntityFrameworkCore.Infrastructure;
using Uwn.EntityFrameworkCore.Querying;

namespace Textbase.Application.Features.Formalities;

public sealed partial class FormalityQueries(
	IDbContextFactory<TextbaseDbContext> dbContextFactory)
	: ModelQueries1<TextbaseDbContext, CM.FormalityDto, DM.Formality, EM.FormalityEntity, FormalityFilter, string>(dbContextFactory)
	, IFormalityQueries
	, ICanSort
{
	public override string DefaultSortKey => "formalitykey";

	protected override IQueryable<EM.FormalityEntity> ApplyFilter(
		IQueryable<EM.FormalityEntity> query,
		FormalityFilter filter)
		=> query
			.ApplyStringFilter(e => e.FormalityKey, filter.FormalityKey)
			.ApplyStringFilter(e => e.Description, filter.Description)
			;
			
}