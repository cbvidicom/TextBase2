/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Textbase.Infrastructure.Persistence.ClientApplicationLocales;

internal sealed partial class ClientApplicationLocaleEntityConfiguration
	: IEntityTypeConfiguration<ClientApplicationLocaleEntity>
{
	public void Configure(EntityTypeBuilder<ClientApplicationLocaleEntity> builder)
	{
		builder.ToTable("ClientApplicationLocale", "dbo", t => t.UseSqlOutputClause(false));

		builder.HasKey(e => new { e.ClientApplicationGuid, e.LocaleKey });
		builder.Property(e => e.ClientApplicationGuid).IsRequired();
		builder.Property(e => e.LocaleKey).IsRequired().HasMaxLength(85).IsUnicode(false);
		builder.Property(e => e.IsDefault).IsRequired();
		builder.HasIndex(e => new {e.ClientApplicationGuid });
		builder.HasIndex(e => new {e.LocaleKey });
		builder.HasIndex(e => new {e.ClientApplicationGuid }).IsUnique().HasFilter("([IsDefault]=(1))");
		builder.HasOne(e => e.ClientApplication).WithMany(e => e.ClientApplicationLocales).HasForeignKey(e => new { e.ClientApplicationGuid }).HasPrincipalKey(e => new { e.ClientApplicationGuid }).OnDelete(DeleteBehavior.NoAction);
		builder.HasOne(e => e.Locale).WithMany(e => e.ClientApplicationLocales).HasForeignKey(e => new { e.LocaleKey }).HasPrincipalKey(e => new { e.LocaleKey }).OnDelete(DeleteBehavior.NoAction);
	}
}
