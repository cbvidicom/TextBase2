/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using Textbase.Application.Common;
using Textbase.Application.Features.TextResources;
using Textbase.Host.Api.Common.Extensions;
using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Uwn.Common.Querying;
using Uwn.EntityFrameworkCore.Infrastructure;

namespace Textbase.Host.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed partial class TextResourcesController(
	ITextResourceQueries Queries,
	ITextResourceServerCommands Commands
	)
	: ControllerBase
{
	[HttpPost]
	public async Task<ActionResult<CM.TextResourceDto>> Create(
		[FromBody] CM.TextResourceDto dto,
		CancellationToken cancellationToken)
	{	
		CreateResult<DM.TextResource> result = await Commands.CreateAsync(dto, cancellationToken);

		return result.ToActionResult<DM.TextResource, CM.TextResourceDto>(
			model => CreatedAtAction(nameof(Read), Commands.ExtractRouteValues(model), result));
	}

	[HttpGet("{textKey}")]
	public async Task<ActionResult<CM.TextResourceDto>> Read(
		string textKey,
		CancellationToken cancellationToken)
	{
		DM.TextResource? result = await Queries.ReadAsync(textKey, cancellationToken);

		return result is null ? NotFound() : Ok(result);
	}

	[HttpGet(ApiStrings.CountRoute)]
	public async Task<ActionResult<long>> Count(
		[FromQuery] TextResourceFilter filter,
		CancellationToken cancellationToken)
	{
		long result = await Queries.CountAsync(filter, cancellationToken);

		Response.Headers[ApiStrings.TotalCountHeaderKey] = result.ToString();

		return Ok(result);
	}

	[HttpGet]
	public async Task<ActionResult<PagedResponse<DM.TextResource>>> List(
		[FromQuery] TextResourceFilter filter,
		CancellationToken cancellationToken)
	{
		PagedResponse<DM.TextResource> result = await Queries.ListAsync(filter, cancellationToken);

		return Ok(result);
	}

	[HttpPut("{textKey}")]
	public async Task<IActionResult> Update(
		string textKey,
		[FromBody] CM.TextResourceDto dto,
		CancellationToken cancellationToken)
	{
		if (textKey != dto.TextKey)
			return Conflict();

		UpdateResult result = await Commands.UpdateAsync(dto, cancellationToken);

		return result.ToActionResult(Ok);
	}

	[HttpDelete("{textKey}")]
	public async Task<IActionResult> Delete(
		string textKey,
		CancellationToken cancellationToken)	
	{
		DeleteResult result = await Commands.DeleteAsync(textKey, cancellationToken);

		return result.ToActionResult(Ok);
	}

	
}