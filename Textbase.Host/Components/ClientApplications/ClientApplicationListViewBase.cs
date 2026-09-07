using System.Security.Claims;
using Microsoft.AspNetCore.Components;
using Textbase.Application.Features.ClientApplications;
using Textbase.Domain.Models;
using Textbase.Host.Api.Authorization;
using Textbase.Host.Components.Infrastructure;
using Textbase.Host.ViewModels.ClientApplications;
using Uwn.Blazor.Components.Abstractions.Querying;

namespace Textbase.Host.Components.ClientApplications;

public abstract class ClientApplicationListViewBase
	: BaseDataGridView<ClientApplicationListViewModel, ClientApplication, ClientApplicationFilter>
{
	[CascadingParameter]
	protected MainLayout MainLayout { get; set; } = default!;

	[Inject]
	private IClientApplicationAuthorization Authorization { get; set; } = default!;

	[Inject]
	private IHttpContextAccessor HttpContextAccessor { get; set; } = default!;

	protected IReadOnlyCollection<Guid>? AuthorizedClientApplicationGuids { get; private set; }

	protected override async Task OnInitializedAsync()
	{
		await base.OnInitializedAsync();
		MainLayout.SetHeader("Applications");

		ClientApplicationFilter filter = ClientApplicationFilter.All();
		ClaimsPrincipal user = HttpContextAccessor.HttpContext?.User ?? new ClaimsPrincipal();
		if (!await Authorization.CanListAsync(filter, user))
		{
			NavigationManager.NavigateTo(StaticRoutes.Home);
			return;
		}

		AuthorizedClientApplicationGuids = filter.ClientApplicationGuid?.AnyOf;
	}
}
