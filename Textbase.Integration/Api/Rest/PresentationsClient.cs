/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using Textbase.Application.Common;
using Textbase.Application.Features.Presentations;
using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using Uwn.Common.Net.Http;
using Uwn.Common.Querying;
using Uwn.EntityFrameworkCore.Infrastructure;

namespace Textbase.Integration.Api.Rest;

public sealed partial class PresentationsClient(
	string baseUrl,
	string? bearerToken = null,
	HttpClient? httpClient = null
	)
	: RestClientBase(baseUrl, bearerToken, httpClient)
	, IPresentationCommands
	, IPresentationQueries
	, IClientForController
{
	public string ControllerName => ApiStrings.PresentationsControllerName;

	//

	protected override bool ThrowOnNonSuccessStatusCode => false;

	//

	public async Task<CreateResult<DM.Presentation>> CreateAsync(
		CM.PresentationDto dto,
		CancellationToken cancellationToken = default)
		=> await SendAsync<CreateResult<DM.Presentation>>(HttpMethod.Post, ControllerName, null, dto, cancellationToken)
		?? throw new Exception("SendAsync (Create) failed.");

	public async Task<DM.Presentation?> ReadAsync(
		string presentationKey,
		CancellationToken cancellationToken = default)
		=> await SendToRouteAsync<DM.Presentation?>(HttpMethod.Get, ControllerName, $"{presentationKey}", null, null, cancellationToken);

	public async Task<long> CountAsync(
		PresentationFilter filter,
		CancellationToken cancellationToken = default)
		=> await SendToRouteAsync<long>(HttpMethod.Get, ControllerName, ApiStrings.CountRoute, filter, null, cancellationToken);

	public async Task<PagedResponse<DM.Presentation>> ListAsync(
		PresentationFilter filter,
		CancellationToken cancellationToken = default)
		=> await SendAsync<PagedResponse<DM.Presentation>?>(HttpMethod.Get, ControllerName, filter, null, cancellationToken)
		?? throw new Exception("List returned a null result.");

	public async Task<UpdateResult> UpdateAsync(
		CM.PresentationDto dto,
		CancellationToken cancellationToken = default)
		=> await SendToRouteAsync<UpdateResult>(HttpMethod.Put, ControllerName, $"{dto.PresentationKey}", null, dto, cancellationToken)
		?? throw new Exception("SendToRouteAsync (Update) failed.");

	public async Task<DeleteResult> DeleteAsync(
		string presentationKey,
		CancellationToken cancellationToken = default)
		=> await SendToRouteAsync<DeleteResult>(HttpMethod.Delete, ControllerName, $"{presentationKey}", null, null, cancellationToken)
		?? throw new Exception("SendToRouteAsync (Delete) failed.");

	public async Task<DeleteResult> DeleteAsync(
		CM.PresentationDto dto,
		CancellationToken cancellationToken = default)
		=> await DeleteAsync(dto.PresentationKey, cancellationToken);

	//

	public async Task<bool> TryCreateAsync(
		CM.PresentationDto dto,
		CancellationToken cancellationToken = default)
	{
		CreateResult<DM.Presentation> result = await CreateAsync(dto, cancellationToken);

		return result.Succeeded;
	}

	public async Task<long> CountAllAsync(
		CancellationToken cancellationToken = default)
	{
		PresentationFilter filter = PresentationFilter.All();

		return await CountAsync(filter, cancellationToken);
	}

	public async Task<PagedResponse<DM.Presentation>> ListAllAsync(
		CancellationToken cancellationToken = default)
	{
		PresentationFilter filter = PresentationFilter.All();

		return await ListAsync(filter, cancellationToken);
	}

	public async Task<IReadOnlyList<DM.Presentation>> ListItemsAsync(
		PresentationFilter filter,
		CancellationToken cancellationToken = default)
	{
		PagedResponse<DM.Presentation> response = await ListAsync(filter, cancellationToken);

		return response.Items;
	}

	public async Task<IReadOnlyList<DM.Presentation>> ListAllItemsAsync(
		CancellationToken cancellationToken = default)
	{
		PresentationFilter filter = PresentationFilter.All();

		PagedResponse<DM.Presentation> response = await ListAsync(filter, cancellationToken);

		return response.Items;
	}

	public async Task<DM.Presentation> SingleAsync(
		PresentationFilter filter,
		CancellationToken cancellationToken = default)
	{
		IReadOnlyList<DM.Presentation> items = await ListItemsAsync(filter, cancellationToken);

		return items.Single();		
	}

	public async Task<DM.Presentation?> SingleOrDefaultAsync(
		PresentationFilter filter,
		CancellationToken cancellationToken = default)
	{
		IReadOnlyList<DM.Presentation> items = await ListItemsAsync(filter, cancellationToken);

		return items.SingleOrDefault();		
	}

	public async Task<bool> TryUpdateAsync(
		CM.PresentationDto dto,
		CancellationToken cancellationToken = default)
	{
		UpdateResult result = await UpdateAsync(dto, cancellationToken);

		return result.Succeeded;
	}

	public async Task<bool> TryDeleteAsync(
		string presentationKey,
		CancellationToken cancellationToken = default)
	{
		DeleteResult result = await DeleteAsync(presentationKey, cancellationToken);

		return result.Succeeded;
	}

	public async Task<bool> TryDeleteAsync(
		CM.PresentationDto dto,
		CancellationToken cancellationToken = default)
		=> await TryDeleteAsync(dto.PresentationKey, cancellationToken);

	//
}