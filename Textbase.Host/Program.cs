using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Radzen;
using Textbase.Application.Common;
using Textbase.Application.Features.ClientApplications;
using Textbase.Host.Api.Authorization;
using Textbase.Host.Authorization;
using Textbase.Host.Components.Infrastructure;
using Textbase.Host.ViewModels;
using Textbase.Infrastructure;
using Uwn.Blazor.Extensions.Common;
using Uwn.Blazor.Services.Infrastructure;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("Textbase")
	?? throw new InvalidOperationException("Connection string 'Textbase' is not configured.");

builder.Services
	.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAdB2C"));

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentPrincipalAccessor, CurrentPrincipalAccessor>();
builder.Services.AddScoped<IAuthorizationHandler, ActivePrincipalAuthorizationHandler>();

AuthorizationBase.RegisterAuthorizationServices(builder.Services);

builder.Services.AddAuthorizationBuilder()
	.SetDefaultPolicy(new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
	.RequireAuthenticatedUser()
	.AddRequirements(new ActivePrincipalRequirement())
	.Build());

builder.Services.AddTextbaseInfrastructure(connectionString);
builder.Services.AddTextbaseApplication(includeServerCommands: true);
builder.Services.AddScoped<IClientApplicationServerQueries>(services => (IClientApplicationServerQueries)services.GetRequiredService<IClientApplicationQueries>());

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
