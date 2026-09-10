using Textbase.Application.Features.FlatTranslations;
using CM = Textbase.Contracts.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Textbase.Host.Api.Controllers;

public sealed partial class FlatTranslationsController
{
	[AllowAnonymous]
	[HttpGet("ClientApplication/{clientApplicationGuid:guid}")]
	public async Task<ActionResult<CM.RuntimeLocalizationSnapshotDto>> GetClientApplicationSnapshot(
		Guid clientApplicationGuid,
		[FromServices] IFlatTranslationRuntimeQueries runtimeQueries,
		CancellationToken cancellationToken)
	{
		CM.RuntimeLocalizationSnapshotDto? result = await runtimeQueries.GetClientApplicationSnapshotAsync(clientApplicationGuid, cancellationToken);
		return result is null ? NotFound() : Ok(result);
	}
}
