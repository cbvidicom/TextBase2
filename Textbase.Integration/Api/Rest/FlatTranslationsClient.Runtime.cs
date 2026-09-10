using Textbase.Integration.Localization;
using DM = Textbase.Domain.Models;

namespace Textbase.Integration.Api.Rest;

public sealed partial class FlatTranslationsClient
	: ITranslationSnapshotClient
{
	public async Task<IReadOnlyList<DM.FlatTranslation>> GetAsync(
		Guid clientApplicationGuid,
		CancellationToken cancellationToken = default)
		=> await SendToRouteAsync<IReadOnlyList<DM.FlatTranslation>>(
			HttpMethod.Get,
			ControllerName,
			$"ClientApplication/{clientApplicationGuid}",
			null,
			null,
			cancellationToken)
		?? throw new Exception("Translation snapshot returned a null result.");
}
