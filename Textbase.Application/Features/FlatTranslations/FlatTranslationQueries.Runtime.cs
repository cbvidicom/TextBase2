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
			return null;

		List<(string LocaleKey, bool IsDefault)> locales = await dbContext.ClientApplicationLocales.AsNoTracking()
			.Where(CAL => CAL.ClientApplicationGuid == clientApplicationGuid)
			.Select(CAL => new ValueTuple<string, bool>(CAL.LocaleKey, CAL.IsDefault))
			.ToListAsync(cancellationToken);

		string? defaultLocaleKey = locales.Where(L => L.IsDefault).Select(L => L.LocaleKey).SingleOrDefault();
		if (defaultLocaleKey is null)
			return null;

		IQueryable<FlatTranslationEntity> query =
			from FT in dbContext.FlatTranslations.AsNoTracking()
			join CAL in dbContext.ClientApplicationLocales.AsNoTracking() on FT.LocaleKey equals CAL.LocaleKey
			join CATR in dbContext.ClientApplicationTextResources.AsNoTracking() on FT.TextKey equals CATR.TextKey
			where CAL.ClientApplicationGuid == clientApplicationGuid && CATR.ClientApplicationGuid == clientApplicationGuid
			select FT;

		List<CM.FlatTranslationDto> translations = await query.Cast<CM.FlatTranslationDto>().ToListAsync(cancellationToken);

		return new CM.RuntimeLocalizationSnapshotDto
		{
			DefaultLocaleKey = defaultLocaleKey,
			SupportedLocaleKeys = locales.Select(L => L.LocaleKey).ToList(),
			Translations = translations
		};
	}
}
