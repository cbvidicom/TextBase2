using Microsoft.EntityFrameworkCore;
using Textbase.Application.Common;
using Textbase.Infrastructure.Persistence;
using Textbase.Infrastructure.Persistence.AuthPrincipalClientApplications;
using Textbase.Infrastructure.Persistence.ClientApplicationLocales;
using Textbase.Infrastructure.Persistence.ClientApplications;
using Textbase.Infrastructure.Persistence.ClientApplicationTextResources;
using Uwn.EntityFrameworkCore.Infrastructure;

namespace Textbase.Application.Features.ClientApplications;

public sealed partial class ClientApplicationCommands
{
	protected override async Task<HookResult> OnBeforeSaveChangesAsync(
		TextbaseDbContext dbContext,
		DbContextOperation operation,
		ClientApplicationEntity entity,
		CancellationToken cancellationToken = default)
	{
		if (operation != DbContextOperation.Remove)
			return HookResult.Continue;

		if (await dbContext.HasConnectedTranslationsAsync(entity.ClientApplicationGuid, cancellationToken))
			return HookResult.Cancel;

		List<AuthPrincipalClientApplicationEntity> authPrincipalClientApplications = await dbContext.AuthPrincipalClientApplications
			.Where(authPrincipalClientApplication => authPrincipalClientApplication.ClientApplicationGuid == entity.ClientApplicationGuid)
			.ToListAsync(cancellationToken);

		List<ClientApplicationLocaleEntity> clientApplicationLocales = await dbContext.ClientApplicationLocales
			.Where(clientApplicationLocale => clientApplicationLocale.ClientApplicationGuid == entity.ClientApplicationGuid)
			.ToListAsync(cancellationToken);

		List<ClientApplicationTextResourceEntity> clientApplicationTextResources = await dbContext.ClientApplicationTextResources
			.Where(clientApplicationTextResource => clientApplicationTextResource.ClientApplicationGuid == entity.ClientApplicationGuid)
			.ToListAsync(cancellationToken);

		dbContext.RemoveRange(authPrincipalClientApplications);
		dbContext.RemoveRange(clientApplicationLocales);
		dbContext.RemoveRange(clientApplicationTextResources);

		return HookResult.Continue;
	}
}
