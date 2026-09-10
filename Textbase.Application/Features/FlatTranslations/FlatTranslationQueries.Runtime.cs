using Textbase.Infrastructure.Persistence;
using Textbase.Infrastructure.Persistence.FlatTranslations;
using Microsoft.EntityFrameworkCore;
using CM = Textbase.Contracts.Models;

namespace Textbase.Application.Features.FlatTranslations;

public sealed partial class FlatTranslationQueries
	: IFlatTranslationRuntimeQueries
{
	public async Task<CM.RuntimeLocalizationSnapshotDto?> GetClientApplicationSnapshotAsync(
		Guid clientApplicationGuid,
		CancellationToken cancellationToken = default)
	{
		await using TextbaseDbContext dbContext = await dbContextFactory.CreateDbContextAsync(cancellationToken);

		bool isActive = await dbContext.ClientApplications.AsNoTracking().AnyAsync(CA => CA.ClientApplicationGuid == clientApplicationGuid && CA.IsActive, cancellationToken);
		if (!isActive)
		{
			return null;
		}

		List<string> supportedLocaleKeys = await dbContext.ClientApplicationLocales.AsNoTracking()
			.Where(CAL => CAL.ClientApplicationGuid == clientApplicationGuid)
			.Select(CAL => CAL.LocaleKey)
			.ToListAsync(cancellationToken);

		string? defaultLocaleKey = await dbContext.ClientApplicationLocales.AsNoTracking()
			.Where(CAL => CAL.ClientApplicationGuid == clientApplicationGuid && CAL.IsDefault)
			.Select(CAL => CAL.LocaleKey)
			.SingleOrDefaultAsync(cancellationToken);

		if (defaultLocaleKey is null)
		{
			return null;
		}

		IQueryable<FlatTranslationEntity> query =
			from FT in dbContext.FlatTranslations.AsNoTracking()
			join CAL in dbContext.ClientApplicationLocales.AsNoTracking() on FT.LocaleKey equals CAL.LocaleKey
			join CATR in dbContext.ClientApplicationTextResources.AsNoTracking() on FT.TextKey equals CATR.TextKey
			where CAL.ClientApplicationGuid == clientApplicationGuid && CATR.ClientApplicationGuid == clientApplicationGuid
			select FT;

		List<FlatTranslationEntity> entities = await query.ToListAsync(cancellationToken);

		return new CM.RuntimeLocalizationSnapshotDto
		{
			DefaultLocaleKey = defaultLocaleKey,
			SupportedLocaleKeys = supportedLocaleKeys,
			Translations = entities.Cast<CM.FlatTranslationDto>().ToList()
		};
	}
}
