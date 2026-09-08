using Radzen;
using Textbase.Host.Components.Infrastructure;
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

		if (hasAccess)
			return;

		string message = accessDeniedMessage ?? "Access denied.";

		if (alertService.IsVisible &&
			alertService.AlertStyle == AlertStyle.Danger &&
			String.Equals(alertService.Text, message, StringComparison.Ordinal))
			return;

		alertService.Show(message, AlertStyle.Danger);
	}
}
