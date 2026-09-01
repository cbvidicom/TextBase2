/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Textbase.Infrastructure.Persistence.Translations;

internal sealed partial class TranslationEntityConfiguration
	: IEntityTypeConfiguration<TranslationEntity>
{
	public void Configure(EntityTypeBuilder<TranslationEntity> builder)
	{
		builder.ToTable("Translation", "dbo", t => t.UseSqlOutputClause(false));

		builder.HasKey(e => new { e.LocaleKey, e.TextKey, e.FormalityKey, e.PresentationKey });
		builder.HasIndex(e => new {e.LocaleKey });
		builder.HasIndex(e => new {e.TextKey });
		builder.HasOne(e => e.Formality).WithMany(e => e.Translations).HasForeignKey(e => new { e.FormalityKey }).HasPrincipalKey(e => new { e.FormalityKey }).OnDelete(DeleteBehavior.NoAction);
		builder.HasOne(e => e.Locale).WithMany(e => e.Translations).HasForeignKey(e => new { e.LocaleKey }).HasPrincipalKey(e => new { e.LocaleKey }).OnDelete(DeleteBehavior.NoAction);
		builder.HasOne(e => e.Presentation).WithMany(e => e.Translations).HasForeignKey(e => new { e.PresentationKey }).HasPrincipalKey(e => new { e.PresentationKey }).OnDelete(DeleteBehavior.NoAction);
		builder.HasOne(e => e.TextResource).WithMany(e => e.Translations).HasForeignKey(e => new { e.TextKey }).HasPrincipalKey(e => new { e.TextKey }).OnDelete(DeleteBehavior.NoAction);
	}
}
