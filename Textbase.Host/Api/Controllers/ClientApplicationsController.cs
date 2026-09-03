/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using Textbase.Application.Common;
using Textbase.Host.Api.Authorization;
using Textbase.Application.Features.ClientApplications;
using Textbase.Host.Api.Common.Extensions;
using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Uwn.Common.Querying;
using Uwn.EntityFrameworkCore.Infrastructure;

namespace Textbase.Host.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed partial class ClientApplicationsController(
	IClientApplicationQueries Queries,
	IClientApplicationServerCommands Commands,
	IClientApplicationAuthorization? Authorization = null
	)
	: ControllerBase
{
	[HttpPost]
	public async Task<ActionResult<CM.ClientApplicationDto>> Create(
		[FromBody] CM.ClientApplicationDto dto,
		CancellationToken cancellationToken)
	{	
		if (Authorization is not null &&
			!await Authorization.CanCreateAsync(dto, User, cancellationToken))
			return Forbid();

		CreateResult<DM.ClientApplication> result = await Commands.CreateAsync(dto, cancellationToken);

		return result.ToActionResult<DM.ClientApplication, CM.ClientApplicationDto>(
			model => CreatedAtAction(nameof(Read), Commands.ExtractRouteValues(model), result));
	}

	[HttpGet("{clientApplicationGuid:Guid}")]
	public async Task<ActionResult<CM.ClientApplicationDto>> Read(
		Guid clientApplicationGuid,
		CancellationToken cancellationToken)
	{
		if (Authorization is not null &&
			!await Authorization.CanReadAsync(clientApplicationGuid, User, cancellationToken))
			return Forbid();

		DM.ClientApplication? result = await Queries.ReadAsync(clientApplicationGuid, cancellationToken);

		return result is null ? NotFound() : Ok(result);
	}

	[HttpGet(ApiStrings.CountRoute)]
	public async Task<ActionResult<long>> Count(
		[FromQuery] ClientApplicationFilter filter,
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
	public async Task<ActionResult<PagedResponse<DM.ClientApplication>>> List(
		[FromQuery] ClientApplicationFilter filter,
		CancellationToken cancellationToken)
	{
		if (Authorization is not null &&
			!await Authorization.CanListAsync(filter, User, cancellationToken))
			return Forbid();

		PagedResponse<DM.ClientApplication> result = await Queries.ListAsync(filter, cancellationToken);

		return Ok(result);
	}

	[HttpPut("{clientApplicationGuid:Guid}")]
	public async Task<IActionResult> Update(
		Guid clientApplicationGuid,
		[FromBody] CM.ClientApplicationDto dto,
		CancellationToken cancellationToken)
	{
		if (Authorization is not null &&
			!await Authorization.CanUpdateAsync(clientApplicationGuid, dto, User, cancellationToken))
			return Forbid();

		if (clientApplicationGuid != dto.ClientApplicationGuid)
			return Conflict();

		UpdateResult result = await Commands.UpdateAsync(dto, cancellationToken);

		return result.ToActionResult(Ok);
	}

	[HttpDelete("{clientApplicationGuid:Guid}")]
	public async Task<IActionResult> Delete(
		Guid clientApplicationGuid,
		CancellationToken cancellationToken)	
	{
		if (Authorization is not null &&
			!await Authorization.CanDeleteAsync(clientApplicationGuid, User, cancellationToken))
			return Forbid();

		DeleteResult result = await Commands.DeleteAsync(clientApplicationGuid, cancellationToken);

		return result.ToActionResult(Ok);
	}

	
}