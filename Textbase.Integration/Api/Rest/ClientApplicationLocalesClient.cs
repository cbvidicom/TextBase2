/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using Textbase.Application.Common;
using Textbase.Application.Features.ClientApplicationLocales;
using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using Uwn.Common.Net.Http;
using Uwn.Common.Querying;
using Uwn.EntityFrameworkCore.Infrastructure;

namespace Textbase.Integration.Api.Rest;

public sealed partial class ClientApplicationLocalesClient(
	string baseUrl,
	string? bearerToken = null,
	HttpClient? httpClient = null
	)
	: RestClientBase(baseUrl, bearerToken, httpClient)
	, IClientApplicationLocaleCommands
	, IClientApplicationLocaleQueries
	, IClientForController
{
	public string ControllerName => ApiStrings.ClientApplicationLocalesControllerName;

	//

	protected override bool ThrowOnNonSuccessStatusCode => false;

	//

	public async Task<CreateResult<DM.ClientApplicationLocale>> CreateAsync(
		CM.ClientApplicationLocaleDto dto,
		CancellationToken cancellationToken = default)
		=> await SendAsync<CreateResult<DM.ClientApplicationLocale>>(HttpMethod.Post, ControllerName, null, dto, cancellationToken)
		?? throw new Exception("SendAsync (Create) failed.");

	public async Task<DM.ClientApplicationLocale?> ReadAsync(
		Guid clientApplicationGuid, string localeKey,
		CancellationToken cancellationToken = default)
		=> await SendToRouteAsync<DM.ClientApplicationLocale?>(HttpMethod.Get, ControllerName, $"{clientApplicationGuid}/{localeKey}", null, null, cancellationToken);

	public async Task<long> CountAsync(
		ClientApplicationLocaleFilter filter,
		CancellationToken cancellationToken = default)
		=> await SendToRouteAsync<long>(HttpMethod.Get, ControllerName, ApiStrings.CountRoute, filter, null, cancellationToken);

	public async Task<PagedResponse<DM.ClientApplicationLocale>> ListAsync(
		ClientApplicationLocaleFilter filter,
		CancellationToken cancellationToken = default)
		=> await SendAsync<PagedResponse<DM.ClientApplicationLocale>?>(HttpMethod.Get, ControllerName, filter, null, cancellationToken)
		?? throw new Exception("List returned a null result.");

	public async Task<UpdateResult> UpdateAsync(
		CM.ClientApplicationLocaleDto dto,
		CancellationToken cancellationToken = default)
		=> await SendToRouteAsync<UpdateResult>(HttpMethod.Put, ControllerName, $"{dto.ClientApplicationGuid}/{dto.LocaleKey}", null, dto, cancellationToken)
		?? throw new Exception("SendToRouteAsync (Update) failed.");

	public async Task<DeleteResult> DeleteAsync(
		Guid clientApplicationGuid, string localeKey,
		CancellationToken cancellationToken = default)
		=> await SendToRouteAsync<DeleteResult>(HttpMethod.Delete, ControllerName, $"{clientApplicationGuid}/{localeKey}", null, null, cancellationToken)
		?? throw new Exception("SendToRouteAsync (Delete) failed.");

	public async Task<DeleteResult> DeleteAsync(
		CM.ClientApplicationLocaleDto dto,
		CancellationToken cancellationToken = default)
		=> await DeleteAsync(dto.ClientApplicationGuid, dto.LocaleKey, cancellationToken);

	//

	public async Task<bool> TryCreateAsync(
		CM.ClientApplicationLocaleDto dto,
		CancellationToken cancellationToken = default)
	{
		CreateResult<DM.ClientApplicationLocale> result = await CreateAsync(dto, cancellationToken);

		return result.Succeeded;
	}

	public async Task<long> CountAllAsync(
		CancellationToken cancellationToken = default)
	{
		ClientApplicationLocaleFilter filter = ClientApplicationLocaleFilter.All();

		return await CountAsync(filter, cancellationToken);
	}

	public async Task<PagedResponse<DM.ClientApplicationLocale>> ListAllAsync(
		CancellationToken cancellationToken = default)
	{
		ClientApplicationLocaleFilter filter = ClientApplicationLocaleFilter.All();

		return await ListAsync(filter, cancellationToken);
	}

	public async Task<IReadOnlyList<DM.ClientApplicationLocale>> ListItemsAsync(
		ClientApplicationLocaleFilter filter,
		CancellationToken cancellationToken = default)
	{
		PagedResponse<DM.ClientApplicationLocale> response = await ListAsync(filter, cancellationToken);

		return response.Items;
	}

	public async Task<IReadOnlyList<DM.ClientApplicationLocale>> ListAllItemsAsync(
		CancellationToken cancellationToken = default)
	{
		ClientApplicationLocaleFilter filter = ClientApplicationLocaleFilter.All();

		PagedResponse<DM.ClientApplicationLocale> response = await ListAsync(filter, cancellationToken);

		return response.Items;
	}

	public async Task<DM.ClientApplicationLocale> SingleAsync(
		ClientApplicationLocaleFilter filter,
		CancellationToken cancellationToken = default)
	{
		IReadOnlyList<DM.ClientApplicationLocale> items = await ListItemsAsync(filter, cancellationToken);

		return items.Single();		
	}

	public async Task<DM.ClientApplicationLocale?> SingleOrDefaultAsync(
		ClientApplicationLocaleFilter filter,
		CancellationToken cancellationToken = default)
	{
		IReadOnlyList<DM.ClientApplicationLocale> items = await ListItemsAsync(filter, cancellationToken);

		return items.SingleOrDefault();		
	}

	public async Task<bool> TryUpdateAsync(
		CM.ClientApplicationLocaleDto dto,
		CancellationToken cancellationToken = default)
	{
		UpdateResult result = await UpdateAsync(dto, cancellationToken);

		return result.Succeeded;
	}

	public async Task<bool> TryDeleteAsync(
		Guid clientApplicationGuid, string localeKey,
		CancellationToken cancellationToken = default)
	{
		DeleteResult result = await DeleteAsync(clientApplicationGuid, localeKey, cancellationToken);

		return result.Succeeded;
	}

	public async Task<bool> TryDeleteAsync(
		CM.ClientApplicationLocaleDto dto,
		CancellationToken cancellationToken = default)
		=> await TryDeleteAsync(dto.ClientApplicationGuid, dto.LocaleKey, cancellationToken);

	//
	public async Task<DM.ClientApplicationLocale?> ReadByClientApplicationGuidAsync(
		Guid key,
		CancellationToken cancellationToken = default)
		=> await SendToRouteAsync<DM.ClientApplicationLocale?>(HttpMethod.Get, ControllerName, $"ByClientApplicationGuid/{key}", null, null, cancellationToken);
	
}