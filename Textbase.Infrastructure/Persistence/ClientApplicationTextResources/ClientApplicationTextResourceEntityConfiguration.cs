/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Textbase.Infrastructure.Persistence.ClientApplicationTextResources;

internal sealed partial class ClientApplicationTextResourceEntityConfiguration
	: IEntityTypeConfiguration<ClientApplicationTextResourceEntity>
{
	public void Configure(EntityTypeBuilder<ClientApplicationTextResourceEntity> builder)
	{
		builder.ToTable("ClientApplicationTextResource", "dbo", t => t.UseSqlOutputClause(false));

		builder.HasKey(e => new { e.ClientApplicationGuid, e.TextKey });
		builder.HasIndex(e => new {e.ClientApplicationGuid });
		builder.HasIndex(e => new {e.TextKey });
		builder.HasOne(e => e.ClientApplication).WithMany(e => e.ClientApplicationTextResources).HasForeignKey(e => new { e.ClientApplicationGuid }).HasPrincipalKey(e => new { e.ClientApplicationGuid }).OnDelete(DeleteBehavior.NoAction);
		builder.HasOne(e => e.TextResource).WithMany(e => e.ClientApplicationTextResources).HasForeignKey(e => new { e.TextKey }).HasPrincipalKey(e => new { e.TextKey }).OnDelete(DeleteBehavior.NoAction);
	}
}
