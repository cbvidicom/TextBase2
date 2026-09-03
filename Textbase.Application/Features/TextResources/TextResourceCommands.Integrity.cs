using Microsoft.EntityFrameworkCore;
using Textbase.Application.Common;
using Textbase.Infrastructure.Persistence;
using Textbase.Infrastructure.Persistence.ClientApplicationTextResources;
using Textbase.Infrastructure.Persistence.TextResources;
using Textbase.Infrastructure.Persistence.Translations;
using Uwn.EntityFrameworkCore.Infrastructure;

namespace Textbase.Application.Features.TextResources;

public sealed partial class TextResourceCommands
{
	protected override async Task<HookResult> OnBeforeSaveChangesAsync(
		TextbaseDbContext dbContext,
		DbContextOperation operation,
		TextResourceEntity entity,
		CancellationToken cancellationToken = default)
	{
		if (operation != DbContextOperation.Remove)
			return HookResult.Continue;

		if (await dbContext.HasActiveApplicationForTextAsync(entity.TextKey, cancellationToken))
			return HookResult.Cancel;

		List<TranslationEntity> translations = await dbContext.Translations
			.Where(translation => translation.TextKey == entity.TextKey)
			.ToListAsync(cancellationToken);

		List<ClientApplicationTextResourceEntity> clientApplicationTextResources = await dbContext.ClientApplicationTextResources
			.Where(clientApplicationTextResource => clientApplicationTextResource.TextKey == entity.TextKey)
			.ToListAsync(cancellationToken);

		dbContext.RemoveRange(translations);
		dbContext.RemoveRange(clientApplicationTextResources);

		return HookResult.Continue;
	}
}
