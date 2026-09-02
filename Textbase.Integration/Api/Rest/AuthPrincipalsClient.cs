/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using Textbase.Application.Common;
using Textbase.Application.Features.AuthPrincipals;
using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using Uwn.Common.Net.Http;
using Uwn.Common.Querying;
using Uwn.EntityFrameworkCore.Infrastructure;

namespace Textbase.Integration.Api.Rest;

public sealed partial class AuthPrincipalsClient(
	string baseUrl,
	string? bearerToken = null,
	HttpClient? httpClient = null
	)
	: RestClientBase(baseUrl, bearerToken, httpClient)
	, IAuthPrincipalCommands
	, IAuthPrincipalQueries
	, IClientForController
{
	public string ControllerName => ApiStrings.AuthPrincipalsControllerName;

	//

	protected override bool ThrowOnNonSuccessStatusCode => false;

	//

	public async Task<CreateResult<DM.AuthPrincipal>> CreateAsync(
		CM.AuthPrincipalDto dto,
		CancellationToken cancellationToken = default)
		=> await SendAsync<CreateResult<DM.AuthPrincipal>>(HttpMethod.Post, ControllerName, null, dto, cancellationToken)
		?? throw new Exception("SendAsync (Create) failed.");

	public async Task<DM.AuthPrincipal?> ReadAsync(
		Guid entraObjectId,
		CancellationToken cancellationToken = default)
		=> await SendToRouteAsync<DM.AuthPrincipal?>(HttpMethod.Get, ControllerName, $"{entraObjectId}", null, null, cancellationToken);

	public async Task<long> CountAsync(
		AuthPrincipalFilter filter,
		CancellationToken cancellationToken = default)
		=> await SendToRouteAsync<long>(HttpMethod.Get, ControllerName, ApiStrings.CountRoute, filter, null, cancellationToken);

	public async Task<PagedResponse<DM.AuthPrincipal>> ListAsync(
		AuthPrincipalFilter filter,
		CancellationToken cancellationToken = default)
		=> await SendAsync<PagedResponse<DM.AuthPrincipal>?>(HttpMethod.Get, ControllerName, filter, null, cancellationToken)
		?? throw new Exception("List returned a null result.");

	public async Task<UpdateResult> UpdateAsync(
		CM.AuthPrincipalDto dto,
		CancellationToken cancellationToken = default)
		=> await SendToRouteAsync<UpdateResult>(HttpMethod.Put, ControllerName, $"{dto.EntraObjectId}", null, dto, cancellationToken)
		?? throw new Exception("SendToRouteAsync (Update) failed.");

	public async Task<DeleteResult> DeleteAsync(
		Guid entraObjectId,
		CancellationToken cancellationToken = default)
		=> await SendToRouteAsync<DeleteResult>(HttpMethod.Delete, ControllerName, $"{entraObjectId}", null, null, cancellationToken)
		?? throw new Exception("SendToRouteAsync (Delete) failed.");

	public async Task<DeleteResult> DeleteAsync(
		CM.AuthPrincipalDto dto,
		CancellationToken cancellationToken = default)
		=> await DeleteAsync(dto.EntraObjectId, cancellationToken);

	//

	public async Task<bool> TryCreateAsync(
		CM.AuthPrincipalDto dto,
		CancellationToken cancellationToken = default)
	{
		CreateResult<DM.AuthPrincipal> result = await CreateAsync(dto, cancellationToken);

		return result.Succeeded;
	}

	public async Task<long> CountAllAsync(
		CancellationToken cancellationToken = default)
	{
		AuthPrincipalFilter filter = AuthPrincipalFilter.All();

		return await CountAsync(filter, cancellationToken);
	}

	public async Task<PagedResponse<DM.AuthPrincipal>> ListAllAsync(
		CancellationToken cancellationToken = default)
	{
		AuthPrincipalFilter filter = AuthPrincipalFilter.All();

		return await ListAsync(filter, cancellationToken);
	}

	public async Task<IReadOnlyList<DM.AuthPrincipal>> ListItemsAsync(
		AuthPrincipalFilter filter,
		CancellationToken cancellationToken = default)
	{
		PagedResponse<DM.AuthPrincipal> response = await ListAsync(filter, cancellationToken);

		return response.Items;
	}

	public async Task<IReadOnlyList<DM.AuthPrincipal>> ListAllItemsAsync(
		CancellationToken cancellationToken = default)
	{
		AuthPrincipalFilter filter = AuthPrincipalFilter.All();

		PagedResponse<DM.AuthPrincipal> response = await ListAsync(filter, cancellationToken);

		return response.Items;
	}

	public async Task<DM.AuthPrincipal> SingleAsync(
		AuthPrincipalFilter filter,
		CancellationToken cancellationToken = default)
	{
		IReadOnlyList<DM.AuthPrincipal> items = await ListItemsAsync(filter, cancellationToken);

		return items.Single();		
	}

	public async Task<DM.AuthPrincipal?> SingleOrDefaultAsync(
		AuthPrincipalFilter filter,
		CancellationToken cancellationToken = default)
	{
		IReadOnlyList<DM.AuthPrincipal> items = await ListItemsAsync(filter, cancellationToken);

		return items.SingleOrDefault();		
	}

	public async Task<bool> TryUpdateAsync(
		CM.AuthPrincipalDto dto,
		CancellationToken cancellationToken = default)
	{
		UpdateResult result = await UpdateAsync(dto, cancellationToken);

		return result.Succeeded;
	}

	public async Task<bool> TryDeleteAsync(
		Guid entraObjectId,
		CancellationToken cancellationToken = default)
	{
		DeleteResult result = await DeleteAsync(entraObjectId, cancellationToken);

		return result.Succeeded;
	}

	public async Task<bool> TryDeleteAsync(
		CM.AuthPrincipalDto dto,
		CancellationToken cancellationToken = default)
		=> await TryDeleteAsync(dto.EntraObjectId, cancellationToken);

	//
}