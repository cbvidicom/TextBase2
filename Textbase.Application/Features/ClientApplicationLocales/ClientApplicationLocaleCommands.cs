/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using System.Linq.Expressions;
using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using EM = Textbase.Infrastructure.Persistence.ClientApplicationLocales;
using Textbase.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Uwn.EntityFrameworkCore.Infrastructure;

namespace Textbase.Application.Features.ClientApplicationLocales;

public sealed partial class ClientApplicationLocaleCommands(
	IDbContextFactory<TextbaseDbContext> dbContextFactory,
	EM.IClientApplicationLocaleEntityFactory entityFactory)
	: ModelCommands2<TextbaseDbContext, CM.ClientApplicationLocaleDto, DM.ClientApplicationLocale, EM.ClientApplicationLocaleEntity, EM.IClientApplicationLocaleEntityFactory, Guid, string>(dbContextFactory, entityFactory)
	, IClientApplicationLocaleServerCommands
{
	public override Expression<Func<CM.ClientApplicationLocaleDto, object>> KeySelector => e => new { e.ClientApplicationGuid, e.LocaleKey };
}