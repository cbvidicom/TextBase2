/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using Textbase.Application.Common;
using Textbase.Application.Features.AuthPrincipals;
using Textbase.Host.Api.Common.Extensions;
using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Uwn.Common.Querying;
using Uwn.EntityFrameworkCore.Infrastructure;

namespace Textbase.Host.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed partial class AuthPrincipalsController(
	IAuthPrincipalQueries Queries,
	IAuthPrincipalServerCommands Commands
	)
	: ControllerBase
{
	[HttpPost]
	public async Task<ActionResult<CM.AuthPrincipalDto>> Create(
		[FromBody] CM.AuthPrincipalDto dto,
		CancellationToken cancellationToken)
	{	
		CreateResult<DM.AuthPrincipal> result = await Commands.CreateAsync(dto, cancellationToken);

		return result.ToActionResult<DM.AuthPrincipal, CM.AuthPrincipalDto>(
			model => CreatedAtAction(nameof(Read), Commands.ExtractRouteValues(model), result));
	}

	[HttpGet("{entraObjectId:Guid}")]
	public async Task<ActionResult<CM.AuthPrincipalDto>> Read(
		Guid entraObjectId,
		CancellationToken cancellationToken)
	{
		DM.AuthPrincipal? result = await Queries.ReadAsync(entraObjectId, cancellationToken);

		return result is null ? NotFound() : Ok(result);
	}

	[HttpGet(ApiStrings.CountRoute)]
	public async Task<ActionResult<long>> Count(
		[FromQuery] AuthPrincipalFilter filter,
		CancellationToken cancellationToken)
	{
		long result = await Queries.CountAsync(filter, cancellationToken);

		Response.Headers[ApiStrings.TotalCountHeaderKey] = result.ToString();

		return Ok(result);
	}

	[HttpGet]
	public async Task<ActionResult<PagedResponse<DM.AuthPrincipal>>> List(
		[FromQuery] AuthPrincipalFilter filter,
		CancellationToken cancellationToken)
	{
		PagedResponse<DM.AuthPrincipal> result = await Queries.ListAsync(filter, cancellationToken);

		return Ok(result);
	}

	[HttpPut("{entraObjectId:Guid}")]
	public async Task<IActionResult> Update(
		Guid entraObjectId,
		[FromBody] CM.AuthPrincipalDto dto,
		CancellationToken cancellationToken)
	{
		if (entraObjectId != dto.EntraObjectId)
			return Conflict();

		UpdateResult result = await Commands.UpdateAsync(dto, cancellationToken);

		return result.ToActionResult(Ok);
	}

	[HttpDelete("{entraObjectId:Guid}")]
	public async Task<IActionResult> Delete(
		Guid entraObjectId,
		CancellationToken cancellationToken)	
	{
		DeleteResult result = await Commands.DeleteAsync(entraObjectId, cancellationToken);

		return result.ToActionResult(Ok);
	}

	
}