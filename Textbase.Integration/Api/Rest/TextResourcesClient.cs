/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using Textbase.Application.Common;
using Textbase.Application.Features.TextResources;
using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using Uwn.Common.Net.Http;
using Uwn.Common.Querying;
using Uwn.EntityFrameworkCore.Infrastructure;

namespace Textbase.Integration.Api.Rest;

public sealed partial class TextResourcesClient(
	string baseUrl,
	string? bearerToken = null,
	HttpClient? httpClient = null
	)
	: RestClientBase(baseUrl, bearerToken, httpClient)
	, ITextResourceCommands
	, ITextResourceQueries
	, IClientForController
{
	public string ControllerName => ApiStrings.TextResourcesControllerName;

	//

	protected override bool ThrowOnNonSuccessStatusCode => false;

	//

	public async Task<CreateResult<DM.TextResource>> CreateAsync(
		CM.TextResourceDto dto,
		CancellationToken cancellationToken = default)
		=> await SendAsync<CreateResult<DM.TextResource>>(HttpMethod.Post, ControllerName, null, dto, cancellationToken)
		?? throw new Exception("SendAsync (Create) failed.");

	public async Task<DM.TextResource?> ReadAsync(
		string textKey,
		CancellationToken cancellationToken = default)
		=> await SendToRouteAsync<DM.TextResource?>(HttpMethod.Get, ControllerName, $"{textKey}", null, null, cancellationToken);

	public async Task<long> CountAsync(
		TextResourceFilter filter,
		CancellationToken cancellationToken = default)
		=> await SendToRouteAsync<long>(HttpMethod.Get, ControllerName, ApiStrings.CountRoute, filter, null, cancellationToken);

	public async Task<PagedResponse<DM.TextResource>> ListAsync(
		TextResourceFilter filter,
		CancellationToken cancellationToken = default)
		=> await SendAsync<PagedResponse<DM.TextResource>?>(HttpMethod.Get, ControllerName, filter, null, cancellationToken)
		?? throw new Exception("List returned a null result.");

	public async Task<UpdateResult> UpdateAsync(
		CM.TextResourceDto dto,
		CancellationToken cancellationToken = default)
		=> await SendToRouteAsync<UpdateResult>(HttpMethod.Put, ControllerName, $"{dto.TextKey}", null, dto, cancellationToken)
		?? throw new Exception("SendToRouteAsync (Update) failed.");

	public async Task<DeleteResult> DeleteAsync(
		string textKey,
		CancellationToken cancellationToken = default)
		=> await SendToRouteAsync<DeleteResult>(HttpMethod.Delete, ControllerName, $"{textKey}", null, null, cancellationToken)
		?? throw new Exception("SendToRouteAsync (Delete) failed.");

	public async Task<DeleteResult> DeleteAsync(
		CM.TextResourceDto dto,
		CancellationToken cancellationToken = default)
		=> await DeleteAsync(dto.TextKey, cancellationToken);

	//

	public async Task<bool> TryCreateAsync(
		CM.TextResourceDto dto,
		CancellationToken cancellationToken = default)
	{
		CreateResult<DM.TextResource> result = await CreateAsync(dto, cancellationToken);

		return result.Succeeded;
	}

	public async Task<long> CountAllAsync(
		CancellationToken cancellationToken = default)
	{
		TextResourceFilter filter = TextResourceFilter.All();

		return await CountAsync(filter, cancellationToken);
	}

	public async Task<PagedResponse<DM.TextResource>> ListAllAsync(
		CancellationToken cancellationToken = default)
	{
		TextResourceFilter filter = TextResourceFilter.All();

		return await ListAsync(filter, cancellationToken);
	}

	public async Task<IReadOnlyList<DM.TextResource>> ListItemsAsync(
		TextResourceFilter filter,
		CancellationToken cancellationToken = default)
	{
		PagedResponse<DM.TextResource> response = await ListAsync(filter, cancellationToken);

		return response.Items;
	}

	public async Task<IReadOnlyList<DM.TextResource>> ListAllItemsAsync(
		CancellationToken cancellationToken = default)
	{
		TextResourceFilter filter = TextResourceFilter.All();

		PagedResponse<DM.TextResource> response = await ListAsync(filter, cancellationToken);

		return response.Items;
	}

	public async Task<DM.TextResource> SingleAsync(
		TextResourceFilter filter,
		CancellationToken cancellationToken = default)
	{
		IReadOnlyList<DM.TextResource> items = await ListItemsAsync(filter, cancellationToken);

		return items.Single();		
	}

	public async Task<DM.TextResource?> SingleOrDefaultAsync(
		TextResourceFilter filter,
		CancellationToken cancellationToken = default)
	{
		IReadOnlyList<DM.TextResource> items = await ListItemsAsync(filter, cancellationToken);

		return items.SingleOrDefault();		
	}

	public async Task<bool> TryUpdateAsync(
		CM.TextResourceDto dto,
		CancellationToken cancellationToken = default)
	{
		UpdateResult result = await UpdateAsync(dto, cancellationToken);

		return result.Succeeded;
	}

	public async Task<bool> TryDeleteAsync(
		string textKey,
		CancellationToken cancellationToken = default)
	{
		DeleteResult result = await DeleteAsync(textKey, cancellationToken);

		return result.Succeeded;
	}

	public async Task<bool> TryDeleteAsync(
		CM.TextResourceDto dto,
		CancellationToken cancellationToken = default)
		=> await TryDeleteAsync(dto.TextKey, cancellationToken);

	//
}