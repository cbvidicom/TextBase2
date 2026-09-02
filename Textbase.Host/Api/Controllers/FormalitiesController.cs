/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using Textbase.Application.Common;
using Textbase.Application.Features.Formalities;
using Textbase.Host.Api.Common.Extensions;
using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Uwn.Common.Querying;
using Uwn.EntityFrameworkCore.Infrastructure;

namespace Textbase.Host.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed partial class FormalitiesController(
	IFormalityQueries Queries,
	IFormalityServerCommands Commands
	)
	: ControllerBase
{
	[HttpPost]
	public async Task<ActionResult<CM.FormalityDto>> Create(
		[FromBody] CM.FormalityDto dto,
		CancellationToken cancellationToken)
	{	
		CreateResult<DM.Formality> result = await Commands.CreateAsync(dto, cancellationToken);

		return result.ToActionResult<DM.Formality, CM.FormalityDto>(
			model => CreatedAtAction(nameof(Read), Commands.ExtractRouteValues(model), result));
	}

	[HttpGet("{formalityKey}")]
	public async Task<ActionResult<CM.FormalityDto>> Read(
		string formalityKey,
		CancellationToken cancellationToken)
	{
		DM.Formality? result = await Queries.ReadAsync(formalityKey, cancellationToken);

		return result is null ? NotFound() : Ok(result);
	}

	[HttpGet(ApiStrings.CountRoute)]
	public async Task<ActionResult<long>> Count(
		[FromQuery] FormalityFilter filter,
		CancellationToken cancellationToken)
	{
		long result = await Queries.CountAsync(filter, cancellationToken);

		Response.Headers[ApiStrings.TotalCountHeaderKey] = result.ToString();

		return Ok(result);
	}

	[HttpGet]
	public async Task<ActionResult<PagedResponse<DM.Formality>>> List(
		[FromQuery] FormalityFilter filter,
		CancellationToken cancellationToken)
	{
		PagedResponse<DM.Formality> result = await Queries.ListAsync(filter, cancellationToken);

		return Ok(result);
	}

	[HttpPut("{formalityKey}")]
	public async Task<IActionResult> Update(
		string formalityKey,
		[FromBody] CM.FormalityDto dto,
		CancellationToken cancellationToken)
	{
		if (formalityKey != dto.FormalityKey)
			return Conflict();

		UpdateResult result = await Commands.UpdateAsync(dto, cancellationToken);

		return result.ToActionResult(Ok);
	}

	[HttpDelete("{formalityKey}")]
	public async Task<IActionResult> Delete(
		string formalityKey,
		CancellationToken cancellationToken)	
	{
		DeleteResult result = await Commands.DeleteAsync(formalityKey, cancellationToken);

		return result.ToActionResult(Ok);
	}

	
}