/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using DM = Textbase.Domain.Models;
using Textbase.Infrastructure.Persistence.ClientApplicationTextResources;
using Textbase.Infrastructure.Persistence.Translations;

namespace Textbase.Infrastructure.Persistence.TextResources;

public partial class TextResourceEntity
	: DM.TextResource
{
	public virtual ICollection<ClientApplicationTextResourceEntity> ClientApplicationTextResources { get; set; } = [];
	public virtual ICollection<TranslationEntity> Translations { get; set; } = [];
}
