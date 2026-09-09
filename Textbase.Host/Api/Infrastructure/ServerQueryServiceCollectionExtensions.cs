using Textbase.Application.Features.ClientApplications;
using Textbase.Application.Features.Formalities;
using Textbase.Application.Features.Presentations;

namespace Textbase.Host.Api.Infrastructure;

public static class ServerQueryServiceCollectionExtensions
{
	public static IServiceCollection AddTextbaseServerQueries(
		this IServiceCollection services)
	{
		services.AddScoped(services => (IClientApplicationServerQueries)services.GetRequiredService<IClientApplicationQueries>());
		services.AddScoped(services => (IFormalityServerQueries)services.GetRequiredService<IFormalityQueries>());
		services.AddScoped(services => (IPresentationServerQueries)services.GetRequiredService<IPresentationQueries>());

		return services;
	}
}
