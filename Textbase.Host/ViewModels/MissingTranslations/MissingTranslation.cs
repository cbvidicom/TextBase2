namespace Textbase.Host.ViewModels.MissingTranslations;

public sealed record MissingTranslation(
	string LocaleKey,
	string TextKey,
	string Applications);
