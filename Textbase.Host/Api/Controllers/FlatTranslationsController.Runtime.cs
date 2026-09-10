using Textbase.Application.Features.FlatTranslations;
using DM = Textbase.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Textbase.Host.Api.Controllers;

public sealed partial class FlatTranslationsController
{
	[HttpGet("ClientApplication/{clientApplicationGuid:guid}")]
	public async Task<ActionResult<IReadOnlyList<DM.FlatTranslation>>> ListForClientApplication(
		Guid clientApplicationGuid,
		[FromServices] IFlatTranslationRuntimeQueries runtimeQueries,
		CancellationToken cancellationToken)
	{
		IReadOnlyList<DM.FlatTranslation> result = await runtimeQueries.ListForClientApplicationAsync(clientApplicationGuid, cancellationToken);
		return Ok(result);
	}
}
