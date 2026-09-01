/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using System.Linq.Expressions;
using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using EM = Textbase.Infrastructure.Persistence.Translations;
using Textbase.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Uwn.EntityFrameworkCore.Infrastructure;

namespace Textbase.Application.Features.Translations;

public sealed partial class TranslationCommands(
	IDbContextFactory<TextbaseDbContext> dbContextFactory,
	EM.ITranslationEntityFactory entityFactory)
	: ModelCommands4<TextbaseDbContext, CM.TranslationDto, DM.Translation, EM.TranslationEntity, EM.ITranslationEntityFactory, string, string, string, string>(dbContextFactory, entityFactory)
	, ITranslationServerCommands
{
	public override Expression<Func<CM.TranslationDto, object>> KeySelector => e => new { e.LocaleKey, e.TextKey, e.FormalityKey, e.PresentationKey };
}