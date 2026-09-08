using Microsoft.AspNetCore.Components;
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

	protected bool IsInitialized { get; private set; }

	protected override async Task OnParametersSetAsync()
	{
		await base.OnParametersSetAsync();
		MainLayout.SetHeader(MainHeader ?? String.Empty);
	}

	protected override void OnAfterRender(
		bool firstRender)
		=> IsInitialized = true;
}
