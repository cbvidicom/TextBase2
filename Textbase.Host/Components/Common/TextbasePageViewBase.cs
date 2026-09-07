using Microsoft.AspNetCore.Components;
using Textbase.Host.Components.Infrastructure;

namespace Textbase.Host.Components.Common;

public abstract class TextbasePageViewBase
	: ComponentBase
{
	[CascadingParameter]
	protected MainLayout MainLayout { get; set; } = default!;

	protected abstract string Header { get; }

	protected override void OnParametersSet()
	{
		base.OnParametersSet();
		MainLayout.SetHeader(Header);
	}
}
