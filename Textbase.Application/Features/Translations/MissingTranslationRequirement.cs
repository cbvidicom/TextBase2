namespace Textbase.Application.Features.Translations;

public sealed record MissingTranslationRequirement(
	Guid ClientApplicationGuid,
	string ClientApplicationName,
	string LocaleKey,
	string TextKey);
