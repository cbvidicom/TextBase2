/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.
using Textbase.Application.Features.ClientApplications;
using Textbase.Application.Features.ClientApplicationLocales;
using Textbase.Application.Features.ClientApplicationTextResources;
using Textbase.Application.Features.Formalities;
using Textbase.Application.Features.Locales;
using Textbase.Application.Features.Presentations;
using Textbase.Application.Features.TextResources;
using Textbase.Application.Features.Translations;
using Textbase.Application.Features.FlatTranslations;

namespace Textbase.Application.Features;

public partial class Queries(
	IClientApplicationQueries clientApplicationQueries,
	IClientApplicationLocaleQueries clientApplicationLocaleQueries,
	IClientApplicationTextResourceQueries clientApplicationTextResourceQueries,
	IFormalityQueries formalityQueries,
	ILocaleQueries localeQueries,
	IPresentationQueries presentationQueries,
	ITextResourceQueries textResourceQueries,
	ITranslationQueries translationQueries,
	IFlatTranslationQueries flatTranslationQueries
)
{
	public readonly IClientApplicationQueries ClientApplicationQueries = clientApplicationQueries;
	public readonly IClientApplicationLocaleQueries ClientApplicationLocaleQueries = clientApplicationLocaleQueries;
	public readonly IClientApplicationTextResourceQueries ClientApplicationTextResourceQueries = clientApplicationTextResourceQueries;
	public readonly IFormalityQueries FormalityQueries = formalityQueries;
	public readonly ILocaleQueries LocaleQueries = localeQueries;
	public readonly IPresentationQueries PresentationQueries = presentationQueries;
	public readonly ITextResourceQueries TextResourceQueries = textResourceQueries;
	public readonly ITranslationQueries TranslationQueries = translationQueries;
	public readonly IFlatTranslationQueries FlatTranslationQueries = flatTranslationQueries;
}