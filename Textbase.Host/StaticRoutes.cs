namespace Textbase.Host;

public static class StaticRoutes
{
	public const string ClientApplicationList = "/client-applications";
	public const string ClientApplicationCreate = "/client-application";
	public const string ClientApplicationEdit = "/client-application/{0}";

	public const string Home = "/";

	public const string SignIn = "/MicrosoftIdentity/Account/SignIn";
	public const string SignOut = "/MicrosoftIdentity/Account/SignOut";
}
