/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using Textbase.Application.Common;
using Textbase.Host.Api.Authorization;
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
	IFormalityServerCommands Commands,
	IFormalityAuthorization? Authorization = null
	)
	: ControllerBase
{
	[HttpPost]
	public async Task<ActionResult<CM.FormalityDto>> Create(
		[FromBody] CM.FormalityDto dto,
		CancellationToken cancellationToken)
	{	
		if (Authorization is not null &&
			!await Authorization.CanCreateAsync(dto, User, cancellationToken))
			return Forbid();

		CreateResult<DM.Formality> result = await Commands.CreateAsync(dto, cancellationToken);

		return result.ToActionResult<DM.Formality, CM.FormalityDto>(
			model => CreatedAtAction(nameof(Read), Commands.ExtractRouteValues(model), result));
	}

	[HttpGet("{formalityKey}")]
	public async Task<ActionResult<CM.FormalityDto>> Read(
		string formalityKey,
		CancellationToken cancellationToken)
	{
		if (Authorization is not null &&
			!await Authorization.CanReadAsync(formalityKey, User, cancellationToken))
			return Forbid();

		DM.Formality? result = await Queries.ReadAsync(formalityKey, cancellationToken);

		return result is null ? NotFound() : Ok(result);
	}

	[HttpGet(ApiStrings.CountRoute)]
	public async Task<ActionResult<long>> Count(
		[FromQuery] FormalityFilter filter,
		CancellationToken cancellationToken)
	{
		if (Authorization is not null &&
			!await Authorization.CanCountAsync(filter, User, cancellationToken))
			return Forbid();

		long result = await Queries.CountAsync(filter, cancellationToken);

		Response.Headers[ApiStrings.TotalCountHeaderKey] = result.ToString();

		return Ok(result);
	}

	[HttpGet]
	public async Task<ActionResult<PagedResponse<DM.Formality>>> List(
		[FromQuery] FormalityFilter filter,
		CancellationToken cancellationToken)
	{
		if (Authorization is not null &&
			!await Authorization.CanListAsync(filter, User, cancellationToken))
			return Forbid();

		PagedResponse<DM.Formality> result = await Queries.ListAsync(filter, cancellationToken);

		return Ok(result);
	}

	[HttpPut("{formalityKey}")]
	public async Task<IActionResult> Update(
		string formalityKey,
		[FromBody] CM.FormalityDto dto,
		CancellationToken cancellationToken)
	{
		if (Authorization is not null &&
			!await Authorization.CanUpdateAsync(formalityKey, dto, User, cancellationToken))
			return Forbid();

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
		if (Authorization is not null &&
			!await Authorization.CanDeleteAsync(formalityKey, User, cancellationToken))
			return Forbid();

		DeleteResult result = await Commands.DeleteAsync(formalityKey, cancellationToken);

		return result.ToActionResult(Ok);
	}

	
}