/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using DM = Textbase.Domain.Models;
using Textbase.Infrastructure.Persistence.Locales;
using Textbase.Infrastructure.Persistence.AuthPrincipals;

namespace Textbase.Infrastructure.Persistence.AuthPrincipalLocales;

public partial class AuthPrincipalLocaleEntity
	: DM.AuthPrincipalLocale
{
	public virtual LocaleEntity? Locale { get; set; }
	public virtual AuthPrincipalEntity? AuthPrincipal { get; set; }
}
