/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using Textbase.Application.Common;
using Textbase.Application.Features.AuthPrincipalClientApplications;
using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using Uwn.Common.Net.Http;
using Uwn.Common.Querying;
using Uwn.EntityFrameworkCore.Infrastructure;

namespace Textbase.Integration.Api.Rest;

public sealed partial class AuthPrincipalClientApplicationsClient(
	string baseUrl,
	string? bearerToken = null,
	HttpClient? httpClient = null
	)
	: RestClientBase(baseUrl, bearerToken, httpClient)
	, IAuthPrincipalClientApplicationCommands
	, IAuthPrincipalClientApplicationQueries
	, IClientForController
{
	public string ControllerName => ApiStrings.AuthPrincipalClientApplicationsControllerName;

	//

	protected override bool ThrowOnNonSuccessStatusCode => false;

	//

	public async Task<CreateResult<DM.AuthPrincipalClientApplication>> CreateAsync(
		CM.AuthPrincipalClientApplicationDto dto,
		CancellationToken cancellationToken = default)
		=> await SendAsync<CreateResult<DM.AuthPrincipalClientApplication>>(HttpMethod.Post, ControllerName, null, dto, cancellationToken)
		?? throw new Exception("SendAsync (Create) failed.");

	public async Task<DM.AuthPrincipalClientApplication?> ReadAsync(
		Guid entraObjectId, Guid clientApplicationGuid,
		CancellationToken cancellationToken = default)
		=> await SendToRouteAsync<DM.AuthPrincipalClientApplication?>(HttpMethod.Get, ControllerName, $"{entraObjectId}/{clientApplicationGuid}", null, null, cancellationToken);

	public async Task<long> CountAsync(
		AuthPrincipalClientApplicationFilter filter,
		CancellationToken cancellationToken = default)
		=> await SendToRouteAsync<long>(HttpMethod.Get, ControllerName, ApiStrings.CountRoute, filter, null, cancellationToken);

	public async Task<PagedResponse<DM.AuthPrincipalClientApplication>> ListAsync(
		AuthPrincipalClientApplicationFilter filter,
		CancellationToken cancellationToken = default)
		=> await SendAsync<PagedResponse<DM.AuthPrincipalClientApplication>?>(HttpMethod.Get, ControllerName, filter, null, cancellationToken)
		?? throw new Exception("List returned a null result.");

	public async Task<UpdateResult> UpdateAsync(
		CM.AuthPrincipalClientApplicationDto dto,
		CancellationToken cancellationToken = default)
		=> await SendToRouteAsync<UpdateResult>(HttpMethod.Put, ControllerName, $"{dto.EntraObjectId}/{dto.ClientApplicationGuid}", null, dto, cancellationToken)
		?? throw new Exception("SendToRouteAsync (Update) failed.");

	public async Task<DeleteResult> DeleteAsync(
		Guid entraObjectId, Guid clientApplicationGuid,
		CancellationToken cancellationToken = default)
		=> await SendToRouteAsync<DeleteResult>(HttpMethod.Delete, ControllerName, $"{entraObjectId}/{clientApplicationGuid}", null, null, cancellationToken)
		?? throw new Exception("SendToRouteAsync (Delete) failed.");

	public async Task<DeleteResult> DeleteAsync(
		CM.AuthPrincipalClientApplicationDto dto,
		CancellationToken cancellationToken = default)
		=> await DeleteAsync(dto.EntraObjectId, dto.ClientApplicationGuid, cancellationToken);

	//

	public async Task<bool> TryCreateAsync(
		CM.AuthPrincipalClientApplicationDto dto,
		CancellationToken cancellationToken = default)
	{
		CreateResult<DM.AuthPrincipalClientApplication> result = await CreateAsync(dto, cancellationToken);

		return result.Succeeded;
	}

	public async Task<long> CountAllAsync(
		CancellationToken cancellationToken = default)
	{
		AuthPrincipalClientApplicationFilter filter = AuthPrincipalClientApplicationFilter.All();

		return await CountAsync(filter, cancellationToken);
	}

	public async Task<PagedResponse<DM.AuthPrincipalClientApplication>> ListAllAsync(
		CancellationToken cancellationToken = default)
	{
		AuthPrincipalClientApplicationFilter filter = AuthPrincipalClientApplicationFilter.All();

		return await ListAsync(filter, cancellationToken);
	}

	public async Task<IReadOnlyList<DM.AuthPrincipalClientApplication>> ListItemsAsync(
		AuthPrincipalClientApplicationFilter filter,
		CancellationToken cancellationToken = default)
	{
		PagedResponse<DM.AuthPrincipalClientApplication> response = await ListAsync(filter, cancellationToken);

		return response.Items;
	}

	public async Task<IReadOnlyList<DM.AuthPrincipalClientApplication>> ListAllItemsAsync(
		CancellationToken cancellationToken = default)
	{
		AuthPrincipalClientApplicationFilter filter = AuthPrincipalClientApplicationFilter.All();

		PagedResponse<DM.AuthPrincipalClientApplication> response = await ListAsync(filter, cancellationToken);

		return response.Items;
	}

	public async Task<DM.AuthPrincipalClientApplication> SingleAsync(
		AuthPrincipalClientApplicationFilter filter,
		CancellationToken cancellationToken = default)
	{
		IReadOnlyList<DM.AuthPrincipalClientApplication> items = await ListItemsAsync(filter, cancellationToken);

		return items.Single();		
	}

	public async Task<DM.AuthPrincipalClientApplication?> SingleOrDefaultAsync(
		AuthPrincipalClientApplicationFilter filter,
		CancellationToken cancellationToken = default)
	{
		IReadOnlyList<DM.AuthPrincipalClientApplication> items = await ListItemsAsync(filter, cancellationToken);

		return items.SingleOrDefault();		
	}

	public async Task<bool> TryUpdateAsync(
		CM.AuthPrincipalClientApplicationDto dto,
		CancellationToken cancellationToken = default)
	{
		UpdateResult result = await UpdateAsync(dto, cancellationToken);

		return result.Succeeded;
	}

	public async Task<bool> TryDeleteAsync(
		Guid entraObjectId, Guid clientApplicationGuid,
		CancellationToken cancellationToken = default)
	{
		DeleteResult result = await DeleteAsync(entraObjectId, clientApplicationGuid, cancellationToken);

		return result.Succeeded;
	}

	public async Task<bool> TryDeleteAsync(
		CM.AuthPrincipalClientApplicationDto dto,
		CancellationToken cancellationToken = default)
		=> await TryDeleteAsync(dto.EntraObjectId, dto.ClientApplicationGuid, cancellationToken);

	//
}