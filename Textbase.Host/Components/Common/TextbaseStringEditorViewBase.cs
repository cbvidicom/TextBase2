using Microsoft.AspNetCore.Components;
using Textbase.Host.ViewModels.Common;
using Uwn.Common.Abstractions;
using Uwn.Common.Querying;

namespace Textbase.Host.Components.Common;

public abstract class TextbaseStringEditorViewBase<TViewModel, TDTO, TModel, TFilter>
	: TextbaseEditorViewBase<TViewModel, TDTO, TModel, TFilter>
	where TViewModel : TextbaseStringEditorViewModel<TDTO, TModel>
	where TDTO : class
	where TModel : class, TDTO
	where TFilter : QueryFilterBase, new()
{
	[Parameter]
	public string? Key { get; set; }

	protected override async Task OnParametersSetAsync()
	{
		await base.OnParametersSetAsync();

		IAsyncInitializable<string?> initializable = ViewModel;
		if (ViewModel is IDelayInitialize)
			return;

		await BeforeInitializeViewModelAsync();
		await initializable.InitializeAsync(Key);
		await AfterInitializeViewModelAsync();
	}
}
