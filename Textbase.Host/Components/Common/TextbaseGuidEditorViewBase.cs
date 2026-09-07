using Radzen;
using Uwn.Blazor.Components.Abstractions.Querying;
using Uwn.Blazor.Models.Common;
using Uwn.Blazor.Models.ViewModels.Abstractions.Querying;

namespace Textbase.Host.Components.Common;

public abstract class TextbaseGuidEditorViewBase<TViewModel, TDTO, TModel, TFilter>
	: BaseGuidEditorView<TViewModel, TDTO, TModel, TFilter>
	where TViewModel : GuidEditorViewModel<TDTO, TModel>
	where TDTO : class
	where TModel : class, TDTO
	where TFilter : class
{
	protected virtual string UnauthorizedPath => StaticRoutes.Home;

	protected override async Task OnParametersSetAsync()
	{
		try
		{
			await base.OnParametersSetAsync();
		}
		catch (UnauthorizedAccessException exception)
		{
			CoreAlertService.Show(exception.Message, AlertStyle.Danger);
			CoreNavigationManager.NavigateTo(UnauthorizedPath);
		}
	}

	protected async Task SaveAuthorizedItemAsync(
		SaveMode mode)
	{
		try
		{
			await SaveItemAsync(mode);
		}
		catch (UnauthorizedAccessException exception)
		{
			CoreAlertService.Show(exception.Message, AlertStyle.Danger);
		}
	}
}
