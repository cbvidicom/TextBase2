namespace Textbase.Domain.Enumerations;

[Flags]
public enum Roles
{
	None = 0,

	Sysadmin = 1 << 0,
	Appadmin = 1 << 1,
	Translator = 1 << 2,
	Consumer = 1 << 3
}
