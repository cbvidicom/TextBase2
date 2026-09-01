/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using System.Linq.Expressions;
using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using EM = Textbase.Infrastructure.Persistence.ClientApplications;
using Textbase.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Uwn.EntityFrameworkCore.Infrastructure;

namespace Textbase.Application.Features.ClientApplications;

public sealed partial class ClientApplicationCommands(
	IDbContextFactory<TextbaseDbContext> dbContextFactory,
	EM.IClientApplicationEntityFactory entityFactory)
	: ModelCommands1<TextbaseDbContext, CM.ClientApplicationDto, DM.ClientApplication, EM.ClientApplicationEntity, EM.IClientApplicationEntityFactory, Guid>(dbContextFactory, entityFactory)
	, IClientApplicationServerCommands
{
	public override Expression<Func<CM.ClientApplicationDto, object>> KeySelector => e => new { e.ClientApplicationGuid };
}