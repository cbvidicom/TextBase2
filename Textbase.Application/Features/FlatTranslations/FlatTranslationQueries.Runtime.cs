using Textbase.Infrastructure.Persistence;
using Textbase.Infrastructure.Persistence.FlatTranslations;
using Microsoft.EntityFrameworkCore;
using DM = Textbase.Domain.Models;

namespace Textbase.Application.Features.FlatTranslations;

public sealed partial class FlatTranslationQueries
	: IFlatTranslationRuntimeQueries
{
	public async Task<IReadOnlyList<DM.FlatTranslation>> ListForClientApplicationAsync(
		Guid clientApplicationGuid,
		CancellationToken cancellationToken = default)
	{
		await using TextbaseDbContext dbContext = await dbContextFactory.CreateDbContextAsync(cancellationToken);

		IQueryable<FlatTranslationEntity> query =
			from FT in dbContext.FlatTranslations.AsNoTracking()
			join CAL in dbContext.ClientApplicationLocales.AsNoTracking() on FT.LocaleKey equals CAL.LocaleKey
			join CATR in dbContext.ClientApplicationTextResources.AsNoTracking() on FT.TextKey equals CATR.TextKey
			join CA in dbContext.ClientApplications.AsNoTracking() on CAL.ClientApplicationGuid equals CA.ClientApplicationGuid
			where CA.ClientApplicationGuid == clientApplicationGuid && CA.IsActive && CATR.ClientApplicationGuid == clientApplicationGuid
			select FT;

		List<FlatTranslationEntity> entities = await query.ToListAsync(cancellationToken);
		return entities.Cast<DM.FlatTranslation>().ToList();
	}
}
