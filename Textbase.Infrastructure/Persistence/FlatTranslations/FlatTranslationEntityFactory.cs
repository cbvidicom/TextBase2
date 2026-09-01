/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using Uwn.EntityFrameworkCore.Infrastructure;

namespace Textbase.Infrastructure.Persistence.FlatTranslations;

public sealed partial class FlatTranslationEntityFactory
	: EntityFactory<CM.FlatTranslationDto, DM.FlatTranslation, FlatTranslationEntity>
	, IFlatTranslationEntityFactory
{
	public override FlatTranslationEntity Create() => Create(default!, default!, default!, default!, default!, default!);

	public FlatTranslationEntity Create(string localeKey) => Create(localeKey, default!, default!, default!, default!, default!);
	public FlatTranslationEntity Create(string localeKey, string textKey) => Create(localeKey, textKey, default!, default!, default!, default!);
	public FlatTranslationEntity Create(string localeKey, string textKey, string formalityKey) => Create(localeKey, textKey, formalityKey, default!, default!, default!);
	public FlatTranslationEntity Create(string localeKey, string textKey, string formalityKey, string presentationKey) => Create(localeKey, textKey, formalityKey, presentationKey, default!, default!);
	public FlatTranslationEntity Create(string localeKey, string textKey, string formalityKey, string presentationKey, string sourceLocaleKey) => Create(localeKey, textKey, formalityKey, presentationKey, sourceLocaleKey, default!);
	public FlatTranslationEntity Create(string localeKey, string textKey, string formalityKey, string presentationKey, string sourceLocaleKey, string value)
	=> new() { LocaleKey = localeKey, TextKey = textKey, FormalityKey = formalityKey, PresentationKey = presentationKey, SourceLocaleKey = sourceLocaleKey, Value = value };
}
