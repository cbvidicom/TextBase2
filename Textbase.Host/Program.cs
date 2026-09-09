using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using Radzen;
using Textbase.Application.Common;
using Textbase.Application.Features.ClientApplications;
using Textbase.Host.Api.Authorization;
using Textbase.Host.Api.Infrastructure;
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

IConfigurationSection azureAdB2C = builder.Configuration.GetSection("AzureAdB2C");

builder.Services
	.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
	.AddMicrosoftIdentityWebApp(azureAdB2C)
	.EnableTokenAcquisitionToCallDownstreamApi()
	.AddDistributedTokenCaches();

builder.Services
	.AddAuthentication()
	.AddMicrosoftIdentityWebApi(azureAdB2C, JwtBearerDefaults.AuthenticationScheme);

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentPrincipalAccessor, CurrentPrincipalAccessor>();
builder.Services.AddScoped<IAuthorizationHandler, ActivePrincipalAuthorizationHandler>();

AuthorizationBase.RegisterAuthorizationServices(builder.Services);

builder.Services.AddAuthorizationBuilder()
	.SetDefaultPolicy(new AuthorizationPolicyBuilder(CookieAuthenticationDefaults.AuthenticationScheme)
	.RequireAuthenticatedUser()
	.AddRequirements(new ActivePrincipalRequirement())
	.Build())
	.AddPolicy(ApiAuthorizationConvention.PolicyName, policy =>
	{
		policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
		policy.RequireAuthenticatedUser();
		policy.AddRequirements(new ActivePrincipalRequirement());
	});

builder.Services.AddTextbaseInfrastructure(connectionString);
builder.Services.AddTextbaseApplication(includeServerCommands: true);
builder.Services.AddScoped(services => (IClientApplicationServerQueries)services.GetRequiredService<IClientApplicationQueries>());

builder.Services
	.AddControllersWithViews(options =>
	{
		options.Conventions.Add(new ApiAuthorizationConvention());
	})
	.AddMicrosoftIdentityUI();

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

app.MapControllers();

app.MapStaticAssets();
app.MapRazorComponents<App>()
	.AddInteractiveServerRenderMode();

app.Run();
