using Microsoft.AspNetCore.Components;
using Radzen;
using Textbase.Host.Components.Infrastructure;
using Uwn.Blazor.Components.Abstractions.Querying;
using Uwn.Blazor.Models.ViewModels.Abstractions.Querying;
using Uwn.Common.Querying;

namespace Textbase.Host.Components.Common;

public abstract class TextbaseDataGridViewBase<TViewModel, TModel, TFilter>
	: BaseDataGridView<TViewModel, TModel, TFilter>
	where TViewModel : DataGridViewModel<TModel, TFilter>
	where TModel : class
	where TFilter : QueryFilterBase, new()
{
	[CascadingParameter]
	private MainLayout MainLayout { get; set; } = default!;

	protected abstract string Header { get; }
	protected virtual string UnauthorizedPath => StaticRoutes.Home;

	protected override void OnParametersSet()
	{
		base.OnParametersSet();
		MainLayout.SetHeader(Header);
	}

	protected async Task LoadDataAsync(
		LoadDataArgs args)
	{
		try
		{
			await ViewModel.LoadDataAsync(args);
		}
		catch (UnauthorizedAccessException exception)
		{
			CoreAlertService.Show(exception.Message, AlertStyle.Danger);
			CoreNavigationManager.NavigateTo(UnauthorizedPath);
		}
	}
}
