/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using Textbase.Application.Common;
using Textbase.Application.Features.AuthPrincipalLocales;
using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using Uwn.Common.Net.Http;
using Uwn.Common.Querying;
using Uwn.EntityFrameworkCore.Infrastructure;

namespace Textbase.Integration.Api.Rest;

public sealed partial class AuthPrincipalLocalesClient(
	string baseUrl,
	string? bearerToken = null,
	HttpClient? httpClient = null
	)
	: RestClientBase(baseUrl, bearerToken, httpClient)
	, IAuthPrincipalLocaleCommands
	, IAuthPrincipalLocaleQueries
	, IClientForController
{
	public string ControllerName => ApiStrings.AuthPrincipalLocalesControllerName;

	//

	protected override bool ThrowOnNonSuccessStatusCode => false;

	//

	public async Task<CreateResult<DM.AuthPrincipalLocale>> CreateAsync(
		CM.AuthPrincipalLocaleDto dto,
		CancellationToken cancellationToken = default)
		=> await SendAsync<CreateResult<DM.AuthPrincipalLocale>>(HttpMethod.Post, ControllerName, null, dto, cancellationToken)
		?? throw new Exception("SendAsync (Create) failed.");

	public async Task<DM.AuthPrincipalLocale?> ReadAsync(
		Guid entraObjectId, string localeKey,
		CancellationToken cancellationToken = default)
		=> await SendToRouteAsync<DM.AuthPrincipalLocale?>(HttpMethod.Get, ControllerName, $"{entraObjectId}/{localeKey}", null, null, cancellationToken);

	public async Task<long> CountAsync(
		AuthPrincipalLocaleFilter filter,
		CancellationToken cancellationToken = default)
		=> await SendToRouteAsync<long>(HttpMethod.Get, ControllerName, ApiStrings.CountRoute, filter, null, cancellationToken);

	public async Task<PagedResponse<DM.AuthPrincipalLocale>> ListAsync(
		AuthPrincipalLocaleFilter filter,
		CancellationToken cancellationToken = default)
		=> await SendAsync<PagedResponse<DM.AuthPrincipalLocale>?>(HttpMethod.Get, ControllerName, filter, null, cancellationToken)
		?? throw new Exception("List returned a null result.");

	public async Task<UpdateResult> UpdateAsync(
		CM.AuthPrincipalLocaleDto dto,
		CancellationToken cancellationToken = default)
		=> await SendToRouteAsync<UpdateResult>(HttpMethod.Put, ControllerName, $"{dto.EntraObjectId}/{dto.LocaleKey}", null, dto, cancellationToken)
		?? throw new Exception("SendToRouteAsync (Update) failed.");

	public async Task<DeleteResult> DeleteAsync(
		Guid entraObjectId, string localeKey,
		CancellationToken cancellationToken = default)
		=> await SendToRouteAsync<DeleteResult>(HttpMethod.Delete, ControllerName, $"{entraObjectId}/{localeKey}", null, null, cancellationToken)
		?? throw new Exception("SendToRouteAsync (Delete) failed.");

	public async Task<DeleteResult> DeleteAsync(
		CM.AuthPrincipalLocaleDto dto,
		CancellationToken cancellationToken = default)
		=> await DeleteAsync(dto.EntraObjectId, dto.LocaleKey, cancellationToken);

	//

	public async Task<bool> TryCreateAsync(
		CM.AuthPrincipalLocaleDto dto,
		CancellationToken cancellationToken = default)
	{
		CreateResult<DM.AuthPrincipalLocale> result = await CreateAsync(dto, cancellationToken);

		return result.Succeeded;
	}

	public async Task<long> CountAllAsync(
		CancellationToken cancellationToken = default)
	{
		AuthPrincipalLocaleFilter filter = AuthPrincipalLocaleFilter.All();

		return await CountAsync(filter, cancellationToken);
	}

	public async Task<PagedResponse<DM.AuthPrincipalLocale>> ListAllAsync(
		CancellationToken cancellationToken = default)
	{
		AuthPrincipalLocaleFilter filter = AuthPrincipalLocaleFilter.All();

		return await ListAsync(filter, cancellationToken);
	}

	public async Task<IReadOnlyList<DM.AuthPrincipalLocale>> ListItemsAsync(
		AuthPrincipalLocaleFilter filter,
		CancellationToken cancellationToken = default)
	{
		PagedResponse<DM.AuthPrincipalLocale> response = await ListAsync(filter, cancellationToken);

		return response.Items;
	}

	public async Task<IReadOnlyList<DM.AuthPrincipalLocale>> ListAllItemsAsync(
		CancellationToken cancellationToken = default)
	{
		AuthPrincipalLocaleFilter filter = AuthPrincipalLocaleFilter.All();

		PagedResponse<DM.AuthPrincipalLocale> response = await ListAsync(filter, cancellationToken);

		return response.Items;
	}

	public async Task<DM.AuthPrincipalLocale> SingleAsync(
		AuthPrincipalLocaleFilter filter,
		CancellationToken cancellationToken = default)
	{
		IReadOnlyList<DM.AuthPrincipalLocale> items = await ListItemsAsync(filter, cancellationToken);

		return items.Single();		
	}

	public async Task<DM.AuthPrincipalLocale?> SingleOrDefaultAsync(
		AuthPrincipalLocaleFilter filter,
		CancellationToken cancellationToken = default)
	{
		IReadOnlyList<DM.AuthPrincipalLocale> items = await ListItemsAsync(filter, cancellationToken);

		return items.SingleOrDefault();		
	}

	public async Task<bool> TryUpdateAsync(
		CM.AuthPrincipalLocaleDto dto,
		CancellationToken cancellationToken = default)
	{
		UpdateResult result = await UpdateAsync(dto, cancellationToken);

		return result.Succeeded;
	}

	public async Task<bool> TryDeleteAsync(
		Guid entraObjectId, string localeKey,
		CancellationToken cancellationToken = default)
	{
		DeleteResult result = await DeleteAsync(entraObjectId, localeKey, cancellationToken);

		return result.Succeeded;
	}

	public async Task<bool> TryDeleteAsync(
		CM.AuthPrincipalLocaleDto dto,
		CancellationToken cancellationToken = default)
		=> await TryDeleteAsync(dto.EntraObjectId, dto.LocaleKey, cancellationToken);

	//
}