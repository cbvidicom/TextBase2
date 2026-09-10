namespace Textbase.Host;

public static class StaticRoutes
{
	public const string AuthPrincipalList = "/principals";
	public const string AuthPrincipalEdit = "/principal/{0}";

	public const string ClientApplicationList = "/client-applications";
	public const string ClientApplicationCreate = "/client-application";
	public const string ClientApplicationEdit = "/client-application/{0}";

	public const string FormalityList = "/formalities";
	public const string FormalityCreate = "/formality";
	public const string FormalityEdit = "/formality/{0}";

	public const string Home = "/";

	public const string LocaleList = "/locales";
	public const string LocaleEdit = "/locale/{0}";

	public const string PresentationList = "/presentations";
	public const string PresentationCreate = "/presentation";
	public const string PresentationEdit = "/presentation/{0}";

	public const string TextResourceList = "/text-resources";
	public const string TextResourceCreate = "/text-resource";
	public const string TextResourceEdit = "/text-resource/{0}";

	public const string SignIn = "/MicrosoftIdentity/Account/SignIn";
	public const string SignOut = "/MicrosoftIdentity/Account/SignOut";
}
