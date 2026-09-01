/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using System.Linq.Expressions;
using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using EM = Textbase.Infrastructure.Persistence.TextResources;
using Textbase.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Uwn.EntityFrameworkCore.Infrastructure;

namespace Textbase.Application.Features.TextResources;

public sealed partial class TextResourceCommands(
	IDbContextFactory<TextbaseDbContext> dbContextFactory,
	EM.ITextResourceEntityFactory entityFactory)
	: ModelCommands1<TextbaseDbContext, CM.TextResourceDto, DM.TextResource, EM.TextResourceEntity, EM.ITextResourceEntityFactory, string>(dbContextFactory, entityFactory)
	, ITextResourceServerCommands
{
	public override Expression<Func<CM.TextResourceDto, object>> KeySelector => e => new { e.TextKey };
}