/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using EM = Textbase.Infrastructure.Persistence.Translations;
using Textbase.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Uwn.Common.Querying;
using Uwn.EntityFrameworkCore.Infrastructure;
using Uwn.EntityFrameworkCore.Querying;

namespace Textbase.Application.Features.Translations;

public sealed partial class TranslationQueries(
	IDbContextFactory<TextbaseDbContext> dbContextFactory)
	: ModelQueries4<TextbaseDbContext, CM.TranslationDto, DM.Translation, EM.TranslationEntity, TranslationFilter, string, string, string, string>(dbContextFactory)
	, ITranslationQueries
	, ICanSort
{
	public override string DefaultSortKey => "localekey,textkey,formalitykey,presentationkey";

	protected override IQueryable<EM.TranslationEntity> ApplyFilter(
		IQueryable<EM.TranslationEntity> query,
		TranslationFilter filter)
		=> query
			.ApplyStringFilter(e => e.LocaleKey, filter.LocaleKey)
			.ApplyStringFilter(e => e.TextKey, filter.TextKey)
			.ApplyStringFilter(e => e.FormalityKey, filter.FormalityKey)
			.ApplyStringFilter(e => e.PresentationKey, filter.PresentationKey)
			.ApplyStringFilter(e => e.Value, filter.Value)
			;
			
}