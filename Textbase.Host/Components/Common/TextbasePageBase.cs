using Microsoft.AspNetCore.Components;
using Textbase.Host.Components.Infrastructure;

namespace Textbase.Host.Components.Common;

public abstract class TextbasePageBase
	: ComponentBase
{
	[CascadingParameter]
	private MainLayout MainLayout { get; set; } = default!;

	public abstract string MainHeader { get; }

	protected override void OnParametersSet()
	{
		base.OnParametersSet();
		MainLayout.SetHeader(MainHeader);
	}
}
