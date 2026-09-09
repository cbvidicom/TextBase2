namespace Textbase.Domain.Models;

public partial class Locale
{
	public override string ToString()
		=> $"{LocaleKey} ({EnglishName})";
}
