/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using Textbase.Application.Common;
using Textbase.Application.Features.Formalities;
using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using Uwn.Common.Net.Http;
using Uwn.Common.Querying;
using Uwn.EntityFrameworkCore.Infrastructure;

namespace Textbase.Integration.Api.Rest;

public sealed partial class FormalitiesClient(
	string baseUrl,
	string? bearerToken = null,
	HttpClient? httpClient = null
	)
	: RestClientBase(baseUrl, bearerToken, httpClient)
	, IFormalityCommands
	, IFormalityQueries
	, IClientForController
{
	public string ControllerName => ApiStrings.FormalitiesControllerName;

	//

	protected override bool ThrowOnNonSuccessStatusCode => false;

	//

	public async Task<CreateResult<DM.Formality>> CreateAsync(
		CM.FormalityDto dto,
		CancellationToken cancellationToken = default)
		=> await SendAsync<CreateResult<DM.Formality>>(HttpMethod.Post, ControllerName, null, dto, cancellationToken)
		?? throw new Exception("SendAsync (Create) failed.");

	public async Task<DM.Formality?> ReadAsync(
		string formalityKey,
		CancellationToken cancellationToken = default)
		=> await SendToRouteAsync<DM.Formality?>(HttpMethod.Get, ControllerName, $"{formalityKey}", null, null, cancellationToken);

	public async Task<long> CountAsync(
		FormalityFilter filter,
		CancellationToken cancellationToken = default)
		=> await SendToRouteAsync<long>(HttpMethod.Get, ControllerName, ApiStrings.CountRoute, filter, null, cancellationToken);

	public async Task<PagedResponse<DM.Formality>> ListAsync(
		FormalityFilter filter,
		CancellationToken cancellationToken = default)
		=> await SendAsync<PagedResponse<DM.Formality>?>(HttpMethod.Get, ControllerName, filter, null, cancellationToken)
		?? throw new Exception("List returned a null result.");

	public async Task<UpdateResult> UpdateAsync(
		CM.FormalityDto dto,
		CancellationToken cancellationToken = default)
		=> await SendToRouteAsync<UpdateResult>(HttpMethod.Put, ControllerName, $"{dto.FormalityKey}", null, dto, cancellationToken)
		?? throw new Exception("SendToRouteAsync (Update) failed.");

	public async Task<DeleteResult> DeleteAsync(
		string formalityKey,
		CancellationToken cancellationToken = default)
		=> await SendToRouteAsync<DeleteResult>(HttpMethod.Delete, ControllerName, $"{formalityKey}", null, null, cancellationToken)
		?? throw new Exception("SendToRouteAsync (Delete) failed.");

	public async Task<DeleteResult> DeleteAsync(
		CM.FormalityDto dto,
		CancellationToken cancellationToken = default)
		=> await DeleteAsync(dto.FormalityKey, cancellationToken);

	//

	public async Task<bool> TryCreateAsync(
		CM.FormalityDto dto,
		CancellationToken cancellationToken = default)
	{
		CreateResult<DM.Formality> result = await CreateAsync(dto, cancellationToken);

		return result.Succeeded;
	}

	public async Task<long> CountAllAsync(
		CancellationToken cancellationToken = default)
	{
		FormalityFilter filter = FormalityFilter.All();

		return await CountAsync(filter, cancellationToken);
	}

	public async Task<PagedResponse<DM.Formality>> ListAllAsync(
		CancellationToken cancellationToken = default)
	{
		FormalityFilter filter = FormalityFilter.All();

		return await ListAsync(filter, cancellationToken);
	}

	public async Task<IReadOnlyList<DM.Formality>> ListItemsAsync(
		FormalityFilter filter,
		CancellationToken cancellationToken = default)
	{
		PagedResponse<DM.Formality> response = await ListAsync(filter, cancellationToken);

		return response.Items;
	}

	public async Task<IReadOnlyList<DM.Formality>> ListAllItemsAsync(
		CancellationToken cancellationToken = default)
	{
		FormalityFilter filter = FormalityFilter.All();

		PagedResponse<DM.Formality> response = await ListAsync(filter, cancellationToken);

		return response.Items;
	}

	public async Task<DM.Formality> SingleAsync(
		FormalityFilter filter,
		CancellationToken cancellationToken = default)
	{
		IReadOnlyList<DM.Formality> items = await ListItemsAsync(filter, cancellationToken);

		return items.Single();		
	}

	public async Task<DM.Formality?> SingleOrDefaultAsync(
		FormalityFilter filter,
		CancellationToken cancellationToken = default)
	{
		IReadOnlyList<DM.Formality> items = await ListItemsAsync(filter, cancellationToken);

		return items.SingleOrDefault();		
	}

	public async Task<bool> TryUpdateAsync(
		CM.FormalityDto dto,
		CancellationToken cancellationToken = default)
	{
		UpdateResult result = await UpdateAsync(dto, cancellationToken);

		return result.Succeeded;
	}

	public async Task<bool> TryDeleteAsync(
		string formalityKey,
		CancellationToken cancellationToken = default)
	{
		DeleteResult result = await DeleteAsync(formalityKey, cancellationToken);

		return result.Succeeded;
	}

	public async Task<bool> TryDeleteAsync(
		CM.FormalityDto dto,
		CancellationToken cancellationToken = default)
		=> await TryDeleteAsync(dto.FormalityKey, cancellationToken);

	//
}