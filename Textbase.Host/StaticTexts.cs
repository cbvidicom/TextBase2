using Textbase.Host.Enumerations;

namespace Textbase.Host;

public static class StaticTexts
{
	public const string PrincipalNotAuthorizedTo01 = "You are not authorized to {0} an item of type '{1}'.";

	//

	public static string GetPrincipalNotAuthorizedText(
		OpType opType,
		Type type)
		=> String.Format(PrincipalNotAuthorizedTo01, opType.ToString(), type.Name);
}
