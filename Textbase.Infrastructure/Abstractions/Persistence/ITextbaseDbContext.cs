/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.
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
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Textbase.Infrastructure.Abstractions.Persistence;

public partial interface ITextbaseDbContext
{
	DbSet<ClientApplicationEntity> ClientApplications { get; }
	DbSet<ClientApplicationLocaleEntity> ClientApplicationLocales { get; }
	DbSet<ClientApplicationTextResourceEntity> ClientApplicationTextResources { get; }
	DbSet<FormalityEntity> Formalities { get; }
	DbSet<LocaleEntity> Locales { get; }
	DbSet<PresentationEntity> Presentations { get; }
	DbSet<TextResourceEntity> TextResources { get; }
	DbSet<TranslationEntity> Translations { get; }
	DbSet<FlatTranslationEntity> FlatTranslations { get; }

	EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
	Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
