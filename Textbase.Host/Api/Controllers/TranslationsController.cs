/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using Textbase.Application.Common;
using Textbase.Application.Features.Translations;
using Textbase.Host.Api.Common.Extensions;
using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Uwn.Common.Querying;
using Uwn.EntityFrameworkCore.Infrastructure;

namespace Textbase.Host.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed partial class TranslationsController(
	ITranslationQueries Queries,
	ITranslationServerCommands Commands
	)
	: ControllerBase
{
	[HttpPost]
	public async Task<ActionResult<CM.TranslationDto>> Create(
		[FromBody] CM.TranslationDto dto,
		CancellationToken cancellationToken)
	{	
		CreateResult<DM.Translation> result = await Commands.CreateAsync(dto, cancellationToken);

		return result.ToActionResult<DM.Translation, CM.TranslationDto>(
			model => CreatedAtAction(nameof(Read), Commands.ExtractRouteValues(model), result));
	}

	[HttpGet("{localeKey}/{textKey}/{formalityKey}/{presentationKey}")]
	public async Task<ActionResult<CM.TranslationDto>> Read(
		string localeKey, string textKey, string formalityKey, string presentationKey,
		CancellationToken cancellationToken)
	{
		DM.Translation? result = await Queries.ReadAsync(localeKey, textKey, formalityKey, presentationKey, cancellationToken);

		return result is null ? NotFound() : Ok(result);
	}

	[HttpGet(ApiStrings.CountRoute)]
	public async Task<ActionResult<long>> Count(
		[FromQuery] TranslationFilter filter,
		CancellationToken cancellationToken)
	{
		long result = await Queries.CountAsync(filter, cancellationToken);

		Response.Headers[ApiStrings.TotalCountHeaderKey] = result.ToString();

		return Ok(result);
	}

	[HttpGet]
	public async Task<ActionResult<PagedResponse<DM.Translation>>> List(
		[FromQuery] TranslationFilter filter,
		CancellationToken cancellationToken)
	{
		PagedResponse<DM.Translation> result = await Queries.ListAsync(filter, cancellationToken);

		return Ok(result);
	}

	[HttpPut("{localeKey}/{textKey}/{formalityKey}/{presentationKey}")]
	public async Task<IActionResult> Update(
		string localeKey, string textKey, string formalityKey, string presentationKey,
		[FromBody] CM.TranslationDto dto,
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