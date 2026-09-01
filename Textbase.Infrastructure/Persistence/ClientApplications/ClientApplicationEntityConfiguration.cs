/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Textbase.Infrastructure.Persistence.ClientApplications;

internal sealed partial class ClientApplicationEntityConfiguration
	: IEntityTypeConfiguration<ClientApplicationEntity>
{
	public void Configure(EntityTypeBuilder<ClientApplicationEntity> builder)
	{
		builder.ToTable("ClientApplication", "dbo", t => t.UseSqlOutputClause(false));

		builder.HasKey(e => new { e.ClientApplicationGuid });
	}
}
