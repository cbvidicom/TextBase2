/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using System.Reflection;

namespace Textbase.Integration.Api.Rest;

[AttributeUsage(AttributeTargets.Method)] internal sealed class SetBearerTokenForAttribute : Attribute { }
[AttributeUsage(AttributeTargets.Method)] internal sealed class SetHttpClientForAttribute : Attribute { }
[AttributeUsage(AttributeTargets.Method)] internal sealed class SetTimeoutForAttribute : Attribute { }
[AttributeUsage(AttributeTargets.Method)] internal sealed class SetInitialRetryDelayForAttribute : Attribute { }
[AttributeUsage(AttributeTargets.Method)] internal sealed class SetMaxRetryDelayForAttribute : Attribute { }
[AttributeUsage(AttributeTargets.Method)] internal sealed class SetMaxRetriesForAttribute : Attribute { }
[AttributeUsage(AttributeTargets.Method)] internal sealed class InitializeForAttribute : Attribute { }
[AttributeUsage(AttributeTargets.Method)] internal sealed class AddCustomRequestHeaderToAttribute : Attribute { }
[AttributeUsage(AttributeTargets.Method)] internal sealed class RemoveCustomRequestHeaderFromAttribute : Attribute { }

public sealed partial class RestClients
{
	private string? _bearerToken;
	private HttpClient? _httpClient;
	private readonly Dictionary<string, string> _customRequestHeaders;
	private TimeSpan _timeout;
	private TimeSpan _initialRetryDelay;
	private TimeSpan _maxRetryDelay;
	private int _maxRetries;

	//

	public string? BearerToken
	{
		get => _bearerToken;
		set
		{
			_bearerToken = value;

			CallAttributedMethods<SetBearerTokenForAttribute>(value);
		}
	}

	public HttpClient? HttpClient
	{
		get => _httpClient;
		set
		{
			_httpClient = value;

			if (value is null)
				return;

			CallAttributedMethods<SetHttpClientForAttribute>(value);
		}
	}

	public TimeSpan Timeout
	{
		get => _timeout;
		set
		{
			_timeout = value;

			CallAttributedMethods<SetTimeoutForAttribute>(value);
		}
	}

	public TimeSpan InitialRetryDelay
	{
		get => _initialRetryDelay;
		set
		{
			_initialRetryDelay = value;

			CallAttributedMethods<SetInitialRetryDelayForAttribute>(value);
		}
	}

	public TimeSpan MaxRetryDelay
	{
		get => _maxRetryDelay;
		set
		{
			_maxRetryDelay = value;

			CallAttributedMethods<SetMaxRetryDelayForAttribute>(value);
		}
	}

	public int MaxRetries
	{
		get => _maxRetries;
		set
		{
			_maxRetries = value;

			CallAttributedMethods<SetMaxRetriesForAttribute>(value);
		}
	}

	//

#pragma warning disable CS8618
	public RestClients(
		string baseUrl,
		string? bearerToken = null,
		HttpClient? httpClient = null)
	{
		_bearerToken = bearerToken;
		_httpClient = httpClient;
		_customRequestHeaders = [];
		_timeout = TimeSpan.FromSeconds(30);
		_initialRetryDelay = TimeSpan.FromMilliseconds(200);
		_maxRetryDelay = TimeSpan.FromSeconds(2);
		_maxRetries = 3;

		CallAttributedMethods<InitializeForAttribute>(baseUrl, bearerToken, httpClient);
	}
#pragma warning restore CS8618

	//

	public bool TryGetCustomRequestHeader(
		string key,
		out string? value)
		=> _customRequestHeaders.TryGetValue(key, out value);

	public void AddCustomRequestHeader(
		string key,
		string value)
	{
		_customRequestHeaders[key] = value;

		CallAttributedMethods<AddCustomRequestHeaderToAttribute>(key, value);
	}

	public bool RemoveCustomRequestHeader(
		string key)
	{
		CallAttributedMethods<RemoveCustomRequestHeaderFromAttribute>(key);

		return _customRequestHeaders.Remove(key);
	}

	//

	private void CallAttributedMethods<TAttribute>(
		params object?[]? parameters)
		where TAttribute : Attribute
	{
		MethodInfo[] methods = GetType().GetMethods(BindingFlags.Instance | BindingFlags.NonPublic);

		foreach (MethodInfo method in methods)
		{
			if (!method.IsDefined(typeof(TAttribute), false))
				continue;

			if (method.ReturnType != typeof(void))
				continue;

			if (parameters is null)
			{
				if (method.GetParameters().Length != 0)
					continue;

				method.Invoke(this, null);
			}
			else
			{
				if (method.GetParameters().Length != parameters.Length)
					continue;

				method.Invoke(this, parameters);
			}
		}
	}
}