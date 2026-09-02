/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using System.Linq.Expressions;
using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using EM = Textbase.Infrastructure.Persistence.AuthPrincipalClientApplications;
using Textbase.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Uwn.EntityFrameworkCore.Infrastructure;

namespace Textbase.Application.Features.AuthPrincipalClientApplications;

public sealed partial class AuthPrincipalClientApplicationCommands(
	IDbContextFactory<TextbaseDbContext> dbContextFactory,
	EM.IAuthPrincipalClientApplicationEntityFactory entityFactory)
	: ModelCommands2<TextbaseDbContext, CM.AuthPrincipalClientApplicationDto, DM.AuthPrincipalClientApplication, EM.AuthPrincipalClientApplicationEntity, EM.IAuthPrincipalClientApplicationEntityFactory, Guid, Guid>(dbContextFactory, entityFactory)
	, IAuthPrincipalClientApplicationServerCommands
{
	public override Expression<Func<CM.AuthPrincipalClientApplicationDto, object>> KeySelector => e => new { e.EntraObjectId, e.ClientApplicationGuid };
}