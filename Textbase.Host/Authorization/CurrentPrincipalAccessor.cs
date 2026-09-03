using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Textbase.Domain.Enumerations;
using Textbase.Infrastructure.Persistence;
using Textbase.Infrastructure.Persistence.AuthPrincipals;

namespace Textbase.Host.Authorization;

public sealed class CurrentPrincipalAccessor(
	IHttpContextAccessor httpContextAccessor,
	IDbContextFactory<TextbaseDbContext> dbContextFactory)
	: ICurrentPrincipalAccessor
{
	private Task<CurrentPrincipal?>? currentPrincipalTask;

	public Task<CurrentPrincipal?> GetAsync(CancellationToken cancellationToken = default)
	{
		currentPrincipalTask ??= LoadAsync(cancellationToken);

		return currentPrincipalTask;
	}

	private async Task<CurrentPrincipal?> LoadAsync(CancellationToken cancellationToken)
	{
		string? objectId = httpContextAccessor.HttpContext?.User.GetObjectId();

		if (!Guid.TryParse(objectId, out Guid entraObjectId))
		{
			return null;
		}

		await using TextbaseDbContext dbContext = await dbContextFactory.CreateDbContextAsync(cancellationToken);

		AuthPrincipalEntity? entity = await dbContext.AuthPrincipals
			.AsNoTracking()
			.AsSplitQuery()
			.Include(e => e.AuthPrincipalClientApplications)
			.Include(e => e.AuthPrincipalLocales)
			.SingleOrDefaultAsync(e => e.EntraObjectId == entraObjectId && e.IsActive, cancellationToken);

		if (entity is null)
		{
			return null;
		}

		return new CurrentPrincipal(
			entity.EntraObjectId,
			(Roles)entity.Role,
			entity.DisplayName,
			entity.EmailAddress,
			[.. entity.AuthPrincipalClientApplications.Select(e => e.ClientApplicationGuid).Order()],
			[.. entity.AuthPrincipalLocales.Select(e => e.LocaleKey).Order(StringComparer.OrdinalIgnoreCase)]);
	}
}
