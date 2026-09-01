/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using System.Linq.Expressions;
using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using EM = Textbase.Infrastructure.Persistence.ClientApplicationTextResources;
using Textbase.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Uwn.EntityFrameworkCore.Infrastructure;

namespace Textbase.Application.Features.ClientApplicationTextResources;

public sealed partial class ClientApplicationTextResourceCommands(
	IDbContextFactory<TextbaseDbContext> dbContextFactory,
	EM.IClientApplicationTextResourceEntityFactory entityFactory)
	: ModelCommands2<TextbaseDbContext, CM.ClientApplicationTextResourceDto, DM.ClientApplicationTextResource, EM.ClientApplicationTextResourceEntity, EM.IClientApplicationTextResourceEntityFactory, Guid, string>(dbContextFactory, entityFactory)
	, IClientApplicationTextResourceServerCommands
{
	public override Expression<Func<CM.ClientApplicationTextResourceDto, object>> KeySelector => e => new { e.ClientApplicationGuid, e.TextKey };
}