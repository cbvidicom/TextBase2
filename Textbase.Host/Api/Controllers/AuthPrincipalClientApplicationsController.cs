/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using Textbase.Application.Common;
using Textbase.Application.Features.AuthPrincipalClientApplications;
using Textbase.Host.Api.Common.Extensions;
using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Uwn.Common.Querying;
using Uwn.EntityFrameworkCore.Infrastructure;

namespace Textbase.Host.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed partial class AuthPrincipalClientApplicationsController(
	IAuthPrincipalClientApplicationQueries Queries,
	IAuthPrincipalClientApplicationServerCommands Commands
	)
	: ControllerBase
{
	[HttpPost]
	public async Task<ActionResult<CM.AuthPrincipalClientApplicationDto>> Create(
		[FromBody] CM.AuthPrincipalClientApplicationDto dto,
		CancellationToken cancellationToken)
	{	
		CreateResult<DM.AuthPrincipalClientApplication> result = await Commands.CreateAsync(dto, cancellationToken);

		return result.ToActionResult<DM.AuthPrincipalClientApplication, CM.AuthPrincipalClientApplicationDto>(
			model => CreatedAtAction(nameof(Read), Commands.ExtractRouteValues(model), result));
	}

	[HttpGet("{entraObjectId:Guid}/{clientApplicationGuid:Guid}")]
	public async Task<ActionResult<CM.AuthPrincipalClientApplicationDto>> Read(
		Guid entraObjectId, Guid clientApplicationGuid,
		CancellationToken cancellationToken)
	{
		DM.AuthPrincipalClientApplication? result = await Queries.ReadAsync(entraObjectId, clientApplicationGuid, cancellationToken);

		return result is null ? NotFound() : Ok(result);
	}

	[HttpGet(ApiStrings.CountRoute)]
	public async Task<ActionResult<long>> Count(
		[FromQuery] AuthPrincipalClientApplicationFilter filter,
		CancellationToken cancellationToken)
	{
		long result = await Queries.CountAsync(filter, cancellationToken);

		Response.Headers[ApiStrings.TotalCountHeaderKey] = result.ToString();

		return Ok(result);
	}

	[HttpGet]
	public async Task<ActionResult<PagedResponse<DM.AuthPrincipalClientApplication>>> List(
		[FromQuery] AuthPrincipalClientApplicationFilter filter,
		CancellationToken cancellationToken)
	{
		PagedResponse<DM.AuthPrincipalClientApplication> result = await Queries.ListAsync(filter, cancellationToken);

		return Ok(result);
	}

	[HttpPut("{entraObjectId:Guid}/{clientApplicationGuid:Guid}")]
	public async Task<IActionResult> Update(
		Guid entraObjectId, Guid clientApplicationGuid,
		[FromBody] CM.AuthPrincipalClientApplicationDto dto,
		CancellationToken cancellationToken)
	{
		if (entraObjectId != dto.EntraObjectId || clientApplicationGuid != dto.ClientApplicationGuid)
			return Conflict();

		UpdateResult result = await Commands.UpdateAsync(dto, cancellationToken);

		return result.ToActionResult(Ok);
	}

	[HttpDelete("{entraObjectId:Guid}/{clientApplicationGuid:Guid}")]
	public async Task<IActionResult> Delete(
		Guid entraObjectId, Guid clientApplicationGuid,
		CancellationToken cancellationToken)	
	{
		DeleteResult result = await Commands.DeleteAsync(entraObjectId, clientApplicationGuid, cancellationToken);

		return result.ToActionResult(Ok);
	}

	
}