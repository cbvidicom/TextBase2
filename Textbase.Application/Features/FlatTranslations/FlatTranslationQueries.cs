/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using EM = Textbase.Infrastructure.Persistence.FlatTranslations;
using Textbase.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Uwn.Common.Querying;
using Uwn.EntityFrameworkCore.Infrastructure;
using Uwn.EntityFrameworkCore.Querying;

namespace Textbase.Application.Features.FlatTranslations;

public sealed partial class FlatTranslationQueries(
	IDbContextFactory<TextbaseDbContext> dbContextFactory)
	: ModelQueries4<TextbaseDbContext, CM.FlatTranslationDto, DM.FlatTranslation, EM.FlatTranslationEntity, FlatTranslationFilter, string, string, string, string>(dbContextFactory)
	, IFlatTranslationQueries
	, ICanSort
{
	public override string DefaultSortKey => "localekey,textkey,formalitykey,presentationkey";

	protected override IQueryable<EM.FlatTranslationEntity> ApplyFilter(
		IQueryable<EM.FlatTranslationEntity> query,
		FlatTranslationFilter filter)
		=> query
			.ApplyStringFilter(e => e.LocaleKey, filter.LocaleKey)
			.ApplyStringFilter(e => e.SourceLocaleKey, filter.SourceLocaleKey)
			.ApplyStringFilter(e => e.TextKey, filter.TextKey)
			.ApplyStringFilter(e => e.FormalityKey, filter.FormalityKey)
			.ApplyStringFilter(e => e.PresentationKey, filter.PresentationKey)
			.ApplyStringFilter(e => e.Value, filter.Value)
			;
			
}