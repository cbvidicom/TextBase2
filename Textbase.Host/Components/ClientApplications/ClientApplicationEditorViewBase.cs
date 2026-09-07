using System.Security.Claims;
using Microsoft.AspNetCore.Components;
using Textbase.Application.Features.ClientApplications;
using Textbase.Contracts.Models;
using Textbase.Domain.Models;
using Textbase.Host.Api.Authorization;
using Textbase.Host.Components.Infrastructure;
using Textbase.Host.ViewModels.ClientApplications;
using Uwn.Blazor.Components.Abstractions.Querying;
using Uwn.Blazor.Models.Common;
using Uwn.Common.Conversion;

namespace Textbase.Host.Components.ClientApplications;

public abstract class ClientApplicationEditorViewBase
	: BaseGuidEditorView<ClientApplicationEditorViewModel, ClientApplicationDto, ClientApplication, ClientApplicationFilter>
{
	[CascadingParameter]
	protected MainLayout MainLayout { get; set; } = default!;

	[Inject]
	private IClientApplicationAuthorization Authorization { get; set; } = default!;

	[Inject]
	private IHttpContextAccessor HttpContextAccessor { get; set; } = default!;

	protected bool CanWrite { get; private set; }

	protected override async Task OnParametersSetAsync()
	{
		await base.OnParametersSetAsync();
		MainLayout.SetHeader(IsNewItem ? "Create Application" : "Edit Application");
		await CheckAuthorizationAsync();
	}

	protected async Task HandleSaveAsync(
		SaveMode mode)
	{
		if (!await CheckAuthorizationAsync())
			return;

		await SaveItemAsync(mode);
	}

	private async Task<bool> CheckAuthorizationAsync()
	{
		ClaimsPrincipal user = HttpContextAccessor.HttpContext?.User ?? new ClaimsPrincipal();
		ClientApplicationDto dto = ObjectMapper.MapTo<ClientApplicationDto>(Item);

		if (IsNewItem)
		{
			CanWrite = await Authorization.CanCreateAsync(dto, user);
			if (!CanWrite)
				NavigationManager.NavigateTo(StaticRoutes.ClientApplicationList);

			return CanWrite;
		}

		if (!await Authorization.CanReadAsync(Item.ClientApplicationGuid, user))
		{
			CanWrite = false;
			NavigationManager.NavigateTo(StaticRoutes.ClientApplicationList);
			return false;
		}

		CanWrite = await Authorization.CanUpdateAsync(Item.ClientApplicationGuid, dto, user);
		return true;
	}
}
