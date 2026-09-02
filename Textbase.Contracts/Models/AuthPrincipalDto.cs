/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

namespace Textbase.Contracts.Models;

public partial class AuthPrincipalDto
{
	public required Guid EntraObjectId { get; set; }
	public required int Role { get; set; }
	public string? DisplayName { get; set; }
	public string? EmailAddress { get; set; }
	public required bool IsActive { get; set; }
}
