/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

namespace Textbase.Contracts.Models;

public partial class ClientApplicationDto
{
	public required Guid ClientApplicationGuid { get; set; }
	public required string Name { get; set; }
	public string? Description { get; set; }
	public string? DefaultLanguageTag { get; set; }
	public string? DefaultFormat { get; set; }
	public string? DefaultFileName { get; set; }
	public required bool IsActive { get; set; }
}
