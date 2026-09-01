/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using System.Linq.Expressions;
using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using EM = Textbase.Infrastructure.Persistence.Presentations;
using Textbase.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Uwn.EntityFrameworkCore.Infrastructure;

namespace Textbase.Application.Features.Presentations;

public sealed partial class PresentationCommands(
	IDbContextFactory<TextbaseDbContext> dbContextFactory,
	EM.IPresentationEntityFactory entityFactory)
	: ModelCommands1<TextbaseDbContext, CM.PresentationDto, DM.Presentation, EM.PresentationEntity, EM.IPresentationEntityFactory, string>(dbContextFactory, entityFactory)
	, IPresentationServerCommands
{
	public override Expression<Func<CM.PresentationDto, object>> KeySelector => e => new { e.PresentationKey };
}