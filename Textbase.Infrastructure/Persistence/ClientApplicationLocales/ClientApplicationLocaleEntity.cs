/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using DM = Textbase.Domain.Models;
using Textbase.Infrastructure.Persistence.ClientApplications;
using Textbase.Infrastructure.Persistence.Locales;

namespace Textbase.Infrastructure.Persistence.ClientApplicationLocales;

public partial class ClientApplicationLocaleEntity
	: DM.ClientApplicationLocale
{
	public virtual ClientApplicationEntity? ClientApplication { get; set; }
	public virtual LocaleEntity? Locale { get; set; }
}
