namespace Textbase.Cli;

internal readonly record struct LocaleSynchronizationResult(
	int Added,
	int Updated,
	int Unchanged);
