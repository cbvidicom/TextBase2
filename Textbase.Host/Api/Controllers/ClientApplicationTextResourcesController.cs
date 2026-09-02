/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using Textbase.Application.Common;
using Textbase.Application.Features.ClientApplicationTextResources;
using Textbase.Host.Api.Common.Extensions;
using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Uwn.Common.Querying;
using Uwn.EntityFrameworkCore.Infrastructure;

namespace Textbase.Host.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed partial class ClientApplicationTextResourcesController(
	IClientApplicationTextResourceQueries Queries,
	IClientApplicationTextResourceServerCommands Commands
	)
	: ControllerBase
{
	[HttpPost]
	public async Task<ActionResult<CM.ClientApplicationTextResourceDto>> Create(
		[FromBody] CM.ClientApplicationTextResourceDto dto,
		CancellationToken cancellationToken)
	{	
		CreateResult<DM.ClientApplicationTextResource> result = await Commands.CreateAsync(dto, cancellationToken);

		return result.ToActionResult<DM.ClientApplicationTextResource, CM.ClientApplicationTextResourceDto>(
			model => CreatedAtAction(nameof(Read), Commands.ExtractRouteValues(model), result));
	}

	[HttpGet("{clientApplicationGuid:Guid}/{textKey}")]
	public async Task<ActionResult<CM.ClientApplicationTextResourceDto>> Read(
		Guid clientApplicationGuid, string textKey,
		CancellationToken cancellationToken)
	{
		DM.ClientApplicationTextResource? result = await Queries.ReadAsync(clientApplicationGuid, textKey, cancellationToken);

		return result is null ? NotFound() : Ok(result);
	}

	[HttpGet(ApiStrings.CountRoute)]
	public async Task<ActionResult<long>> Count(
		[FromQuery] ClientApplicationTextResourceFilter filter,
		CancellationToken cancellationToken)
	{
		long result = await Queries.CountAsync(filter, cancellationToken);

		Response.Headers[ApiStrings.TotalCountHeaderKey] = result.ToString();

		return Ok(result);
	}

	[HttpGet]
	public async Task<ActionResult<PagedResponse<DM.ClientApplicationTextResource>>> List(
		[FromQuery] ClientApplicationTextResourceFilter filter,
		CancellationToken cancellationToken)
	{
		PagedResponse<DM.ClientApplicationTextResource> result = await Queries.ListAsync(filter, cancellationToken);

		return Ok(result);
	}

	[HttpPut("{clientApplicationGuid:Guid}/{textKey}")]
	public async Task<IActionResult> Update(
		Guid clientApplicationGuid, string textKey,
		[FromBody] CM.ClientApplicationTextResourceDto dto,
		CancellationToken cancellationToken)
	{
		if (clientApplicationGuid != dto.ClientApplicationGuid || textKey != dto.TextKey)
			return Conflict();

		UpdateResult result = await Commands.UpdateAsync(dto, cancellationToken);

		return result.ToActionResult(Ok);
	}

	[HttpDelete("{clientApplicationGuid:Guid}/{textKey}")]
	public async Task<IActionResult> Delete(
		Guid clientApplicationGuid, string textKey,
		CancellationToken cancellationToken)	
	{
		DeleteResult result = await Commands.DeleteAsync(clientApplicationGuid, textKey, cancellationToken);

		return result.ToActionResult(Ok);
	}

	
}