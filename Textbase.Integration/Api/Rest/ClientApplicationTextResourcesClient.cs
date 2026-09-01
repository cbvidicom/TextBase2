/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using Textbase.Application.Common;
using Textbase.Application.Features.ClientApplicationTextResources;
using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using Uwn.Common.Net.Http;
using Uwn.Common.Querying;
using Uwn.EntityFrameworkCore.Infrastructure;

namespace Textbase.Integration.Api.Rest;

public sealed partial class ClientApplicationTextResourcesClient(
	string baseUrl,
	string? bearerToken = null,
	HttpClient? httpClient = null
	)
	: RestClientBase(baseUrl, bearerToken, httpClient)
	, IClientApplicationTextResourceCommands
	, IClientApplicationTextResourceQueries
	, IClientForController
{
	public string ControllerName => ApiStrings.ClientApplicationTextResourcesControllerName;

	//

	protected override bool ThrowOnNonSuccessStatusCode => false;

	//

	public async Task<CreateResult<DM.ClientApplicationTextResource>> CreateAsync(
		CM.ClientApplicationTextResourceDto dto,
		CancellationToken cancellationToken = default)
		=> await SendAsync<CreateResult<DM.ClientApplicationTextResource>>(HttpMethod.Post, ControllerName, null, dto, cancellationToken)
		?? throw new Exception("SendAsync (Create) failed.");

	public async Task<DM.ClientApplicationTextResource?> ReadAsync(
		Guid clientApplicationGuid, string textKey,
		CancellationToken cancellationToken = default)
		=> await SendToRouteAsync<DM.ClientApplicationTextResource?>(HttpMethod.Get, ControllerName, $"{clientApplicationGuid}/{textKey}", null, null, cancellationToken);

	public async Task<long> CountAsync(
		ClientApplicationTextResourceFilter filter,
		CancellationToken cancellationToken = default)
		=> await SendToRouteAsync<long>(HttpMethod.Get, ControllerName, ApiStrings.CountRoute, filter, null, cancellationToken);

	public async Task<PagedResponse<DM.ClientApplicationTextResource>> ListAsync(
		ClientApplicationTextResourceFilter filter,
		CancellationToken cancellationToken = default)
		=> await SendAsync<PagedResponse<DM.ClientApplicationTextResource>?>(HttpMethod.Get, ControllerName, filter, null, cancellationToken)
		?? throw new Exception("List returned a null result.");

	public async Task<UpdateResult> UpdateAsync(
		CM.ClientApplicationTextResourceDto dto,
		CancellationToken cancellationToken = default)
		=> await SendToRouteAsync<UpdateResult>(HttpMethod.Put, ControllerName, $"{dto.ClientApplicationGuid}/{dto.TextKey}", null, dto, cancellationToken)
		?? throw new Exception("SendToRouteAsync (Update) failed.");

	public async Task<DeleteResult> DeleteAsync(
		Guid clientApplicationGuid, string textKey,
		CancellationToken cancellationToken = default)
		=> await SendToRouteAsync<DeleteResult>(HttpMethod.Delete, ControllerName, $"{clientApplicationGuid}/{textKey}", null, null, cancellationToken)
		?? throw new Exception("SendToRouteAsync (Delete) failed.");

	public async Task<DeleteResult> DeleteAsync(
		CM.ClientApplicationTextResourceDto dto,
		CancellationToken cancellationToken = default)
		=> await DeleteAsync(dto.ClientApplicationGuid, dto.TextKey, cancellationToken);

	//

	public async Task<bool> TryCreateAsync(
		CM.ClientApplicationTextResourceDto dto,
		CancellationToken cancellationToken = default)
	{
		CreateResult<DM.ClientApplicationTextResource> result = await CreateAsync(dto, cancellationToken);

		return result.Succeeded;
	}

	public async Task<long> CountAllAsync(
		CancellationToken cancellationToken = default)
	{
		ClientApplicationTextResourceFilter filter = ClientApplicationTextResourceFilter.All();

		return await CountAsync(filter, cancellationToken);
	}

	public async Task<PagedResponse<DM.ClientApplicationTextResource>> ListAllAsync(
		CancellationToken cancellationToken = default)
	{
		ClientApplicationTextResourceFilter filter = ClientApplicationTextResourceFilter.All();

		return await ListAsync(filter, cancellationToken);
	}

	public async Task<IReadOnlyList<DM.ClientApplicationTextResource>> ListItemsAsync(
		ClientApplicationTextResourceFilter filter,
		CancellationToken cancellationToken = default)
	{
		PagedResponse<DM.ClientApplicationTextResource> response = await ListAsync(filter, cancellationToken);

		return response.Items;
	}

	public async Task<IReadOnlyList<DM.ClientApplicationTextResource>> ListAllItemsAsync(
		CancellationToken cancellationToken = default)
	{
		ClientApplicationTextResourceFilter filter = ClientApplicationTextResourceFilter.All();

		PagedResponse<DM.ClientApplicationTextResource> response = await ListAsync(filter, cancellationToken);

		return response.Items;
	}

	public async Task<DM.ClientApplicationTextResource> SingleAsync(
		ClientApplicationTextResourceFilter filter,
		CancellationToken cancellationToken = default)
	{
		IReadOnlyList<DM.ClientApplicationTextResource> items = await ListItemsAsync(filter, cancellationToken);

		return items.Single();		
	}

	public async Task<DM.ClientApplicationTextResource?> SingleOrDefaultAsync(
		ClientApplicationTextResourceFilter filter,
		CancellationToken cancellationToken = default)
	{
		IReadOnlyList<DM.ClientApplicationTextResource> items = await ListItemsAsync(filter, cancellationToken);

		return items.SingleOrDefault();		
	}

	public async Task<bool> TryUpdateAsync(
		CM.ClientApplicationTextResourceDto dto,
		CancellationToken cancellationToken = default)
	{
		UpdateResult result = await UpdateAsync(dto, cancellationToken);

		return result.Succeeded;
	}

	public async Task<bool> TryDeleteAsync(
		Guid clientApplicationGuid, string textKey,
		CancellationToken cancellationToken = default)
	{
		DeleteResult result = await DeleteAsync(clientApplicationGuid, textKey, cancellationToken);

		return result.Succeeded;
	}

	public async Task<bool> TryDeleteAsync(
		CM.ClientApplicationTextResourceDto dto,
		CancellationToken cancellationToken = default)
		=> await TryDeleteAsync(dto.ClientApplicationGuid, dto.TextKey, cancellationToken);

	//
}