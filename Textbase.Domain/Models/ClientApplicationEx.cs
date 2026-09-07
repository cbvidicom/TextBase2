using System.ComponentModel.DataAnnotations.Schema;
using Textbase.Contracts.Enumerations;

namespace Textbase.Domain.Models;

public partial class ClientApplication
{
	[NotMapped]
	public LanguageTag? DefaultLanguageTagValue
	{
		get => LanguageTagExtensions.FromDisplayText(DefaultLanguageTag);
		set => DefaultLanguageTag = value.GetDisplayText();
	}
}
