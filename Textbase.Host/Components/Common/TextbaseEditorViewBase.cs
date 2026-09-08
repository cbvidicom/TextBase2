using Microsoft.AspNetCore.Components;
using Textbase.Host.Components.Infrastructure;
using Uwn.Blazor.Components.Abstractions.Querying;
using Uwn.Blazor.Enumerations.Common;
using Uwn.Blazor.Models.ViewModels.Abstractions.Querying;
using Uwn.Common.Querying;

namespace Textbase.Host.Components.Common;

public abstract class TextbaseEditorViewBase<TViewModel, TDTO, TModel, TFilter>
	: BaseGuidEditorView<TViewModel, TDTO, TModel, TFilter>
	where TViewModel : GuidEditorViewModel<TDTO, TModel>
	where TDTO : class
	where TModel : class, TDTO
	where TFilter : QueryFilterBase, new()
{
	[CascadingParameter]
	private MainLayout MainLayout { get; set; } = default!;

	protected virtual string? CreatePath => null;

	protected bool CanGoBack => !String.IsNullOrWhiteSpace(GoBackPath);

	protected bool CanNew => IsNewItem && !String.IsNullOrWhiteSpace(CreatePath);

	protected override async Task AfterInitializeViewModelAsync()
	{
		await base.AfterInitializeViewModelAsync();

		TextbaseViewHelper.Initialize(MainLayout, CoreAlertService, MainHeader, HasAccess, AccessDeniedMessage);
	}

	//

	protected Task OnSave()
		=> SaveItemAsync(SaveMode.Save);

	protected Task OnSaveAndGoBack()
		=> SaveItemAsync(SaveMode.SaveAndGoBack);

	protected Task OnSaveAndNew()
		=> SaveItemAsync(SaveMode.SaveAndNew);
}
