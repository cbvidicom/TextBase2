/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using Uwn.EntityFrameworkCore.Infrastructure;

namespace Textbase.Infrastructure.Persistence.Locales;

public sealed partial class LocaleEntityFactory
	: EntityFactory<CM.LocaleDto, DM.Locale, LocaleEntity>
	, ILocaleEntityFactory
{
	public override LocaleEntity Create() => Create(default!, default!, default!);

	public LocaleEntity Create(string localeKey) => Create(localeKey, default!, default!);
	public LocaleEntity Create(string localeKey, string nativeName) => Create(localeKey, nativeName, default!);
	public LocaleEntity Create(string localeKey, string nativeName, string englishName)
	=> new() { LocaleKey = localeKey, NativeName = nativeName, EnglishName = englishName };
}
