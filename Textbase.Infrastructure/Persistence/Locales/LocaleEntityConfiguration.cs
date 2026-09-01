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
		builder.HasOne(e => e.Locale).WithMany(e => e.Locales).HasForeignKey(e => new { e.ParentLocaleKey }).HasPrincipalKey(e => new { e.LocaleKey }).OnDelete(DeleteBehavior.NoAction);
	}
}
