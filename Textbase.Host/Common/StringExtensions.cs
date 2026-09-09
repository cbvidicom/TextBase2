namespace Textbase.Host.Common;

public static class StringExtensions
{
	public static string? LimitLength(
		this string? value,
		int maximumLength)
		=> value is null || value.Length <= maximumLength
			? value
			: value[..maximumLength];
}
