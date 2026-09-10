namespace Textbase.Application.Features.TextResources;

public sealed record TextResourceReferenceCounts(
	string TextKey,
	int ClientApplicationCount,
	int LocaleCount,
	int TranslationCount);
