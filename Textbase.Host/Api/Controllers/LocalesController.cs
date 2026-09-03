/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using Textbase.Application.Common;
using Textbase.Host.Api.Authorization;
using Textbase.Application.Features.Locales;
using Textbase.Host.Api.Common.Extensions;
using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Uwn.Common.Querying;
using Uwn.EntityFrameworkCore.Infrastructure;

namespace Textbase.Host.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed partial class LocalesController(
	ILocaleQueries Queries,
	ILocaleServerCommands Commands,
	ILocaleAuthorization? Authorization = null
	)
	: ControllerBase
{
	[HttpPost]
	public async Task<ActionResult<CM.LocaleDto>> Create(
		[FromBody] CM.LocaleDto dto,
		CancellationToken cancellationToken)
	{	
		if (Authorization is not null &&
			!await Authorization.CanCreateAsync(dto, User, cancellationToken))
			return Forbid();

		CreateResult<DM.Locale> result = await Commands.CreateAsync(dto, cancellationToken);

		return result.ToActionResult<DM.Locale, CM.LocaleDto>(
			model => CreatedAtAction(nameof(Read), Commands.ExtractRouteValues(model), result));
	}

	[HttpGet("{localeKey}")]
	public async Task<ActionResult<CM.LocaleDto>> Read(
		string localeKey,
		CancellationToken cancellationToken)
	{
		if (Authorization is not null &&
			!await Authorization.CanReadAsync(localeKey, User, cancellationToken))
			return Forbid();

		DM.Locale? result = await Queries.ReadAsync(localeKey, cancellationToken);

		return result is null ? NotFound() : Ok(result);
	}

	[HttpGet(ApiStrings.CountRoute)]
	public async Task<ActionResult<long>> Count(
		[FromQuery] LocaleFilter filter,
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
	public async Task<ActionResult<PagedResponse<DM.Locale>>> List(
		[FromQuery] LocaleFilter filter,
		CancellationToken cancellationToken)
	{
		if (Authorization is not null &&
			!await Authorization.CanListAsync(filter, User, cancellationToken))
			return Forbid();

		PagedResponse<DM.Locale> result = await Queries.ListAsync(filter, cancellationToken);

		return Ok(result);
	}

	[HttpPut("{localeKey}")]
	public async Task<IActionResult> Update(
		string localeKey,
		[FromBody] CM.LocaleDto dto,
		CancellationToken cancellationToken)
	{
		if (Authorization is not null &&
			!await Authorization.CanUpdateAsync(localeKey, dto, User, cancellationToken))
			return Forbid();

		if (localeKey != dto.LocaleKey)
			return Conflict();

		UpdateResult result = await Commands.UpdateAsync(dto, cancellationToken);

		return result.ToActionResult(Ok);
	}

	[HttpDelete("{localeKey}")]
	public async Task<IActionResult> Delete(
		string localeKey,
		CancellationToken cancellationToken)	
	{
		if (Authorization is not null &&
			!await Authorization.CanDeleteAsync(localeKey, User, cancellationToken))
			return Forbid();

		DeleteResult result = await Commands.DeleteAsync(localeKey, cancellationToken);

		return result.ToActionResult(Ok);
	}

	
}