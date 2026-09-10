using Textbase.Integration.Localization;
using CM = Textbase.Contracts.Models;

namespace Textbase.Integration.Api.Rest;

public sealed partial class FlatTranslationsClient
	: ITranslationSnapshotClient
{
	public async Task<CM.RuntimeLocalizationSnapshotDto> GetAsync(
		Guid clientApplicationGuid,
		CancellationToken cancellationToken = default)
		=> await SendToRouteAsync<CM.RuntimeLocalizationSnapshotDto>(
			HttpMethod.Get,
			ControllerName,
			$"ClientApplication/{clientApplicationGuid}",
			null,
			null,
			cancellationToken)
		?? throw new Exception("Translation snapshot returned a null result.");
}
