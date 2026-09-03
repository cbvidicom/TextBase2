/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using Textbase.Application.Common;
using Textbase.Host.Api.Authorization;
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
	IPresentationServerCommands Commands,
	IPresentationAuthorization? Authorization = null
	)
	: ControllerBase
{
	[HttpPost]
	public async Task<ActionResult<CM.PresentationDto>> Create(
		[FromBody] CM.PresentationDto dto,
		CancellationToken cancellationToken)
	{	
		if (Authorization is not null &&
			!await Authorization.CanCreateAsync(dto, User, cancellationToken))
			return Forbid();

		CreateResult<DM.Presentation> result = await Commands.CreateAsync(dto, cancellationToken);

		return result.ToActionResult<DM.Presentation, CM.PresentationDto>(
			model => CreatedAtAction(nameof(Read), Commands.ExtractRouteValues(model), result));
	}

	[HttpGet("{presentationKey}")]
	public async Task<ActionResult<CM.PresentationDto>> Read(
		string presentationKey,
		CancellationToken cancellationToken)
	{
		if (Authorization is not null &&
			!await Authorization.CanReadAsync(presentationKey, User, cancellationToken))
			return Forbid();

		DM.Presentation? result = await Queries.ReadAsync(presentationKey, cancellationToken);

		return result is null ? NotFound() : Ok(result);
	}

	[HttpGet(ApiStrings.CountRoute)]
	public async Task<ActionResult<long>> Count(
		[FromQuery] PresentationFilter filter,
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
	public async Task<ActionResult<PagedResponse<DM.Presentation>>> List(
		[FromQuery] PresentationFilter filter,
		CancellationToken cancellationToken)
	{
		if (Authorization is not null &&
			!await Authorization.CanListAsync(filter, User, cancellationToken))
			return Forbid();

		PagedResponse<DM.Presentation> result = await Queries.ListAsync(filter, cancellationToken);

		return Ok(result);
	}

	[HttpPut("{presentationKey}")]
	public async Task<IActionResult> Update(
		string presentationKey,
		[FromBody] CM.PresentationDto dto,
		CancellationToken cancellationToken)
	{
		if (Authorization is not null &&
			!await Authorization.CanUpdateAsync(presentationKey, dto, User, cancellationToken))
			return Forbid();

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
		if (Authorization is not null &&
			!await Authorization.CanDeleteAsync(presentationKey, User, cancellationToken))
			return Forbid();

		DeleteResult result = await Commands.DeleteAsync(presentationKey, cancellationToken);

		return result.ToActionResult(Ok);
	}

	
}