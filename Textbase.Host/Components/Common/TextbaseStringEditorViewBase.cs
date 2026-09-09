using Microsoft.AspNetCore.Components;
using Uwn.Blazor.Models.ViewModels.Abstractions.Querying;
using Uwn.Common.Abstractions;
using Uwn.Common.Querying;

namespace Textbase.Host.Components.Common;

public abstract class TextbaseStringEditorViewBase<TViewModel, TDTO, TModel, TFilter>
	: TextbaseEditorViewBase<TViewModel, TDTO, TModel, TFilter>
	where TViewModel : StringEditorViewModel<TDTO, TModel>
	where TDTO : class
	where TModel : class, TDTO
	where TFilter : QueryFilterBase, new()
{
	[Parameter]
	public string? Key { get; set; }

	protected override async Task OnParametersSetAsync()
	{
		await base.OnParametersSetAsync();

		if (SuppressInitialization)
			return;

		IAsyncInitializable<string?> initializable = ViewModel;
		if (ViewModel is IDelayInitialize)
			return;

		await BeforeInitializeViewModelAsync();
		await initializable.InitializeAsync(Key);
		await AfterInitializeViewModelAsync();
	}
}
