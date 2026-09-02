using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Textbase.Cli;
using Textbase.Infrastructure;
using Uwn.Common.IO;

Terminal.Initialize("Textbase CLI");

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("Textbase")
	?? throw new InvalidOperationException("Connection string 'Textbase' is not configured.");

builder.Services.AddTextbaseInfrastructure(connectionString);
builder.Services.AddScoped<LocaleSynchronizer>();

using IHost host = builder.Build();

while (true)
{
	Terminal.Clear();
	Terminal.MainHeader("Textbase CLI");
	Terminal.Empty();

	Terminal.StartMenu("Select an option:");
	ConsoleKey addMissingLocalesKey = Terminal.MenuItem("Add missing locales");
	ConsoleKey key = Terminal.EndMenu();

	Terminal.Empty();

	if (key == Terminal.CancelKey)
		return;

	if (key == addMissingLocalesKey)
	{
		try
		{
			LocaleSynchronizer synchronizer = host.Services.GetRequiredService<LocaleSynchronizer>();
			LocaleSynchronizationResult result = await synchronizer.SynchronizeAsync();

			Terminal.Success($"Locale synchronization completed. Added: {result.Added:N0}; updated: {result.Updated:N0}; unchanged: {result.Unchanged:N0}.");
		}
		catch (Exception exception)
		{
			Terminal.Error(exception.Message);
		}
	}

	Terminal.Empty();
	Terminal.QueryAnyKey("Press any key to continue.");
}
