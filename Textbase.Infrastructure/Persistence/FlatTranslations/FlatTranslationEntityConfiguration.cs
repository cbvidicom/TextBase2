/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Textbase.Infrastructure.Persistence.FlatTranslations;

internal sealed partial class FlatTranslationEntityConfiguration
	: IEntityTypeConfiguration<FlatTranslationEntity>
{
	public void Configure(EntityTypeBuilder<FlatTranslationEntity> builder)
	{
		builder.ToTable("Translation", "flat");

		builder.HasKey(e => new { e.LocaleKey, e.TextKey, e.FormalityKey, e.PresentationKey });
		builder.HasIndex(e => new {e.TextKey, e.LocaleKey });
	}
}
