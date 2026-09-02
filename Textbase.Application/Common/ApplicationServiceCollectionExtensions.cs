/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using Microsoft.Extensions.DependencyInjection;
using Textbase.Application.Features;
using Textbase.Application.Features.AuthPrincipals;
using Textbase.Application.Features.AuthPrincipalClientApplications;
using Textbase.Application.Features.AuthPrincipalLocales;
using Textbase.Application.Features.ClientApplications;
using Textbase.Application.Features.ClientApplicationLocales;
using Textbase.Application.Features.ClientApplicationTextResources;
using Textbase.Application.Features.Formalities;
using Textbase.Application.Features.Locales;
using Textbase.Application.Features.Presentations;
using Textbase.Application.Features.TextResources;
using Textbase.Application.Features.Translations;
using Textbase.Application.Features.FlatTranslations;

namespace Textbase.Application.Common;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddTextbaseApplication(
		this IServiceCollection services,
		bool includeServerCommands = false)
	{
		
		services.AddScoped<IAuthPrincipalQueries, AuthPrincipalQueries>();
		services.AddScoped<IAuthPrincipalClientApplicationQueries, AuthPrincipalClientApplicationQueries>();
		services.AddScoped<IAuthPrincipalLocaleQueries, AuthPrincipalLocaleQueries>();
		services.AddScoped<IClientApplicationQueries, ClientApplicationQueries>();
		services.AddScoped<IClientApplicationLocaleQueries, ClientApplicationLocaleQueries>();
		services.AddScoped<IClientApplicationTextResourceQueries, ClientApplicationTextResourceQueries>();
		services.AddScoped<IFormalityQueries, FormalityQueries>();
		services.AddScoped<ILocaleQueries, LocaleQueries>();
		services.AddScoped<IPresentationQueries, PresentationQueries>();
		services.AddScoped<ITextResourceQueries, TextResourceQueries>();
		services.AddScoped<ITranslationQueries, TranslationQueries>();
		services.AddScoped<IFlatTranslationQueries, FlatTranslationQueries>();
		
		services.AddScoped<IAuthPrincipalCommands, AuthPrincipalCommands>();
		services.AddScoped<IAuthPrincipalClientApplicationCommands, AuthPrincipalClientApplicationCommands>();
		services.AddScoped<IAuthPrincipalLocaleCommands, AuthPrincipalLocaleCommands>();
		services.AddScoped<IClientApplicationCommands, ClientApplicationCommands>();
		services.AddScoped<IClientApplicationLocaleCommands, ClientApplicationLocaleCommands>();
		services.AddScoped<IClientApplicationTextResourceCommands, ClientApplicationTextResourceCommands>();
		services.AddScoped<IFormalityCommands, FormalityCommands>();
		services.AddScoped<ILocaleCommands, LocaleCommands>();
		services.AddScoped<IPresentationCommands, PresentationCommands>();
		services.AddScoped<ITextResourceCommands, TextResourceCommands>();
		services.AddScoped<ITranslationCommands, TranslationCommands>();
		services.AddScoped<IFlatTranslationCommands, FlatTranslationCommands>();

		if (includeServerCommands)
			services.AddTextbaseServerApplication();

		services.AddScoped<Queries>();
		services.AddScoped<Commands>();

		return services;
	}

	public static IServiceCollection AddTextbaseServerApplication(
		this IServiceCollection services)
	{
		
		services.AddScoped<IAuthPrincipalServerCommands, AuthPrincipalCommands>();
		services.AddScoped<IAuthPrincipalClientApplicationServerCommands, AuthPrincipalClientApplicationCommands>();
		services.AddScoped<IAuthPrincipalLocaleServerCommands, AuthPrincipalLocaleCommands>();
		services.AddScoped<IClientApplicationServerCommands, ClientApplicationCommands>();
		services.AddScoped<IClientApplicationLocaleServerCommands, ClientApplicationLocaleCommands>();
		services.AddScoped<IClientApplicationTextResourceServerCommands, ClientApplicationTextResourceCommands>();
		services.AddScoped<IFormalityServerCommands, FormalityCommands>();
		services.AddScoped<ILocaleServerCommands, LocaleCommands>();
		services.AddScoped<IPresentationServerCommands, PresentationCommands>();
		services.AddScoped<ITextResourceServerCommands, TextResourceCommands>();
		services.AddScoped<ITranslationServerCommands, TranslationCommands>();
		services.AddScoped<IFlatTranslationServerCommands, FlatTranslationCommands>();

		return services;
	}
}