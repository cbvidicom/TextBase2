/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using System.Linq.Expressions;
using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using EM = Textbase.Infrastructure.Persistence.Locales;
using Textbase.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Uwn.EntityFrameworkCore.Infrastructure;

namespace Textbase.Application.Features.Locales;

public sealed partial class LocaleCommands(
	IDbContextFactory<TextbaseDbContext> dbContextFactory,
	EM.ILocaleEntityFactory entityFactory)
	: ModelCommands1<TextbaseDbContext, CM.LocaleDto, DM.Locale, EM.LocaleEntity, EM.ILocaleEntityFactory, string>(dbContextFactory, entityFactory)
	, ILocaleServerCommands
{
	public override Expression<Func<CM.LocaleDto, object>> KeySelector => e => new { e.LocaleKey };
}