using Textbase.Application.Common;
using Textbase.Infrastructure.Persistence;
using Textbase.Infrastructure.Persistence.Translations;
using Uwn.EntityFrameworkCore.Infrastructure;

namespace Textbase.Application.Features.Translations;

public sealed partial class TranslationCommands
{
	protected override async Task<HookResult> OnBeforeSaveChangesAsync(
		TextbaseDbContext dbContext,
		DbContextOperation operation,
		TranslationEntity entity,
		CancellationToken cancellationToken = default)
	{
		if (operation != DbContextOperation.Update &&
			operation != DbContextOperation.Remove)
			return HookResult.Continue;

		int activeApplicationCount = await dbContext.CountActiveApplicationsForTranslationAsync(entity.LocaleKey, entity.TextKey, cancellationToken);

		return operation switch
		{
			DbContextOperation.Update when activeApplicationCount > 1 => HookResult.Cancel,
			DbContextOperation.Remove when activeApplicationCount > 0 => HookResult.Cancel,
			_ => HookResult.Continue
		};
	}
}
