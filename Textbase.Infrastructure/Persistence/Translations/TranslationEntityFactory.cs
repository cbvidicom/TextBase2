/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using Uwn.EntityFrameworkCore.Infrastructure;

namespace Textbase.Infrastructure.Persistence.Translations;

public sealed partial class TranslationEntityFactory
	: EntityFactory<CM.TranslationDto, DM.Translation, TranslationEntity>
	, ITranslationEntityFactory
{
	public override TranslationEntity Create() => Create(default!, default!, default!, default!, default!);

	public TranslationEntity Create(string localeKey) => Create(localeKey, default!, default!, default!, default!);
	public TranslationEntity Create(string localeKey, string textKey) => Create(localeKey, textKey, default!, default!, default!);
	public TranslationEntity Create(string localeKey, string textKey, string formalityKey) => Create(localeKey, textKey, formalityKey, default!, default!);
	public TranslationEntity Create(string localeKey, string textKey, string formalityKey, string presentationKey) => Create(localeKey, textKey, formalityKey, presentationKey, default!);
	public TranslationEntity Create(string localeKey, string textKey, string formalityKey, string presentationKey, string value)
	=> new() { LocaleKey = localeKey, TextKey = textKey, FormalityKey = formalityKey, PresentationKey = presentationKey, Value = value };
}
