using Textbase.Host.Components.Infrastructure;

namespace Textbase.Host.Components.Common;

internal static class TextbaseViewHelper
{
	public static void Initialize(
		MainLayout mainLayout,
		string? mainHeader,
		bool hasAccess,
		string? accessDeniedMessage)
	{
		mainLayout.SetHeader(mainHeader ?? String.Empty);
		mainLayout.SetAccessDenied(hasAccess ? null : accessDeniedMessage ?? "Access denied.");
	}
}
