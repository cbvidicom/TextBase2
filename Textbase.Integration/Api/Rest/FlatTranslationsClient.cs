/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using Textbase.Application.Common;
using Textbase.Application.Features.FlatTranslations;
using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using Uwn.Common.Net.Http;
using Uwn.Common.Querying;
using Uwn.EntityFrameworkCore.Infrastructure;

namespace Textbase.Integration.Api.Rest;

public sealed partial class FlatTranslationsClient(
	string baseUrl,
	string? bearerToken = null,
	HttpClient? httpClient = null
	)
	: RestClientBase(baseUrl, bearerToken, httpClient)
	, IFlatTranslationCommands
	, IFlatTranslationQueries
	, IClientForController
{
	public string ControllerName => ApiStrings.FlatTranslationsControllerName;

	//

	protected override bool ThrowOnNonSuccessStatusCode => false;

	//

	public async Task<CreateResult<DM.FlatTranslation>> CreateAsync(
		CM.FlatTranslationDto dto,
		CancellationToken cancellationToken = default)
		=> await SendAsync<CreateResult<DM.FlatTranslation>>(HttpMethod.Post, ControllerName, null, dto, cancellationToken)
		?? throw new Exception("SendAsync (Create) failed.");

	public async Task<DM.FlatTranslation?> ReadAsync(
		string localeKey, string textKey, string formalityKey, string presentationKey,
		CancellationToken cancellationToken = default)
		=> await SendToRouteAsync<DM.FlatTranslation?>(HttpMethod.Get, ControllerName, $"{localeKey}/{textKey}/{formalityKey}/{presentationKey}", null, null, cancellationToken);

	public async Task<long> CountAsync(
		FlatTranslationFilter filter,
		CancellationToken cancellationToken = default)
		=> await SendToRouteAsync<long>(HttpMethod.Get, ControllerName, ApiStrings.CountRoute, filter, null, cancellationToken);

	public async Task<PagedResponse<DM.FlatTranslation>> ListAsync(
		FlatTranslationFilter filter,
		CancellationToken cancellationToken = default)
		=> await SendAsync<PagedResponse<DM.FlatTranslation>?>(HttpMethod.Get, ControllerName, filter, null, cancellationToken)
		?? throw new Exception("List returned a null result.");

	public async Task<UpdateResult> UpdateAsync(
		CM.FlatTranslationDto dto,
		CancellationToken cancellationToken = default)
		=> await SendToRouteAsync<UpdateResult>(HttpMethod.Put, ControllerName, $"{dto.LocaleKey}/{dto.TextKey}/{dto.FormalityKey}/{dto.PresentationKey}", null, dto, cancellationToken)
		?? throw new Exception("SendToRouteAsync (Update) failed.");

	public async Task<DeleteResult> DeleteAsync(
		string localeKey, string textKey, string formalityKey, string presentationKey,
		CancellationToken cancellationToken = default)
		=> await SendToRouteAsync<DeleteResult>(HttpMethod.Delete, ControllerName, $"{localeKey}/{textKey}/{formalityKey}/{presentationKey}", null, null, cancellationToken)
		?? throw new Exception("SendToRouteAsync (Delete) failed.");

	public async Task<DeleteResult> DeleteAsync(
		CM.FlatTranslationDto dto,
		CancellationToken cancellationToken = default)
		=> await DeleteAsync(dto.LocaleKey, dto.TextKey, dto.FormalityKey, dto.PresentationKey, cancellationToken);

	//

	public async Task<bool> TryCreateAsync(
		CM.FlatTranslationDto dto,
		CancellationToken cancellationToken = default)
	{
		CreateResult<DM.FlatTranslation> result = await CreateAsync(dto, cancellationToken);

		return result.Succeeded;
	}

	public async Task<long> CountAllAsync(
		CancellationToken cancellationToken = default)
	{
		FlatTranslationFilter filter = FlatTranslationFilter.All();

		return await CountAsync(filter, cancellationToken);
	}

	public async Task<PagedResponse<DM.FlatTranslation>> ListAllAsync(
		CancellationToken cancellationToken = default)
	{
		FlatTranslationFilter filter = FlatTranslationFilter.All();

		return await ListAsync(filter, cancellationToken);
	}

	public async Task<IReadOnlyList<DM.FlatTranslation>> ListItemsAsync(
		FlatTranslationFilter filter,
		CancellationToken cancellationToken = default)
	{
		PagedResponse<DM.FlatTranslation> response = await ListAsync(filter, cancellationToken);

		return response.Items;
	}

	public async Task<IReadOnlyList<DM.FlatTranslation>> ListAllItemsAsync(
		CancellationToken cancellationToken = default)
	{
		FlatTranslationFilter filter = FlatTranslationFilter.All();

		PagedResponse<DM.FlatTranslation> response = await ListAsync(filter, cancellationToken);

		return response.Items;
	}

	public async Task<DM.FlatTranslation> SingleAsync(
		FlatTranslationFilter filter,
		CancellationToken cancellationToken = default)
	{
		IReadOnlyList<DM.FlatTranslation> items = await ListItemsAsync(filter, cancellationToken);

		return items.Single();		
	}

	public async Task<DM.FlatTranslation?> SingleOrDefaultAsync(
		FlatTranslationFilter filter,
		CancellationToken cancellationToken = default)
	{
		IReadOnlyList<DM.FlatTranslation> items = await ListItemsAsync(filter, cancellationToken);

		return items.SingleOrDefault();		
	}

	public async Task<bool> TryUpdateAsync(
		CM.FlatTranslationDto dto,
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
		CM.FlatTranslationDto dto,
		CancellationToken cancellationToken = default)
		=> await TryDeleteAsync(dto.LocaleKey, dto.TextKey, dto.FormalityKey, dto.PresentationKey, cancellationToken);

	//
}