using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace Textbase.Host.Api.Infrastructure;

public sealed class ApiAuthorizationConvention
	: IControllerModelConvention
{
	public const string PolicyName = "TextbaseApi";

	public void Apply(
		ControllerModel controller)
	{
		if (controller.ControllerType.Namespace?.StartsWith("Textbase.Host.Api.Controllers", StringComparison.Ordinal) != true)
			return;

		controller.Filters.Add(new AuthorizeFilter(PolicyName));
	}
}
