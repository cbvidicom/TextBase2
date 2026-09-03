/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using Textbase.Application.Common;
using Textbase.Host.Api.Authorization;
using Textbase.Application.Features.ClientApplicationLocales;
using Textbase.Host.Api.Common.Extensions;
using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Uwn.Common.Querying;
using Uwn.EntityFrameworkCore.Infrastructure;

namespace Textbase.Host.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed partial class ClientApplicationLocalesController(
	IClientApplicationLocaleQueries Queries,
	IClientApplicationLocaleServerCommands Commands,
	IClientApplicationLocaleAuthorization? Authorization = null
	)
	: ControllerBase
{
	[HttpPost]
	public async Task<ActionResult<CM.ClientApplicationLocaleDto>> Create(
		[FromBody] CM.ClientApplicationLocaleDto dto,
		CancellationToken cancellationToken)
	{	
		if (Authorization is not null &&
			!await Authorization.CanCreateAsync(dto, User, cancellationToken))
			return Forbid();

		CreateResult<DM.ClientApplicationLocale> result = await Commands.CreateAsync(dto, cancellationToken);

		return result.ToActionResult<DM.ClientApplicationLocale, CM.ClientApplicationLocaleDto>(
			model => CreatedAtAction(nameof(Read), Commands.ExtractRouteValues(model), result));
	}

	[HttpGet("{clientApplicationGuid:Guid}/{localeKey}")]
	public async Task<ActionResult<CM.ClientApplicationLocaleDto>> Read(
		Guid clientApplicationGuid, string localeKey,
		CancellationToken cancellationToken)
	{
		if (Authorization is not null &&
			!await Authorization.CanReadAsync(clientApplicationGuid, localeKey, User, cancellationToken))
			return Forbid();

		DM.ClientApplicationLocale? result = await Queries.ReadAsync(clientApplicationGuid, localeKey, cancellationToken);

		return result is null ? NotFound() : Ok(result);
	}

	[HttpGet(ApiStrings.CountRoute)]
	public async Task<ActionResult<long>> Count(
		[FromQuery] ClientApplicationLocaleFilter filter,
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
	public async Task<ActionResult<PagedResponse<DM.ClientApplicationLocale>>> List(
		[FromQuery] ClientApplicationLocaleFilter filter,
		CancellationToken cancellationToken)
	{
		if (Authorization is not null &&
			!await Authorization.CanListAsync(filter, User, cancellationToken))
			return Forbid();

		PagedResponse<DM.ClientApplicationLocale> result = await Queries.ListAsync(filter, cancellationToken);

		return Ok(result);
	}

	[HttpPut("{clientApplicationGuid:Guid}/{localeKey}")]
	public async Task<IActionResult> Update(
		Guid clientApplicationGuid, string localeKey,
		[FromBody] CM.ClientApplicationLocaleDto dto,
		CancellationToken cancellationToken)
	{
		if (Authorization is not null &&
			!await Authorization.CanUpdateAsync(clientApplicationGuid, localeKey, dto, User, cancellationToken))
			return Forbid();

		if (clientApplicationGuid != dto.ClientApplicationGuid || localeKey != dto.LocaleKey)
			return Conflict();

		UpdateResult result = await Commands.UpdateAsync(dto, cancellationToken);

		return result.ToActionResult(Ok);
	}

	[HttpDelete("{clientApplicationGuid:Guid}/{localeKey}")]
	public async Task<IActionResult> Delete(
		Guid clientApplicationGuid, string localeKey,
		CancellationToken cancellationToken)	
	{
		if (Authorization is not null &&
			!await Authorization.CanDeleteAsync(clientApplicationGuid, localeKey, User, cancellationToken))
			return Forbid();

		DeleteResult result = await Commands.DeleteAsync(clientApplicationGuid, localeKey, cancellationToken);

		return result.ToActionResult(Ok);
	}

	
	[HttpGet("ByClientApplicationGuid/{key:Guid}")]
	public async Task<ActionResult<CM.ClientApplicationLocaleDto>> ReadByClientApplicationGuid(
		Guid key,
		CancellationToken cancellationToken)
	{
		if (Authorization is not null &&
			!await Authorization.CanReadByClientApplicationGuidAsync(key, User, cancellationToken))
			return Forbid();

		DM.ClientApplicationLocale? result = await Queries.ReadByClientApplicationGuidAsync(key, cancellationToken);

		return result is null ? NotFound() : Ok(result);
	}
	
}