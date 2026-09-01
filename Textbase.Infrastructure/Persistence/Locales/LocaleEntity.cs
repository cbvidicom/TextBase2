/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using DM = Textbase.Domain.Models;
using Textbase.Infrastructure.Persistence.Locales;
using Textbase.Infrastructure.Persistence.ClientApplicationLocales;
using Textbase.Infrastructure.Persistence.Translations;

namespace Textbase.Infrastructure.Persistence.Locales;

public partial class LocaleEntity
	: DM.Locale
{
	public virtual LocaleEntity? Locale { get; set; }
	public virtual ICollection<ClientApplicationLocaleEntity> ClientApplicationLocales { get; set; } = [];
	public virtual ICollection<LocaleEntity> Locales { get; set; } = [];
	public virtual ICollection<TranslationEntity> Translations { get; set; } = [];
}
