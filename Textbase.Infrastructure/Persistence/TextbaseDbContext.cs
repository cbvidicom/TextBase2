/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using Textbase.Infrastructure.Abstractions.Persistence;
using Textbase.Infrastructure.Persistence.AuthPrincipals;
using Textbase.Infrastructure.Persistence.AuthPrincipalClientApplications;
using Textbase.Infrastructure.Persistence.AuthPrincipalLocales;
using Textbase.Infrastructure.Persistence.ClientApplications;
using Textbase.Infrastructure.Persistence.ClientApplicationLocales;
using Textbase.Infrastructure.Persistence.ClientApplicationTextResources;
using Textbase.Infrastructure.Persistence.Formalities;
using Textbase.Infrastructure.Persistence.Locales;
using Textbase.Infrastructure.Persistence.Presentations;
using Textbase.Infrastructure.Persistence.TextResources;
using Textbase.Infrastructure.Persistence.Translations;
using Textbase.Infrastructure.Persistence.FlatTranslations;
using Microsoft.EntityFrameworkCore;

namespace Textbase.Infrastructure.Persistence;

public sealed partial class TextbaseDbContext(
	DbContextOptions<TextbaseDbContext> options)
	: DbContext(options)
	, ITextbaseDbContext
{
	public DbSet<AuthPrincipalEntity> AuthPrincipals => Set<AuthPrincipalEntity>();
	public DbSet<AuthPrincipalClientApplicationEntity> AuthPrincipalClientApplications => Set<AuthPrincipalClientApplicationEntity>();
	public DbSet<AuthPrincipalLocaleEntity> AuthPrincipalLocales => Set<AuthPrincipalLocaleEntity>();
	public DbSet<ClientApplicationEntity> ClientApplications => Set<ClientApplicationEntity>();
	public DbSet<ClientApplicationLocaleEntity> ClientApplicationLocales => Set<ClientApplicationLocaleEntity>();
	public DbSet<ClientApplicationTextResourceEntity> ClientApplicationTextResources => Set<ClientApplicationTextResourceEntity>();
	public DbSet<FormalityEntity> Formalities => Set<FormalityEntity>();
	public DbSet<LocaleEntity> Locales => Set<LocaleEntity>();
	public DbSet<PresentationEntity> Presentations => Set<PresentationEntity>();
	public DbSet<TextResourceEntity> TextResources => Set<TextResourceEntity>();
	public DbSet<TranslationEntity> Translations => Set<TranslationEntity>();
	public DbSet<FlatTranslationEntity> FlatTranslations => Set<FlatTranslationEntity>();

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
		
		new AuthPrincipalEntityConfiguration().Configure(modelBuilder.Entity<AuthPrincipalEntity>());
		new AuthPrincipalClientApplicationEntityConfiguration().Configure(modelBuilder.Entity<AuthPrincipalClientApplicationEntity>());
		new AuthPrincipalLocaleEntityConfiguration().Configure(modelBuilder.Entity<AuthPrincipalLocaleEntity>());
		new ClientApplicationEntityConfiguration().Configure(modelBuilder.Entity<ClientApplicationEntity>());
		new ClientApplicationLocaleEntityConfiguration().Configure(modelBuilder.Entity<ClientApplicationLocaleEntity>());
		new ClientApplicationTextResourceEntityConfiguration().Configure(modelBuilder.Entity<ClientApplicationTextResourceEntity>());
		new FormalityEntityConfiguration().Configure(modelBuilder.Entity<FormalityEntity>());
		new LocaleEntityConfiguration().Configure(modelBuilder.Entity<LocaleEntity>());
		new PresentationEntityConfiguration().Configure(modelBuilder.Entity<PresentationEntity>());
		new TextResourceEntityConfiguration().Configure(modelBuilder.Entity<TextResourceEntity>());
		new TranslationEntityConfiguration().Configure(modelBuilder.Entity<TranslationEntity>());
		new FlatTranslationEntityConfiguration().Configure(modelBuilder.Entity<FlatTranslationEntity>());
	}
}
