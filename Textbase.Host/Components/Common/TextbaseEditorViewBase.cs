using Uwn.Blazor.Components.Abstractions.Querying;
using Uwn.Blazor.Enumerations.Common;
using Uwn.Blazor.Models.ViewModels.Abstractions.Querying;
using Uwn.Common.Querying;

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
		=> SaveItemAsync(SaveMode.Save);

	protected Task OnSaveAndGoBack()
		=> SaveItemAsync(SaveMode.SaveAndGoBack);

	protected async Task OnSaveAndNew()
	{
		if (await SaveItemAsync() && CanNew)
			CoreNavigationManager.NavigateTo(CreatePath!, true);
	}
}
