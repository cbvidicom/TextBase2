/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using Textbase.Application.Common;
using Textbase.Application.Features.FlatTranslations;
using Textbase.Host.Api.Common.Extensions;
using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Uwn.Common.Querying;
using Uwn.EntityFrameworkCore.Infrastructure;

namespace Textbase.Host.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed partial class FlatTranslationsController(
	IFlatTranslationQueries Queries,
	IFlatTranslationServerCommands Commands
	)
	: ControllerBase
{
	[HttpPost]
	public async Task<ActionResult<CM.FlatTranslationDto>> Create(
		[FromBody] CM.FlatTranslationDto dto,
		CancellationToken cancellationToken)
	{	
		CreateResult<DM.FlatTranslation> result = await Commands.CreateAsync(dto, cancellationToken);

		return result.ToActionResult<DM.FlatTranslation, CM.FlatTranslationDto>(
			model => CreatedAtAction(nameof(Read), Commands.ExtractRouteValues(model), result));
	}

	[HttpGet("{localeKey}/{textKey}/{formalityKey}/{presentationKey}")]
	public async Task<ActionResult<CM.FlatTranslationDto>> Read(
		string localeKey, string textKey, string formalityKey, string presentationKey,
		CancellationToken cancellationToken)
	{
		DM.FlatTranslation? result = await Queries.ReadAsync(localeKey, textKey, formalityKey, presentationKey, cancellationToken);

		return result is null ? NotFound() : Ok(result);
	}

	[HttpGet(ApiStrings.CountRoute)]
	public async Task<ActionResult<long>> Count(
		[FromQuery] FlatTranslationFilter filter,
		CancellationToken cancellationToken)
	{
		long result = await Queries.CountAsync(filter, cancellationToken);

		Response.Headers[ApiStrings.TotalCountHeaderKey] = result.ToString();

		return Ok(result);
	}

	[HttpGet]
	public async Task<ActionResult<PagedResponse<DM.FlatTranslation>>> List(
		[FromQuery] FlatTranslationFilter filter,
		CancellationToken cancellationToken)
	{
		PagedResponse<DM.FlatTranslation> result = await Queries.ListAsync(filter, cancellationToken);

		return Ok(result);
	}

	[HttpPut("{localeKey}/{textKey}/{formalityKey}/{presentationKey}")]
	public async Task<IActionResult> Update(
		string localeKey, string textKey, string formalityKey, string presentationKey,
		[FromBody] CM.FlatTranslationDto dto,
		CancellationToken cancellationToken)
	{
		if (localeKey != dto.LocaleKey || textKey != dto.TextKey || formalityKey != dto.FormalityKey || presentationKey != dto.PresentationKey)
			return Conflict();

		UpdateResult result = await Commands.UpdateAsync(dto, cancellationToken);

		return result.ToActionResult(Ok);
	}

	[HttpDelete("{localeKey}/{textKey}/{formalityKey}/{presentationKey}")]
	public async Task<IActionResult> Delete(
		string localeKey, string textKey, string formalityKey, string presentationKey,
		CancellationToken cancellationToken)	
	{
		DeleteResult result = await Commands.DeleteAsync(localeKey, textKey, formalityKey, presentationKey, cancellationToken);

		return result.ToActionResult(Ok);
	}

	
}