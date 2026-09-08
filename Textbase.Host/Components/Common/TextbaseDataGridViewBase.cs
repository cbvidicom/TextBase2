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
	protected bool IsInitialized { get; private set; }

	protected override void OnAfterRender(
		bool firstRender)
		=> IsInitialized = true;
}
