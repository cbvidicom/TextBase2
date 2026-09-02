/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using System.Linq.Expressions;
using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using EM = Textbase.Infrastructure.Persistence.AuthPrincipals;
using Textbase.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Uwn.EntityFrameworkCore.Infrastructure;

namespace Textbase.Application.Features.AuthPrincipals;

public sealed partial class AuthPrincipalCommands(
	IDbContextFactory<TextbaseDbContext> dbContextFactory,
	EM.IAuthPrincipalEntityFactory entityFactory)
	: ModelCommands1<TextbaseDbContext, CM.AuthPrincipalDto, DM.AuthPrincipal, EM.AuthPrincipalEntity, EM.IAuthPrincipalEntityFactory, Guid>(dbContextFactory, entityFactory)
	, IAuthPrincipalServerCommands
{
	public override Expression<Func<CM.AuthPrincipalDto, object>> KeySelector => e => new { e.EntraObjectId };
}