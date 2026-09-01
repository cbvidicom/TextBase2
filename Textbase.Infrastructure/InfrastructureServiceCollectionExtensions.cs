/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Textbase.Infrastructure.Persistence;
using Textbase.Infrastructure.Persistence.ClientApplications;
using Textbase.Infrastructure.Persistence.ClientApplicationLocales;
using Textbase.Infrastructure.Persistence.ClientApplicationTextResources;
using Textbase.Infrastructure.Persistence.Formalities;
using Textbase.Infrastructure.Persistence.Locales;
using Textbase.Infrastructure.Persistence.Presentations;
using Textbase.Infrastructure.Persistence.TextResources;
using Textbase.Infrastructure.Persistence.Translations;
using Textbase.Infrastructure.Persistence.FlatTranslations;

namespace Textbase.Infrastructure;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddTextbaseInfrastructure(
		this IServiceCollection services,
		string connectionString)
	{
		services.AddTextbaseDbContextFactory(connectionString);
		
		services.AddTextbaseEntityFactories();

		return services;
	}

	public static IServiceCollection AddTextbaseDbContextFactory(
		this IServiceCollection services,
		string connectionString)
	{
		services.AddDbContextFactory<TextbaseDbContext>(options =>
			options.UseSqlServer(connectionString));

		return services;
	}

	public static IServiceCollection AddTextbaseEntityFactories(
		this IServiceCollection services)
	{
		
		services.AddScoped<IClientApplicationEntityFactory, ClientApplicationEntityFactory>();
		services.AddScoped<IClientApplicationLocaleEntityFactory, ClientApplicationLocaleEntityFactory>();
		services.AddScoped<IClientApplicationTextResourceEntityFactory, ClientApplicationTextResourceEntityFactory>();
		services.AddScoped<IFormalityEntityFactory, FormalityEntityFactory>();
		services.AddScoped<ILocaleEntityFactory, LocaleEntityFactory>();
		services.AddScoped<IPresentationEntityFactory, PresentationEntityFactory>();
		services.AddScoped<ITextResourceEntityFactory, TextResourceEntityFactory>();
		services.AddScoped<ITranslationEntityFactory, TranslationEntityFactory>();
		services.AddScoped<IFlatTranslationEntityFactory, FlatTranslationEntityFactory>();

		return services;
	}
}