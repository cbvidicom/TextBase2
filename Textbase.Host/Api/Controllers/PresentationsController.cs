/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using Textbase.Application.Common;
using Textbase.Application.Features.Presentations;
using Textbase.Host.Api.Common.Extensions;
using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Uwn.Common.Querying;
using Uwn.EntityFrameworkCore.Infrastructure;

namespace Textbase.Host.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed partial class PresentationsController(
	IPresentationQueries Queries,
	IPresentationServerCommands Commands
	)
	: ControllerBase
{
	[HttpPost]
	public async Task<ActionResult<CM.PresentationDto>> Create(
		[FromBody] CM.PresentationDto dto,
		CancellationToken cancellationToken)
	{	
		CreateResult<DM.Presentation> result = await Commands.CreateAsync(dto, cancellationToken);

		return result.ToActionResult<DM.Presentation, CM.PresentationDto>(
			model => CreatedAtAction(nameof(Read), Commands.ExtractRouteValues(model), result));
	}

	[HttpGet("{presentationKey}")]
	public async Task<ActionResult<CM.PresentationDto>> Read(
		string presentationKey,
		CancellationToken cancellationToken)
	{
		DM.Presentation? result = await Queries.ReadAsync(presentationKey, cancellationToken);

		return result is null ? NotFound() : Ok(result);
	}

	[HttpGet(ApiStrings.CountRoute)]
	public async Task<ActionResult<long>> Count(
		[FromQuery] PresentationFilter filter,
		CancellationToken cancellationToken)
	{
		long result = await Queries.CountAsync(filter, cancellationToken);

		Response.Headers[ApiStrings.TotalCountHeaderKey] = result.ToString();

		return Ok(result);
	}

	[HttpGet]
	public async Task<ActionResult<PagedResponse<DM.Presentation>>> List(
		[FromQuery] PresentationFilter filter,
		CancellationToken cancellationToken)
	{
		PagedResponse<DM.Presentation> result = await Queries.ListAsync(filter, cancellationToken);

		return Ok(result);
	}

	[HttpPut("{presentationKey}")]
	public async Task<IActionResult> Update(
		string presentationKey,
		[FromBody] CM.PresentationDto dto,
		CancellationToken cancellationToken)
	{
		if (presentationKey != dto.PresentationKey)
			return Conflict();

		UpdateResult result = await Commands.UpdateAsync(dto, cancellationToken);

		return result.ToActionResult(Ok);
	}

	[HttpDelete("{presentationKey}")]
	public async Task<IActionResult> Delete(
		string presentationKey,
		CancellationToken cancellationToken)	
	{
		DeleteResult result = await Commands.DeleteAsync(presentationKey, cancellationToken);

		return result.ToActionResult(Ok);
	}

	
}