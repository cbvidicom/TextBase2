using System.Security.Claims;
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
		ClaimsPrincipal? user = httpContextAccessor.HttpContext?.User;
		string? objectId = user?.GetObjectId();

		if (user is null || !Guid.TryParse(objectId, out Guid entraObjectId))
		{
			return null;
		}

		await using TextbaseDbContext dbContext = await dbContextFactory.CreateDbContextAsync(cancellationToken);

		AuthPrincipalEntity? entity = await ReadAsync(dbContext, entraObjectId, cancellationToken);

		if (entity is null)
		{
			entity = new AuthPrincipalEntity
			{
				EntraObjectId = entraObjectId,
				Role = (int)Roles.None,
				DisplayName = LimitLength(user.FindFirstValue("name") ?? user.Identity?.Name, 128),
				EmailAddress = LimitLength(GetEmailAddress(user), 256),
				IsActive = true
			};

			dbContext.AuthPrincipals.Add(entity);

			try
			{
				await dbContext.SaveChangesAsync(cancellationToken);
			}
			catch (DbUpdateException)
			{
				dbContext.ChangeTracker.Clear();
				entity = await ReadAsync(dbContext, entraObjectId, cancellationToken);

				if (entity is null)
				{
					throw;
				}
			}
		}

		if (!entity.IsActive)
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

	private static async Task<AuthPrincipalEntity?> ReadAsync(
		TextbaseDbContext dbContext,
		Guid entraObjectId,
		CancellationToken cancellationToken)
		=> await dbContext.AuthPrincipals
			.AsNoTracking()
			.AsSplitQuery()
			.Include(e => e.AuthPrincipalClientApplications)
			.Include(e => e.AuthPrincipalLocales)
			.SingleOrDefaultAsync(e => e.EntraObjectId == entraObjectId, cancellationToken);

	private static string? GetEmailAddress(ClaimsPrincipal user)
	{
		string[] claimTypes = [ClaimTypes.Email, "emails", "email", "preferred_username"];

		foreach (string claimType in claimTypes)
		{
			string? value = user.FindFirstValue(claimType);

			if (!string.IsNullOrWhiteSpace(value))
			{
				return value;
			}
		}

		return null;
	}

	private static string? LimitLength(string? value, int maximumLength)
		=> value is null || value.Length <= maximumLength ? value : value[..maximumLength];
}
