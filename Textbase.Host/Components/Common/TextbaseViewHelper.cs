using Radzen;
using Uwn.Blazor.Services.Radzen;

namespace Textbase.Host.Components.Common;

internal static class TextbaseViewHelper
{
	public static void Initialize(
		MainLayout mainLayout,
		RzAlertService alertService,
		string? mainHeader,
		bool hasAccess,
		string? accessDeniedMessage)
	{
		mainLayout.SetHeader(mainHeader ?? String.Empty);

		if (!hasAccess)
			alertService.Show(accessDeniedMessage ?? "Access denied.", AlertStyle.Danger);
	}
}
