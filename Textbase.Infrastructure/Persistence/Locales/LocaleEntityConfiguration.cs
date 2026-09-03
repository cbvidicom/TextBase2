/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Textbase.Infrastructure.Persistence.Locales;

internal sealed partial class LocaleEntityConfiguration
	: IEntityTypeConfiguration<LocaleEntity>
{
	public void Configure(EntityTypeBuilder<LocaleEntity> builder)
	{
		builder.ToTable("Locale", "dbo", t => t.UseSqlOutputClause(false));

		builder.HasKey(e => new { e.LocaleKey });
		builder.Property(e => e.LocaleKey).IsRequired().HasMaxLength(85).IsUnicode(false);
		builder.Property(e => e.ParentLocaleKey).HasMaxLength(85).IsUnicode(false);
		builder.Property(e => e.LanguageIso2).HasMaxLength(2).IsUnicode(false).IsFixedLength();
		builder.Property(e => e.LanguageIso3).HasMaxLength(3).IsUnicode(false).IsFixedLength();
		builder.Property(e => e.LanguageIsoN);
		builder.Property(e => e.LanguageLCID);
		builder.Property(e => e.LanguageWinApi).HasMaxLength(3).IsUnicode(false).IsFixedLength();
		builder.Property(e => e.CountryIso2).HasMaxLength(2).IsUnicode(false).IsFixedLength();
		builder.Property(e => e.CountryIso3).HasMaxLength(3).IsUnicode(false).IsFixedLength();
		builder.Property(e => e.NativeName).IsRequired().HasMaxLength(128).IsUnicode(true);
		builder.Property(e => e.EnglishName).IsRequired().HasMaxLength(128).IsUnicode(true);
		builder.HasOne(e => e.Locale).WithMany(e => e.Locales).HasForeignKey(e => new { e.ParentLocaleKey }).HasPrincipalKey(e => new { e.LocaleKey }).OnDelete(DeleteBehavior.NoAction);
	}
}
