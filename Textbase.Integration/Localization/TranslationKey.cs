namespace Textbase.Integration.Localization;

public readonly record struct TranslationKey(
	string TextKey,
	string LocaleKey,
	string FormalityKey,
	string PresentationKey);
