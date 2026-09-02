/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using System.Linq.Expressions;
using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using EM = Textbase.Infrastructure.Persistence.AuthPrincipalLocales;
using Textbase.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Uwn.EntityFrameworkCore.Infrastructure;

namespace Textbase.Application.Features.AuthPrincipalLocales;

public sealed partial class AuthPrincipalLocaleCommands(
	IDbContextFactory<TextbaseDbContext> dbContextFactory,
	EM.IAuthPrincipalLocaleEntityFactory entityFactory)
	: ModelCommands2<TextbaseDbContext, CM.AuthPrincipalLocaleDto, DM.AuthPrincipalLocale, EM.AuthPrincipalLocaleEntity, EM.IAuthPrincipalLocaleEntityFactory, Guid, string>(dbContextFactory, entityFactory)
	, IAuthPrincipalLocaleServerCommands
{
	public override Expression<Func<CM.AuthPrincipalLocaleDto, object>> KeySelector => e => new { e.EntraObjectId, e.LocaleKey };
}