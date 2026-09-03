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
		builder.Property(e => e.LocaleKey).IsRequired().HasMaxLength(85).IsUnicode(false);
		builder.Property(e => e.SourceLocaleKey).IsRequired().HasMaxLength(85).IsUnicode(false);
		builder.Property(e => e.TextKey).IsRequired().HasMaxLength(128).IsUnicode(false);
		builder.Property(e => e.FormalityKey).IsRequired().HasMaxLength(16).IsUnicode(false);
		builder.Property(e => e.PresentationKey).IsRequired().HasMaxLength(16).IsUnicode(false);
		builder.Property(e => e.Value).IsRequired().IsUnicode(true);
		builder.HasIndex(e => new {e.TextKey, e.LocaleKey });
	}
}
