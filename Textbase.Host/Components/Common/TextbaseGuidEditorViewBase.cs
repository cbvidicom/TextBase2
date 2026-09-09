using Microsoft.AspNetCore.Components;
using Uwn.Blazor.Models.ViewModels.Abstractions.Querying;
using Uwn.Common.Abstractions;
using Uwn.Common.Querying;

namespace Textbase.Host.Components.Common;

public abstract class TextbaseGuidEditorViewBase<TViewModel, TDTO, TModel, TFilter>
	: TextbaseEditorViewBase<TViewModel, TDTO, TModel, TFilter>
	where TViewModel : GuidEditorViewModel<TDTO, TModel>
	where TDTO : class
	where TModel : class, TDTO
	where TFilter : QueryFilterBase, new()
{
	[Parameter]
	public Guid? Guid { get; set; }

	protected override async Task OnParametersSetAsync()
	{
		await base.OnParametersSetAsync();

		IAsyncInitializable<Guid?> initializable = ViewModel;
		if (ViewModel is IDelayInitialize)
			return;

		await BeforeInitializeViewModelAsync();
		await initializable.InitializeAsync(Guid);
		await AfterInitializeViewModelAsync();
	}
}
