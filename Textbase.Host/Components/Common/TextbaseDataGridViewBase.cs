using Radzen;
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
	protected virtual string UnauthorizedPath => StaticRoutes.Home;

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
