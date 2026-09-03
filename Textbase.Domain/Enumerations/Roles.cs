namespace Textbase.Domain.Enumerations;

[Flags]
public enum Roles
{
	None = 0,

	SysAdmin = 1 << 0,
	AppAdmin = 1 << 1,
	Translator = 1 << 2,
	Consumer = 1 << 3
}
