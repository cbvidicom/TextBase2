/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.
using Textbase.Application.Features.AuthPrincipals;
using Textbase.Application.Features.AuthPrincipalClientApplications;
using Textbase.Application.Features.AuthPrincipalLocales;
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

public partial class Commands(
	IAuthPrincipalCommands authPrincipalCommands,
	IAuthPrincipalClientApplicationCommands authPrincipalClientApplicationCommands,
	IAuthPrincipalLocaleCommands authPrincipalLocaleCommands,
	IClientApplicationCommands clientApplicationCommands,
	IClientApplicationLocaleCommands clientApplicationLocaleCommands,
	IClientApplicationTextResourceCommands clientApplicationTextResourceCommands,
	IFormalityCommands formalityCommands,
	ILocaleCommands localeCommands,
	IPresentationCommands presentationCommands,
	ITextResourceCommands textResourceCommands,
	ITranslationCommands translationCommands,
	IFlatTranslationCommands flatTranslationCommands
)
{
	public readonly IAuthPrincipalCommands AuthPrincipalCommands = authPrincipalCommands;
	public readonly IAuthPrincipalClientApplicationCommands AuthPrincipalClientApplicationCommands = authPrincipalClientApplicationCommands;
	public readonly IAuthPrincipalLocaleCommands AuthPrincipalLocaleCommands = authPrincipalLocaleCommands;
	public readonly IClientApplicationCommands ClientApplicationCommands = clientApplicationCommands;
	public readonly IClientApplicationLocaleCommands ClientApplicationLocaleCommands = clientApplicationLocaleCommands;
	public readonly IClientApplicationTextResourceCommands ClientApplicationTextResourceCommands = clientApplicationTextResourceCommands;
	public readonly IFormalityCommands FormalityCommands = formalityCommands;
	public readonly ILocaleCommands LocaleCommands = localeCommands;
	public readonly IPresentationCommands PresentationCommands = presentationCommands;
	public readonly ITextResourceCommands TextResourceCommands = textResourceCommands;
	public readonly ITranslationCommands TranslationCommands = translationCommands;
	public readonly IFlatTranslationCommands FlatTranslationCommands = flatTranslationCommands;
}