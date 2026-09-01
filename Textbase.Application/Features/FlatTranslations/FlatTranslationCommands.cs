/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using System.Linq.Expressions;
using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using EM = Textbase.Infrastructure.Persistence.FlatTranslations;
using Textbase.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Uwn.EntityFrameworkCore.Infrastructure;

namespace Textbase.Application.Features.FlatTranslations;

public sealed partial class FlatTranslationCommands(
	IDbContextFactory<TextbaseDbContext> dbContextFactory,
	EM.IFlatTranslationEntityFactory entityFactory)
	: ModelCommands4<TextbaseDbContext, CM.FlatTranslationDto, DM.FlatTranslation, EM.FlatTranslationEntity, EM.IFlatTranslationEntityFactory, string, string, string, string>(dbContextFactory, entityFactory)
	, IFlatTranslationServerCommands
{
	public override Expression<Func<CM.FlatTranslationDto, object>> KeySelector => e => new { e.LocaleKey, e.TextKey, e.FormalityKey, e.PresentationKey };
}