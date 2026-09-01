/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using Textbase.Application.Common;
using Textbase.Application.Features.ClientApplications;
using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using Uwn.Common.Net.Http;
using Uwn.Common.Querying;
using Uwn.EntityFrameworkCore.Infrastructure;

namespace Textbase.Integration.Api.Rest;

public sealed partial class ClientApplicationsClient(
	string baseUrl,
	string? bearerToken = null,
	HttpClient? httpClient = null
	)
	: RestClientBase(baseUrl, bearerToken, httpClient)
	, IClientApplicationCommands
	, IClientApplicationQueries
	, IClientForController
{
	public string ControllerName => ApiStrings.ClientApplicationsControllerName;

	//

	protected override bool ThrowOnNonSuccessStatusCode => false;

	//

	public async Task<CreateResult<DM.ClientApplication>> CreateAsync(
		CM.ClientApplicationDto dto,
		CancellationToken cancellationToken = default)
		=> await SendAsync<CreateResult<DM.ClientApplication>>(HttpMethod.Post, ControllerName, null, dto, cancellationToken)
		?? throw new Exception("SendAsync (Create) failed.");

	public async Task<DM.ClientApplication?> ReadAsync(
		Guid clientApplicationGuid,
		CancellationToken cancellationToken = default)
		=> await SendToRouteAsync<DM.ClientApplication?>(HttpMethod.Get, ControllerName, $"{clientApplicationGuid}", null, null, cancellationToken);

	public async Task<long> CountAsync(
		ClientApplicationFilter filter,
		CancellationToken cancellationToken = default)
		=> await SendToRouteAsync<long>(HttpMethod.Get, ControllerName, ApiStrings.CountRoute, filter, null, cancellationToken);

	public async Task<PagedResponse<DM.ClientApplication>> ListAsync(
		ClientApplicationFilter filter,
		CancellationToken cancellationToken = default)
		=> await SendAsync<PagedResponse<DM.ClientApplication>?>(HttpMethod.Get, ControllerName, filter, null, cancellationToken)
		?? throw new Exception("List returned a null result.");

	public async Task<UpdateResult> UpdateAsync(
		CM.ClientApplicationDto dto,
		CancellationToken cancellationToken = default)
		=> await SendToRouteAsync<UpdateResult>(HttpMethod.Put, ControllerName, $"{dto.ClientApplicationGuid}", null, dto, cancellationToken)
		?? throw new Exception("SendToRouteAsync (Update) failed.");

	public async Task<DeleteResult> DeleteAsync(
		Guid clientApplicationGuid,
		CancellationToken cancellationToken = default)
		=> await SendToRouteAsync<DeleteResult>(HttpMethod.Delete, ControllerName, $"{clientApplicationGuid}", null, null, cancellationToken)
		?? throw new Exception("SendToRouteAsync (Delete) failed.");

	public async Task<DeleteResult> DeleteAsync(
		CM.ClientApplicationDto dto,
		CancellationToken cancellationToken = default)
		=> await DeleteAsync(dto.ClientApplicationGuid, cancellationToken);

	//

	public async Task<bool> TryCreateAsync(
		CM.ClientApplicationDto dto,
		CancellationToken cancellationToken = default)
	{
		CreateResult<DM.ClientApplication> result = await CreateAsync(dto, cancellationToken);

		return result.Succeeded;
	}

	public async Task<long> CountAllAsync(
		CancellationToken cancellationToken = default)
	{
		ClientApplicationFilter filter = ClientApplicationFilter.All();

		return await CountAsync(filter, cancellationToken);
	}

	public async Task<PagedResponse<DM.ClientApplication>> ListAllAsync(
		CancellationToken cancellationToken = default)
	{
		ClientApplicationFilter filter = ClientApplicationFilter.All();

		return await ListAsync(filter, cancellationToken);
	}

	public async Task<IReadOnlyList<DM.ClientApplication>> ListItemsAsync(
		ClientApplicationFilter filter,
		CancellationToken cancellationToken = default)
	{
		PagedResponse<DM.ClientApplication> response = await ListAsync(filter, cancellationToken);

		return response.Items;
	}

	public async Task<IReadOnlyList<DM.ClientApplication>> ListAllItemsAsync(
		CancellationToken cancellationToken = default)
	{
		ClientApplicationFilter filter = ClientApplicationFilter.All();

		PagedResponse<DM.ClientApplication> response = await ListAsync(filter, cancellationToken);

		return response.Items;
	}

	public async Task<DM.ClientApplication> SingleAsync(
		ClientApplicationFilter filter,
		CancellationToken cancellationToken = default)
	{
		IReadOnlyList<DM.ClientApplication> items = await ListItemsAsync(filter, cancellationToken);

		return items.Single();		
	}

	public async Task<DM.ClientApplication?> SingleOrDefaultAsync(
		ClientApplicationFilter filter,
		CancellationToken cancellationToken = default)
	{
		IReadOnlyList<DM.ClientApplication> items = await ListItemsAsync(filter, cancellationToken);

		return items.SingleOrDefault();		
	}

	public async Task<bool> TryUpdateAsync(
		CM.ClientApplicationDto dto,
		CancellationToken cancellationToken = default)
	{
		UpdateResult result = await UpdateAsync(dto, cancellationToken);

		return result.Succeeded;
	}

	public async Task<bool> TryDeleteAsync(
		Guid clientApplicationGuid,
		CancellationToken cancellationToken = default)
	{
		DeleteResult result = await DeleteAsync(clientApplicationGuid, cancellationToken);

		return result.Succeeded;
	}

	public async Task<bool> TryDeleteAsync(
		CM.ClientApplicationDto dto,
		CancellationToken cancellationToken = default)
		=> await TryDeleteAsync(dto.ClientApplicationGuid, cancellationToken);

	//
}