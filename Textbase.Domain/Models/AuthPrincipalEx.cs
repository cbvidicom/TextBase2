using System.ComponentModel.DataAnnotations.Schema;
using Textbase.Domain.Enumerations;

namespace Textbase.Domain.Models;

public partial class AuthPrincipal
{
	[NotMapped]
	public Roles RolesValue
	{
		get => (Roles)Role;
		set => Role = (int)value;
	}

	[NotMapped]
	public PrincipalStatus StatusValue
	{
		get => (PrincipalStatus)Status;
		set => Status = (int)value;
	}
}
