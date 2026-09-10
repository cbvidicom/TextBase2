using Textbase.Demo.Web.Components;
using Textbase.Demo.Web.Localization;
using Textbase.Integration.Api.Rest;
using Textbase.Integration.Localization;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents().AddInteractiveServerComponents();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddHttpClient();

builder.Services.AddSingleton<ITranslationStore, DistributedTranslationStore>();
builder.Services.AddSingleton<ITranslationSnapshotClient>(services =>
{
	IHttpClientFactory httpClientFactory = services.GetRequiredService<IHttpClientFactory>();
	string baseUrl = builder.Configuration["Textbase:BaseUrl"] ?? throw new InvalidOperationException("Textbase:BaseUrl is not configured.");
	RestClients clients = new(baseUrl, httpClient: httpClientFactory.CreateClient());
	return clients.Textbase.FlatTranslationsClient;
});
builder.Services.AddSingleton<TextbaseTranslationProvider>();

builder.Services.AddScoped<TextbaseContext>(_ => new TextbaseContext(String.Empty));
builder.Services.AddScoped<ITextbaseContext>(services => services.GetRequiredService<TextbaseContext>());
builder.Services.AddScoped<TextbaseLocalizer>();
builder.Services.AddScoped<ITextbaseLocalizer>(services => services.GetRequiredService<TextbaseLocalizer>());
builder.Services.AddScoped<DemoLocalizationSession>();

WebApplication app = builder.Build();

if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error", createScopeForErrors: true);
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAntiforgery();
app.MapStaticAssets();
app.MapRazorComponents<App>().AddInteractiveServerRenderMode();
app.Run();
