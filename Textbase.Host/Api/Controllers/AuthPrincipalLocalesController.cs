/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using Textbase.Application.Common;
using Textbase.Application.Features.AuthPrincipalLocales;
using Textbase.Host.Api.Common.Extensions;
using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Uwn.Common.Querying;
using Uwn.EntityFrameworkCore.Infrastructure;

namespace Textbase.Host.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed partial class AuthPrincipalLocalesController(
	IAuthPrincipalLocaleQueries Queries,
	IAuthPrincipalLocaleServerCommands Commands
	)
	: ControllerBase
{
	[HttpPost]
	public async Task<ActionResult<CM.AuthPrincipalLocaleDto>> Create(
		[FromBody] CM.AuthPrincipalLocaleDto dto,
		CancellationToken cancellationToken)
	{	
		CreateResult<DM.AuthPrincipalLocale> result = await Commands.CreateAsync(dto, cancellationToken);

		return result.ToActionResult<DM.AuthPrincipalLocale, CM.AuthPrincipalLocaleDto>(
			model => CreatedAtAction(nameof(Read), Commands.ExtractRouteValues(model), result));
	}

	[HttpGet("{entraObjectId:Guid}/{localeKey}")]
	public async Task<ActionResult<CM.AuthPrincipalLocaleDto>> Read(
		Guid entraObjectId, string localeKey,
		CancellationToken cancellationToken)
	{
		DM.AuthPrincipalLocale? result = await Queries.ReadAsync(entraObjectId, localeKey, cancellationToken);

		return result is null ? NotFound() : Ok(result);
	}

	[HttpGet(ApiStrings.CountRoute)]
	public async Task<ActionResult<long>> Count(
		[FromQuery] AuthPrincipalLocaleFilter filter,
		CancellationToken cancellationToken)
	{
		long result = await Queries.CountAsync(filter, cancellationToken);

		Response.Headers[ApiStrings.TotalCountHeaderKey] = result.ToString();

		return Ok(result);
	}

	[HttpGet]
	public async Task<ActionResult<PagedResponse<DM.AuthPrincipalLocale>>> List(
		[FromQuery] AuthPrincipalLocaleFilter filter,
		CancellationToken cancellationToken)
	{
		PagedResponse<DM.AuthPrincipalLocale> result = await Queries.ListAsync(filter, cancellationToken);

		return Ok(result);
	}

	[HttpPut("{entraObjectId:Guid}/{localeKey}")]
	public async Task<IActionResult> Update(
		Guid entraObjectId, string localeKey,
		[FromBody] CM.AuthPrincipalLocaleDto dto,
		CancellationToken cancellationToken)
	{
		if (entraObjectId != dto.EntraObjectId || localeKey != dto.LocaleKey)
			return Conflict();

		UpdateResult result = await Commands.UpdateAsync(dto, cancellationToken);

		return result.ToActionResult(Ok);
	}

	[HttpDelete("{entraObjectId:Guid}/{localeKey}")]
	public async Task<IActionResult> Delete(
		Guid entraObjectId, string localeKey,
		CancellationToken cancellationToken)	
	{
		DeleteResult result = await Commands.DeleteAsync(entraObjectId, localeKey, cancellationToken);

		return result.ToActionResult(Ok);
	}

	
}