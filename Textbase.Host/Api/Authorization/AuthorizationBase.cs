using Textbase.Domain.Enumerations;
using Textbase.Host.Authorization;

namespace Textbase.Host.Api.Authorization;

public abstract class AuthorizationBase(
	AuthorizationScope scope)
{
	protected readonly AuthorizationScope _scope = scope;

	protected async ValueTask<bool> IsSysAdminAsync(
		CancellationToken cancellationToken)
	{
		CurrentPrincipal? principal = await _scope.GetPrincipalAsync(cancellationToken);

		return principal is not null &&
			principal.RolesValue.HasFlag(Roles.SysAdmin);
	}

	public static IServiceCollection RegisterAuthorizationServices(
		IServiceCollection services)
	{
		services.AddScoped<AuthorizationScope>();
		services.AddScoped<IAuthPrincipalAuthorization, AuthPrincipalAuthorization>();
		services.AddScoped<IAuthPrincipalClientApplicationAuthorization, AuthPrincipalClientApplicationAuthorization>();
		services.AddScoped<IAuthPrincipalLocaleAuthorization, AuthPrincipalLocaleAuthorization>();
		services.AddScoped<IClientApplicationAuthorization, ClientApplicationAuthorization>();
		services.AddScoped<IClientApplicationLocaleAuthorization, ClientApplicationLocaleAuthorization>();
		services.AddScoped<IClientApplicationTextResourceAuthorization, ClientApplicationTextResourceAuthorization>();
		services.AddScoped<IFlatTranslationAuthorization, FlatTranslationAuthorization>();
		services.AddScoped<IFormalityAuthorization, FormalityAuthorization>();
		services.AddScoped<ILocaleAuthorization, LocaleAuthorization>();
		services.AddScoped<IPresentationAuthorization, PresentationAuthorization>();
		services.AddScoped<ITextResourceAuthorization, TextResourceAuthorization>();
		services.AddScoped<ITranslationAuthorization, TranslationAuthorization>();

		return services;
	}
}
