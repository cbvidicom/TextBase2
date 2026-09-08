using Radzen;
using Uwn.Blazor.Components.Abstractions.Querying;
using Uwn.Blazor.Enumerations.Common;
using Uwn.Blazor.Models.ViewModels.Abstractions.Querying;
using Uwn.Common.Querying;
using Uwn.Common.Resources;

namespace Textbase.Host.Components.Common;

public abstract class TextbaseEditorViewBase<TViewModel, TDTO, TModel, TFilter>
	: BaseEditorView<TViewModel, TDTO, TModel, TFilter>
	where TViewModel : EditorViewModel<TDTO, TModel>
	where TDTO : class
	where TModel : class, TDTO
	where TFilter : QueryFilterBase, new()
{
	protected virtual string? CreatePath => null;

	protected bool CanGoBack => !String.IsNullOrWhiteSpace(GoBackPath);

	protected bool CanNew => IsNewItem && !String.IsNullOrWhiteSpace(CreatePath);

	//

	protected Task OnSave()
		=> HandleSave(SaveMode.Save);

	protected Task OnSaveAndGoBack()
		=> HandleSave(SaveMode.SaveAndGoBack);

	protected Task OnSaveAndNew()
		=> HandleSave(SaveMode.SaveAndNew);

	//

	private async Task HandleSave(
		SaveMode saveMode)
	{
		try
		{
			IsBusy = true;

			await ViewModel.SaveAsync();

			if (ViewModel.LastSaveSucceeded)
			{
				CoreAlertService.ShowSuccess(Localization.ItemHasBeenSaved);

				if (saveMode == SaveMode.SaveAndGoBack &&
					CanGoBack)
					CoreNavigationManager.NavigateTo(GoBackPath!);
				else if (saveMode == SaveMode.SaveAndNew &&
					CanNew)
					CoreNavigationManager.NavigateTo(CreatePath!);
			}
			else
			{
				if (ViewModel.SaveAuthorization.Result.Message is null)
					CoreAlertService.Show(Localization.OperationFailedMessage, AlertStyle.Danger);
				else
					CoreAlertService.Show(Localization.OperationFailedMessage, ViewModel.SaveAuthorization.Result.Message, AlertStyle.Danger);
			}
		}
		catch (Exception ex)
		{
			CoreAlertService.Show(ex);
		}
		finally
		{
			IsBusy = false;
		}
	}
}
