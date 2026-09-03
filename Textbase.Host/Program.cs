using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Identity.Web;
using Textbase.Application.Common;
using Textbase.Host.Api.Authorization;
using Textbase.Host.Authorization;
using Textbase.Host.Components;
using Textbase.Infrastructure;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("Textbase")
	?? throw new InvalidOperationException("Connection string 'Textbase' is not configured.");

builder.Services
	.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAdB2C"));

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentPrincipalAccessor, CurrentPrincipalAccessor>();
builder.Services.AddScoped<IAuthorizationHandler, ActivePrincipalAuthorizationHandler>();
builder.Services.AddScoped<AuthorizationScope>();
builder.Services.AddScoped<IAuthPrincipalAuthorization, AuthPrincipalAuthorization>();
builder.Services.AddScoped<IAuthPrincipalClientApplicationAuthorization, AuthPrincipalClientApplicationAuthorization>();
builder.Services.AddScoped<IAuthPrincipalLocaleAuthorization, AuthPrincipalLocaleAuthorization>();
builder.Services.AddScoped<IClientApplicationAuthorization, ClientApplicationAuthorization>();
builder.Services.AddScoped<IClientApplicationLocaleAuthorization, ClientApplicationLocaleAuthorization>();
builder.Services.AddScoped<IClientApplicationTextResourceAuthorization, ClientApplicationTextResourceAuthorization>();
builder.Services.AddScoped<IFlatTranslationAuthorization, FlatTranslationAuthorization>();
builder.Services.AddScoped<IFormalityAuthorization, FormalityAuthorization>();
builder.Services.AddScoped<ILocaleAuthorization, LocaleAuthorization>();
builder.Services.AddScoped<IPresentationAuthorization, PresentationAuthorization>();
builder.Services.AddScoped<ITextResourceAuthorization, TextResourceAuthorization>();
builder.Services.AddScoped<ITranslationAuthorization, TranslationAuthorization>();

builder.Services.AddAuthorization(options =>
{
	options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
		.RequireAuthenticatedUser()
		.AddRequirements(new ActivePrincipalRequirement())
		.Build();
});

builder.Services.AddTextbaseInfrastructure(connectionString);
builder.Services.AddTextbaseApplication(includeServerCommands: true);

builder.Services.AddControllers(options =>
{
	options.Filters.Add(new AuthorizeFilter());
});

builder.Services
	.AddRazorComponents()
	.AddInteractiveServerComponents();

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
