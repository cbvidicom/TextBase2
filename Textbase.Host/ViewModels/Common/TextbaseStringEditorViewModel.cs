using Uwn.Blazor.Models.ViewModels.Abstractions.Querying;
using Uwn.Common.Abstractions;
using Uwn.Common.Factories;
using Uwn.EntityFrameworkCore.Infrastructure;

namespace Textbase.Host.ViewModels.Common;

public abstract class TextbaseStringEditorViewModel<TDTO, TModel>(
	IModelQueries1<TModel, string> _modelQueries,
	IModelCommands<TDTO, TModel> modelCommands,
	IModelCommands1<string> _modelCommands1)
	: EditorViewModel<TDTO, TModel>(modelCommands), IAsyncInitializable<string?>
	where TDTO : class
	where TModel : class, TDTO
{
	protected abstract string KeyPropertyName { get; }

	public string ItemKey => GetItemKey();

	public Task InitializeAsync(
		string? arg,
		CancellationToken cancellationToken = default)
		=> InitializeCoreAsync(arg, cancellationToken);

	protected override async Task<TModel> ReadItemAsync(
		object arg,
		CancellationToken cancellationToken = default)
	{
		if (arg is not string key)
			throw Exceptions.TypeMismatch(arg.GetType(), typeof(string));

		return await _modelQueries.ReadAsync(key, cancellationToken) ?? throw Exceptions.ItemNotFound(typeof(TModel).Name, arg);
	}

	protected override async Task DeleteItemAsync(
		CancellationToken cancellationToken = default)
	{
		string key = GetItemKey();
		if (!await _modelCommands1.TryDeleteAsync(key, cancellationToken))
			throw Exceptions.ItemNotDeleted(typeof(TModel).Name, key);
	}

	private string GetItemKey()
	{
		object value = Item.GetType().GetProperty(KeyPropertyName)?.GetValue(Item) ?? throw Exceptions.PropertyNotFound(KeyPropertyName);
		if (value is string key)
			return key;

		throw Exceptions.TypeMismatch(value.GetType(), typeof(string));
	}
}
