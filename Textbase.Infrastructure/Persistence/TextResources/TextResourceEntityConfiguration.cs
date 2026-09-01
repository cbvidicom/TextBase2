/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Textbase.Infrastructure.Persistence.TextResources;

internal sealed partial class TextResourceEntityConfiguration
	: IEntityTypeConfiguration<TextResourceEntity>
{
	public void Configure(EntityTypeBuilder<TextResourceEntity> builder)
	{
		builder.ToTable("TextResource", "dbo");

		builder.HasKey(e => new { e.TextKey });
	}
}
