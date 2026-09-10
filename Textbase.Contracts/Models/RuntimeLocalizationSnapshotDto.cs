namespace Textbase.Contracts.Models;

public sealed class RuntimeLocalizationSnapshotDto
{
	public required string DefaultLocaleKey { get; set; }
	public required IReadOnlyList<string> SupportedLocaleKeys { get; set; }
	public required IReadOnlyList<FlatTranslationDto> Translations { get; set; }
}
