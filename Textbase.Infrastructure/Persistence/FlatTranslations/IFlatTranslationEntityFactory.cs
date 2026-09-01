/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using Uwn.EntityFrameworkCore.Infrastructure;

namespace Textbase.Infrastructure.Persistence.FlatTranslations;

public partial interface IFlatTranslationEntityFactory
	: IEntityFactory<CM.FlatTranslationDto, DM.FlatTranslation, FlatTranslationEntity>
{
	FlatTranslationEntity Create(string localeKey);
	FlatTranslationEntity Create(string localeKey, string textKey);
	FlatTranslationEntity Create(string localeKey, string textKey, string formalityKey);
	FlatTranslationEntity Create(string localeKey, string textKey, string formalityKey, string presentationKey);
	FlatTranslationEntity Create(string localeKey, string textKey, string formalityKey, string presentationKey, string sourceLocaleKey);
	FlatTranslationEntity Create(string localeKey, string textKey, string formalityKey, string presentationKey, string sourceLocaleKey, string value);
}