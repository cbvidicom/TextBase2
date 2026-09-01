/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using DM = Textbase.Domain.Models;
using Textbase.Infrastructure.Persistence.Translations;

namespace Textbase.Infrastructure.Persistence.Formalities;

public partial class FormalityEntity
	: DM.Formality
{
	public virtual ICollection<TranslationEntity> Translations { get; set; } = [];
}
