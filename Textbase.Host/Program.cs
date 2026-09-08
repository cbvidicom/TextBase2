using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Radzen;
using Textbase.Application.Common;
using Textbase.Application.Features.ClientApplications;
using Textbase.Host;
using Textbase.Host.Api.Authorization;
using Textbase.Host.Authorization;
using Textbase.Host.Components.Infrastructure;
using Textbase.Host.ViewModels;
using Textbase.Infrastructure;
using Uwn.Blazor.Enumerations.Common;
using Uwn.Blazor.Extensions.Common;
using Uwn.Blazor.Models.Common;
using Uwn.Blazor.Services.Infrastructure;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("Textbase")
	?? throw new InvalidOperationException("Connection string 'Textbase' is not configured.");

builder.Services
	.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
	.AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAdB2C"));

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentPrincipalAccessor, CurrentPrincipalAccessor>();
builder.Services.AddScoped<IAuthorizationHandler, ActivePrincipalAuthorizationHandler>();

AuthorizationBase.RegisterAuthorizationServices(builder.Services);

builder.Services.AddAuthorizationBuilder()
	.SetDefaultPolicy(new AuthorizationPolicyBuilder()
	.RequireAuthenticatedUser()
	.AddRequirements(new ActivePrincipalRequirement())
	.Build());

builder.Services.AddTextbaseInfrastructure(connectionString);
builder.Services.AddTextbaseApplication(includeServerCommands: true);
builder.Services.AddScoped(services => (IClientApplicationServerQueries)services.GetRequiredService<IClientApplicationQueries>());

builder.Services.AddControllers(options =>
{
	options.Filters.Add(new AuthorizeFilter());
});

builder.Services
	.AddRazorComponents()
	.AddInteractiveServerComponents();

builder.Services.AddRadzenComponents();

builder.Services.AddUwRadzen(builder.Configuration);
builder.Services.AddViewModels<ViewModelMarker>();
builder.Services.Configure<QueryingOptions>(options =>
{
	options.AuthorizationMethods = AuthorizationMethods.Custom;
	options.DefaultPageSize = 20;
});

//

WebApplication app = builder.Build();

if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error", createScopeForErrors: true);
	app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

app.MapGet(StaticRoutes.SignIn, async (HttpContext context, string? returnUrl) =>
{
	string redirectUri = IsLocalReturnUrl(returnUrl) ? returnUrl! : StaticRoutes.Home;
	AuthenticationProperties properties = new()
	{
		RedirectUri = redirectUri
	};

	await context.ChallengeAsync(OpenIdConnectDefaults.AuthenticationScheme, properties);
}).AllowAnonymous();

app.MapGet(StaticRoutes.SignOut, async (HttpContext context) =>
{
	AuthenticationProperties properties = new()
	{
		RedirectUri = StaticRoutes.Home
	};

	await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
	await context.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme, properties);
}).AllowAnonymous();

app.MapControllers();

app.MapStaticAssets();
app.MapRazorComponents<App>()
	.AddInteractiveServerRenderMode();

app.Run();

static bool IsLocalReturnUrl(
	string? returnUrl)
	=> !String.IsNullOrWhiteSpace(returnUrl) &&
	returnUrl[0] == '/' &&
	(returnUrl.Length == 1 || returnUrl[1] != '/' && returnUrl[1] != '\\');
