/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using DM = Textbase.Domain.Models;
using Textbase.Infrastructure.Persistence.Formalities;
using Textbase.Infrastructure.Persistence.Locales;
using Textbase.Infrastructure.Persistence.Presentations;
using Textbase.Infrastructure.Persistence.TextResources;

namespace Textbase.Infrastructure.Persistence.Translations;

public partial class TranslationEntity
	: DM.Translation
{
	public virtual FormalityEntity? Formality { get; set; }
	public virtual LocaleEntity? Locale { get; set; }
	public virtual PresentationEntity? Presentation { get; set; }
	public virtual TextResourceEntity? TextResource { get; set; }
}
