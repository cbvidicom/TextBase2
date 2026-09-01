/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Textbase.Infrastructure.Persistence.Formalities;

internal sealed partial class FormalityEntityConfiguration
	: IEntityTypeConfiguration<FormalityEntity>
{
	public void Configure(EntityTypeBuilder<FormalityEntity> builder)
	{
		builder.ToTable("Formality", "dbo", t => t.UseSqlOutputClause(false));

		builder.HasKey(e => new { e.FormalityKey });
	}
}
