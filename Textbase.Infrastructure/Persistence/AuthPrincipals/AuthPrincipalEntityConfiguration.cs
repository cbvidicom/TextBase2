/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Textbase.Infrastructure.Persistence.AuthPrincipals;

internal sealed partial class AuthPrincipalEntityConfiguration
	: IEntityTypeConfiguration<AuthPrincipalEntity>
{
	public void Configure(EntityTypeBuilder<AuthPrincipalEntity> builder)
	{
		builder.ToTable("Principal", "auth");

		builder.HasKey(e => new { e.EntraObjectId });
		builder.Property(e => e.EntraObjectId).IsRequired();
		builder.Property(e => e.Role).IsRequired();
		builder.Property(e => e.DisplayName).HasMaxLength(128).IsUnicode(true);
		builder.Property(e => e.EmailAddress).HasMaxLength(256).IsUnicode(true);
		builder.Property(e => e.IsActive).IsRequired();
	}
}
