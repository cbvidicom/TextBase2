/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using Textbase.Application.Common;
using Textbase.Application.Features.Locales;
using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using Uwn.Common.Net.Http;
using Uwn.Common.Querying;
using Uwn.EntityFrameworkCore.Infrastructure;

namespace Textbase.Integration.Api.Rest;

public sealed partial class LocalesClient(
	string baseUrl,
	string? bearerToken = null,
	HttpClient? httpClient = null
	)
	: RestClientBase(baseUrl, bearerToken, httpClient)
	, ILocaleCommands
	, ILocaleQueries
	, IClientForController
{
	public string ControllerName => ApiStrings.LocalesControllerName;

	//

	protected override bool ThrowOnNonSuccessStatusCode => false;

	//

	public async Task<CreateResult<DM.Locale>> CreateAsync(
		CM.LocaleDto dto,
		CancellationToken cancellationToken = default)
		=> await SendAsync<CreateResult<DM.Locale>>(HttpMethod.Post, ControllerName, null, dto, cancellationToken)
		?? throw new Exception("SendAsync (Create) failed.");

	public async Task<DM.Locale?> ReadAsync(
		string localeKey,
		CancellationToken cancellationToken = default)
		=> await SendToRouteAsync<DM.Locale?>(HttpMethod.Get, ControllerName, $"{localeKey}", null, null, cancellationToken);

	public async Task<long> CountAsync(
		LocaleFilter filter,
		CancellationToken cancellationToken = default)
		=> await SendToRouteAsync<long>(HttpMethod.Get, ControllerName, ApiStrings.CountRoute, filter, null, cancellationToken);

	public async Task<PagedResponse<DM.Locale>> ListAsync(
		LocaleFilter filter,
		CancellationToken cancellationToken = default)
		=> await SendAsync<PagedResponse<DM.Locale>?>(HttpMethod.Get, ControllerName, filter, null, cancellationToken)
		?? throw new Exception("List returned a null result.");

	public async Task<UpdateResult> UpdateAsync(
		CM.LocaleDto dto,
		CancellationToken cancellationToken = default)
		=> await SendToRouteAsync<UpdateResult>(HttpMethod.Put, ControllerName, $"{dto.LocaleKey}", null, dto, cancellationToken)
		?? throw new Exception("SendToRouteAsync (Update) failed.");

	public async Task<DeleteResult> DeleteAsync(
		string localeKey,
		CancellationToken cancellationToken = default)
		=> await SendToRouteAsync<DeleteResult>(HttpMethod.Delete, ControllerName, $"{localeKey}", null, null, cancellationToken)
		?? throw new Exception("SendToRouteAsync (Delete) failed.");

	public async Task<DeleteResult> DeleteAsync(
		CM.LocaleDto dto,
		CancellationToken cancellationToken = default)
		=> await DeleteAsync(dto.LocaleKey, cancellationToken);

	//

	public async Task<bool> TryCreateAsync(
		CM.LocaleDto dto,
		CancellationToken cancellationToken = default)
	{
		CreateResult<DM.Locale> result = await CreateAsync(dto, cancellationToken);

		return result.Succeeded;
	}

	public async Task<long> CountAllAsync(
		CancellationToken cancellationToken = default)
	{
		LocaleFilter filter = LocaleFilter.All();

		return await CountAsync(filter, cancellationToken);
	}

	public async Task<PagedResponse<DM.Locale>> ListAllAsync(
		CancellationToken cancellationToken = default)
	{
		LocaleFilter filter = LocaleFilter.All();

		return await ListAsync(filter, cancellationToken);
	}

	public async Task<IReadOnlyList<DM.Locale>> ListItemsAsync(
		LocaleFilter filter,
		CancellationToken cancellationToken = default)
	{
		PagedResponse<DM.Locale> response = await ListAsync(filter, cancellationToken);

		return response.Items;
	}

	public async Task<IReadOnlyList<DM.Locale>> ListAllItemsAsync(
		CancellationToken cancellationToken = default)
	{
		LocaleFilter filter = LocaleFilter.All();

		PagedResponse<DM.Locale> response = await ListAsync(filter, cancellationToken);

		return response.Items;
	}

	public async Task<DM.Locale> SingleAsync(
		LocaleFilter filter,
		CancellationToken cancellationToken = default)
	{
		IReadOnlyList<DM.Locale> items = await ListItemsAsync(filter, cancellationToken);

		return items.Single();		
	}

	public async Task<DM.Locale?> SingleOrDefaultAsync(
		LocaleFilter filter,
		CancellationToken cancellationToken = default)
	{
		IReadOnlyList<DM.Locale> items = await ListItemsAsync(filter, cancellationToken);

		return items.SingleOrDefault();		
	}

	public async Task<bool> TryUpdateAsync(
		CM.LocaleDto dto,
		CancellationToken cancellationToken = default)
	{
		UpdateResult result = await UpdateAsync(dto, cancellationToken);

		return result.Succeeded;
	}

	public async Task<bool> TryDeleteAsync(
		string localeKey,
		CancellationToken cancellationToken = default)
	{
		DeleteResult result = await DeleteAsync(localeKey, cancellationToken);

		return result.Succeeded;
	}

	public async Task<bool> TryDeleteAsync(
		CM.LocaleDto dto,
		CancellationToken cancellationToken = default)
		=> await TryDeleteAsync(dto.LocaleKey, cancellationToken);

	//
}