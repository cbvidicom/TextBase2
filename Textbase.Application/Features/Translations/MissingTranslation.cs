namespace Textbase.Application.Features.Translations;

public sealed record MissingTranslation(
	string LocaleKey,
	string TextKey,
	string Applications);
