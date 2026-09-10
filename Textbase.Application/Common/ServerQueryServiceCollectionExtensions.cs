using Microsoft.Extensions.DependencyInjection;
using Textbase.Application.Features.AuthPrincipals;
using Textbase.Application.Features.ClientApplications;
using Textbase.Application.Features.Formalities;
using Textbase.Application.Features.Presentations;
using Textbase.Application.Features.TextResources;

namespace Textbase.Application.Common;

public static class ServerQueryServiceCollectionExtensions
{
	public static IServiceCollection AddTextbaseServerQueries(
		this IServiceCollection services)
	{
		services.AddScoped(services => (IAuthPrincipalServerQueries)services.GetRequiredService<IAuthPrincipalQueries>());
		services.AddScoped(services => (IClientApplicationServerQueries)services.GetRequiredService<IClientApplicationQueries>());
		services.AddScoped(services => (IFormalityServerQueries)services.GetRequiredService<IFormalityQueries>());
		services.AddScoped(services => (IPresentationServerQueries)services.GetRequiredService<IPresentationQueries>());
		services.AddScoped(services => (ITextResourceServerQueries)services.GetRequiredService<ITextResourceQueries>());

		return services;
	}
}
