using CM = Textbase.Contracts.Models;

namespace Textbase.Integration.Localization;

public interface ITranslationSnapshotClient
{
	Task<CM.RuntimeLocalizationSnapshotDto> GetAsync(
		Guid clientApplicationGuid,
		CancellationToken cancellationToken = default);
}
