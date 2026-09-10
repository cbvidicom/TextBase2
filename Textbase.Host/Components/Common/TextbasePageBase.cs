using Microsoft.AspNetCore.Components;
using Textbase.Host.Components.Infrastructure;

namespace Textbase.Host.Components.Common;

public abstract class TextbasePageBase
	: ComponentBase
{
	[CascadingParameter]
	protected MainLayout MainLayout { get; set; } = default!;

	public abstract string MainHeader { get; }

	protected override void OnParametersSet()
	{
		base.OnParametersSet();

		MainLayout.SetHeader(MainHeader);
	}

	protected void InitializeView(
		bool hasAccess,
		string? accessDeniedMessage = null)
		=> TextbaseViewHelper.Initialize(MainLayout, MainHeader, hasAccess, accessDeniedMessage);
}

public abstract class TextbasePageBase<TViewModel>
	: TextbasePageBase
	where TViewModel : class
{
	[Inject]
	protected TViewModel ViewModel { get; set; } = default!;
}
