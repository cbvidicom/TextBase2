/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using Textbase.Application.Common;
using Textbase.Application.Features.Translations;
using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using Uwn.Common.Net.Http;
using Uwn.Common.Querying;
using Uwn.EntityFrameworkCore.Infrastructure;

namespace Textbase.Integration.Api.Rest;

public sealed partial class TranslationsClient(
	string baseUrl,
	string? bearerToken = null,
	HttpClient? httpClient = null
	)
	: RestClientBase(baseUrl, bearerToken, httpClient)
	, ITranslationCommands
	, ITranslationQueries
	, IClientForController
{
	public string ControllerName => ApiStrings.TranslationsControllerName;

	//

	protected override bool ThrowOnNonSuccessStatusCode => false;

	//

	public async Task<CreateResult<DM.Translation>> CreateAsync(
		CM.TranslationDto dto,
		CancellationToken cancellationToken = default)
		=> await SendAsync<CreateResult<DM.Translation>>(HttpMethod.Post, ControllerName, null, dto, cancellationToken)
		?? throw new Exception("SendAsync (Create) failed.");

	public async Task<DM.Translation?> ReadAsync(
		string localeKey, string textKey, string formalityKey, string presentationKey,
		CancellationToken cancellationToken = default)
		=> await SendToRouteAsync<DM.Translation?>(HttpMethod.Get, ControllerName, $"{localeKey}/{textKey}/{formalityKey}/{presentationKey}", null, null, cancellationToken);

	public async Task<long> CountAsync(
		TranslationFilter filter,
		CancellationToken cancellationToken = default)
		=> await SendToRouteAsync<long>(HttpMethod.Get, ControllerName, ApiStrings.CountRoute, filter, null, cancellationToken);

	public async Task<PagedResponse<DM.Translation>> ListAsync(
		TranslationFilter filter,
		CancellationToken cancellationToken = default)
		=> await SendAsync<PagedResponse<DM.Translation>?>(HttpMethod.Get, ControllerName, filter, null, cancellationToken)
		?? throw new Exception("List returned a null result.");

	public async Task<UpdateResult> UpdateAsync(
		CM.TranslationDto dto,
		CancellationToken cancellationToken = default)
		=> await SendToRouteAsync<UpdateResult>(HttpMethod.Put, ControllerName, $"{dto.LocaleKey}/{dto.TextKey}/{dto.FormalityKey}/{dto.PresentationKey}", null, dto, cancellationToken)
		?? throw new Exception("SendToRouteAsync (Update) failed.");

	public async Task<DeleteResult> DeleteAsync(
		string localeKey, string textKey, string formalityKey, string presentationKey,
		CancellationToken cancellationToken = default)
		=> await SendToRouteAsync<DeleteResult>(HttpMethod.Delete, ControllerName, $"{localeKey}/{textKey}/{formalityKey}/{presentationKey}", null, null, cancellationToken)
		?? throw new Exception("SendToRouteAsync (Delete) failed.");

	public async Task<DeleteResult> DeleteAsync(
		CM.TranslationDto dto,
		CancellationToken cancellationToken = default)
		=> await DeleteAsync(dto.LocaleKey, dto.TextKey, dto.FormalityKey, dto.PresentationKey, cancellationToken);

	//

	public async Task<bool> TryCreateAsync(
		CM.TranslationDto dto,
		CancellationToken cancellationToken = default)
	{
		CreateResult<DM.Translation> result = await CreateAsync(dto, cancellationToken);

		return result.Succeeded;
	}

	public async Task<long> CountAllAsync(
		CancellationToken cancellationToken = default)
	{
		TranslationFilter filter = TranslationFilter.All();

		return await CountAsync(filter, cancellationToken);
	}

	public async Task<PagedResponse<DM.Translation>> ListAllAsync(
		CancellationToken cancellationToken = default)
	{
		TranslationFilter filter = TranslationFilter.All();

		return await ListAsync(filter, cancellationToken);
	}

	public async Task<IReadOnlyList<DM.Translation>> ListItemsAsync(
		TranslationFilter filter,
		CancellationToken cancellationToken = default)
	{
		PagedResponse<DM.Translation> response = await ListAsync(filter, cancellationToken);

		return response.Items;
	}

	public async Task<IReadOnlyList<DM.Translation>> ListAllItemsAsync(
		CancellationToken cancellationToken = default)
	{
		TranslationFilter filter = TranslationFilter.All();

		PagedResponse<DM.Translation> response = await ListAsync(filter, cancellationToken);

		return response.Items;
	}

	public async Task<DM.Translation> SingleAsync(
		TranslationFilter filter,
		CancellationToken cancellationToken = default)
	{
		IReadOnlyList<DM.Translation> items = await ListItemsAsync(filter, cancellationToken);

		return items.Single();		
	}

	public async Task<DM.Translation?> SingleOrDefaultAsync(
		TranslationFilter filter,
		CancellationToken cancellationToken = default)
	{
		IReadOnlyList<DM.Translation> items = await ListItemsAsync(filter, cancellationToken);

		return items.SingleOrDefault();		
	}

	public async Task<bool> TryUpdateAsync(
		CM.TranslationDto dto,
		CancellationToken cancellationToken = default)
	{
		UpdateResult result = await UpdateAsync(dto, cancellationToken);

		return result.Succeeded;
	}

	public async Task<bool> TryDeleteAsync(
		string localeKey, string textKey, string formalityKey, string presentationKey,
		CancellationToken cancellationToken = default)
	{
		DeleteResult result = await DeleteAsync(localeKey, textKey, formalityKey, presentationKey, cancellationToken);

		return result.Succeeded;
	}

	public async Task<bool> TryDeleteAsync(
		CM.TranslationDto dto,
		CancellationToken cancellationToken = default)
		=> await TryDeleteAsync(dto.LocaleKey, dto.TextKey, dto.FormalityKey, dto.PresentationKey, cancellationToken);

	//
}