using Textbase.Application.Features.FlatTranslations;
using DM = Textbase.Domain.Models;
using Uwn.Common.Querying;

namespace Textbase.Integration.Localization;

public sealed class TextbaseTranslationProvider(
	IFlatTranslationQueries queries,
	ITranslationStore store)
{
	public async Task<IReadOnlyCollection<DM.FlatTranslation>> RefreshAsync(
		Guid clientApplicationGuid,
		CancellationToken cancellationToken = default)
	{
		FlatTranslationFilter filter = FlatTranslationFilter.All();
		PagedResponse<DM.FlatTranslation> response = await queries.ListAsync(filter, cancellationToken);
		IReadOnlyCollection<DM.FlatTranslation> translations = response.Items;

		await store.SetAsync(translations, cancellationToken);
		return translations;
	}
}
