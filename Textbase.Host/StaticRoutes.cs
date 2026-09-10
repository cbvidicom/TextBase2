namespace Textbase.Host;

public static class StaticRoutes
{
	public const string AuthPrincipalList = "/users";
	public const string AuthPrincipalEdit = "/user/{0}";

	public const string ClientApplicationList = "/client-applications";
	public const string ClientApplicationCreate = "/client-application";
	public const string ClientApplicationEdit = "/client-application/{0}";

	public const string FormalityList = "/formalities";
	public const string FormalityCreate = "/formality";
	public const string FormalityEdit = "/formality/{0}";

	public const string Home = "/";

	public const string PresentationList = "/presentations";
	public const string PresentationCreate = "/presentation";
	public const string PresentationEdit = "/presentation/{0}";

	public const string SignIn = "/MicrosoftIdentity/Account/SignIn";
	public const string SignOut = "/MicrosoftIdentity/Account/SignOut";
}
