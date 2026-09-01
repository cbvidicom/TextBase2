/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using Uwn.EntityFrameworkCore.Infrastructure;

namespace Textbase.Infrastructure.Persistence.Translations;

public partial interface ITranslationEntityFactory
	: IEntityFactory<CM.TranslationDto, DM.Translation, TranslationEntity>
{
	TranslationEntity Create(string localeKey);
	TranslationEntity Create(string localeKey, string textKey);
	TranslationEntity Create(string localeKey, string textKey, string formalityKey);
	TranslationEntity Create(string localeKey, string textKey, string formalityKey, string presentationKey);
	TranslationEntity Create(string localeKey, string textKey, string formalityKey, string presentationKey, string value);
}