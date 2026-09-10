using Microsoft.Extensions.Options;
using Textbase.Application.Features.Translations;
using Textbase.Host.Authorization;
using Uwn.Blazor.Models.Common;

namespace Textbase.Host.ViewModels.MissingTranslations;

public sealed class MissingTranslationListViewModel(
	ICurrentPrincipalAccessor _currentPrincipalAccessor,
	IOptions<QueryingOptions> _queryingOptions,
	ITranslationServerQueries _translationQueries)
{
	public IReadOnlyList<MissingTranslation> Items { get; private set; } = [];
	public int PageSize => _queryingOptions.Value.DefaultPageSize;

	public async Task LoadAsync(
		CancellationToken cancellationToken = default)
	{
		CurrentPrincipal? principal = await _currentPrincipalAccessor.GetAsync(cancellationToken);
		if (principal is null)
		{
			Items = [];
			return;
		}

		IReadOnlyCollection<Guid>? clientApplicationGuids = principal.HasApplicationRestrictions ? principal.ClientApplicationGuids : null;
		IReadOnlyCollection<string>? localeKeys = principal.HasLocaleRestrictions ? principal.LocaleKeys : null;
		Items = await _translationQueries.ListMissingTranslationsAsync(clientApplicationGuids, localeKeys, cancellationToken);
	}
}
