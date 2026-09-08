using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Textbase.Host.Api.Infrastructure;

public sealed class ApiAuthorizationConvention
	: IControllerModelConvention
{
	public void Apply(
		ControllerModel controller)
	{
		if (controller.ControllerType.Namespace?.StartsWith("Textbase.Host.Api.Controllers", StringComparison.Ordinal) != true)
			return;

		controller.Filters.Add(new AuthorizeFilter());
	}
}
