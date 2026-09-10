using Microsoft.EntityFrameworkCore;
using Textbase.Infrastructure.Persistence;

namespace Textbase.Application.Features.Translations;

public sealed partial class TranslationQueries
	: ITranslationServerQueries
{
	public async Task<bool> HasActiveApplicationAsync(
		string localeKey,
		string textKey,
		CancellationToken cancellationToken = default)
	{
		await using TextbaseDbContext dbContext = await _DbContextFactory.CreateDbContextAsync(cancellationToken);

		return await dbContext.ClientApplications.AnyAsync(
			clientApplication => clientApplication.IsActive &&
				clientApplication.ClientApplicationLocales.Any(locale => locale.LocaleKey == localeKey) &&
				clientApplication.ClientApplicationTextResources.Any(textResource => textResource.TextKey == textKey),
			cancellationToken);
	}
}
