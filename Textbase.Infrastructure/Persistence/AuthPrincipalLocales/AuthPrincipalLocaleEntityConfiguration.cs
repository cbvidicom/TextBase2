/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Textbase.Infrastructure.Persistence.AuthPrincipalLocales;

internal sealed partial class AuthPrincipalLocaleEntityConfiguration
	: IEntityTypeConfiguration<AuthPrincipalLocaleEntity>
{
	public void Configure(EntityTypeBuilder<AuthPrincipalLocaleEntity> builder)
	{
		builder.ToTable("PrincipalLocale", "auth");

		builder.HasKey(e => new { e.EntraObjectId, e.LocaleKey });
		builder.HasIndex(e => new {e.EntraObjectId });
		builder.HasIndex(e => new {e.LocaleKey });
		builder.HasOne(e => e.Locale).WithMany(e => e.AuthPrincipalLocales).HasForeignKey(e => new { e.LocaleKey }).HasPrincipalKey(e => new { e.LocaleKey }).OnDelete(DeleteBehavior.NoAction);
		builder.HasOne(e => e.AuthPrincipal).WithMany(e => e.AuthPrincipalLocales).HasForeignKey(e => new { e.EntraObjectId }).HasPrincipalKey(e => new { e.EntraObjectId }).OnDelete(DeleteBehavior.NoAction);
	}
}
