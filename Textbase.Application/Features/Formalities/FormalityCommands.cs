/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using System.Linq.Expressions;
using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using EM = Textbase.Infrastructure.Persistence.Formalities;
using Textbase.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Uwn.EntityFrameworkCore.Infrastructure;

namespace Textbase.Application.Features.Formalities;

public sealed partial class FormalityCommands(
	IDbContextFactory<TextbaseDbContext> dbContextFactory,
	EM.IFormalityEntityFactory entityFactory)
	: ModelCommands1<TextbaseDbContext, CM.FormalityDto, DM.Formality, EM.FormalityEntity, EM.IFormalityEntityFactory, string>(dbContextFactory, entityFactory)
	, IFormalityServerCommands
{
	public override Expression<Func<CM.FormalityDto, object>> KeySelector => e => new { e.FormalityKey };
}