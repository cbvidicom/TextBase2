/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Textbase.Infrastructure.Persistence.Presentations;

internal sealed partial class PresentationEntityConfiguration
	: IEntityTypeConfiguration<PresentationEntity>
{
	public void Configure(EntityTypeBuilder<PresentationEntity> builder)
	{
		builder.ToTable("Presentation", "dbo", t => t.UseSqlOutputClause(false));

		builder.HasKey(e => new { e.PresentationKey });
	}
}
