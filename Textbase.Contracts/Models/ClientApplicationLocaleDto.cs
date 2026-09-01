/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

namespace Textbase.Contracts.Models;

public partial class ClientApplicationLocaleDto
{
	public required Guid ClientApplicationGuid { get; set; }
	public required string LocaleKey { get; set; }
	public required bool IsDefault { get; set; }
}
