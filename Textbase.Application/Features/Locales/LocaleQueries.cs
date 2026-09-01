/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using EM = Textbase.Infrastructure.Persistence.Locales;
using Textbase.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Uwn.Common.Querying;
using Uwn.EntityFrameworkCore.Infrastructure;
using Uwn.EntityFrameworkCore.Querying;

namespace Textbase.Application.Features.Locales;

public sealed partial class LocaleQueries(
	IDbContextFactory<TextbaseDbContext> dbContextFactory)
	: ModelQueries1<TextbaseDbContext, CM.LocaleDto, DM.Locale, EM.LocaleEntity, LocaleFilter, string>(dbContextFactory)
	, ILocaleQueries
	, ICanSort
{
	public override string DefaultSortKey => "localekey";

	protected override IQueryable<EM.LocaleEntity> ApplyFilter(
		IQueryable<EM.LocaleEntity> query,
		LocaleFilter filter)
		=> query
			.ApplyStringFilter(e => e.LocaleKey, filter.LocaleKey)
			.ApplyStringFilter(e => e.ParentLocaleKey, filter.ParentLocaleKey)
			.ApplyStringFilter(e => e.LanguageIso2, filter.LanguageIso2)
			.ApplyStringFilter(e => e.LanguageIso3, filter.LanguageIso3)
			.ApplyNumericFilter(e => e.LanguageIsoN, filter.LanguageIsoN)
			.ApplyNumericFilter(e => e.LanguageLCID, filter.LanguageLCID)
			.ApplyStringFilter(e => e.LanguageWinApi, filter.LanguageWinApi)
			.ApplyStringFilter(e => e.CountryIso2, filter.CountryIso2)
			.ApplyStringFilter(e => e.CountryIso3, filter.CountryIso3)
			.ApplyStringFilter(e => e.NativeName, filter.NativeName)
			.ApplyStringFilter(e => e.EnglishName, filter.EnglishName)
			;
			
}