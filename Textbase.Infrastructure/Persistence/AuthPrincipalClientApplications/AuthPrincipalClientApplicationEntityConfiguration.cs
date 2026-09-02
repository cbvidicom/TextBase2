/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Textbase.Infrastructure.Persistence.AuthPrincipalClientApplications;

internal sealed partial class AuthPrincipalClientApplicationEntityConfiguration
	: IEntityTypeConfiguration<AuthPrincipalClientApplicationEntity>
{
	public void Configure(EntityTypeBuilder<AuthPrincipalClientApplicationEntity> builder)
	{
		builder.ToTable("PrincipalClientApplication", "auth");

		builder.HasKey(e => new { e.EntraObjectId, e.ClientApplicationGuid });
		builder.HasIndex(e => new {e.ClientApplicationGuid });
		builder.HasIndex(e => new {e.EntraObjectId });
		builder.HasOne(e => e.ClientApplication).WithMany(e => e.AuthPrincipalClientApplications).HasForeignKey(e => new { e.ClientApplicationGuid }).HasPrincipalKey(e => new { e.ClientApplicationGuid }).OnDelete(DeleteBehavior.NoAction);
		builder.HasOne(e => e.AuthPrincipal).WithMany(e => e.AuthPrincipalClientApplications).HasForeignKey(e => new { e.EntraObjectId }).HasPrincipalKey(e => new { e.EntraObjectId }).OnDelete(DeleteBehavior.NoAction);
	}
}
