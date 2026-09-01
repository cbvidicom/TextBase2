/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using System.Diagnostics.CodeAnalysis;
using Uwn.Common.Text;
using Textbase.Integration.Api.Rest;

namespace Textbase.Integration.Api.Rest;

public sealed partial class RestClients
{
	public sealed record TextbaseClients(
		ClientApplicationsClient ClientApplicationsClient,
		ClientApplicationLocalesClient ClientApplicationLocalesClient,
		ClientApplicationTextResourcesClient ClientApplicationTextResourcesClient,
		FormalitiesClient FormalitiesClient,
		LocalesClient LocalesClient,
		PresentationsClient PresentationsClient,
		TextResourcesClient TextResourcesClient,
		TranslationsClient TranslationsClient,
		FlatTranslationsClient FlatTranslationsClient
		);

	public TextbaseClients Textbase { get; private set; }

	[InitializeFor]
	[MemberNotNull(nameof(Textbase))]
	private void InitializeForTextbase(
		string baseUrl,
		string? bearerToken = null,
		HttpClient? httpClient = null)
	{
		string specificBaseUrl = baseUrl.CombineUrl("api");

		Textbase = new(
			new(specificBaseUrl, bearerToken, httpClient),
			new(specificBaseUrl, bearerToken, httpClient),
			new(specificBaseUrl, bearerToken, httpClient),
			new(specificBaseUrl, bearerToken, httpClient),
			new(specificBaseUrl, bearerToken, httpClient),
			new(specificBaseUrl, bearerToken, httpClient),
			new(specificBaseUrl, bearerToken, httpClient),
			new(specificBaseUrl, bearerToken, httpClient),
			new(specificBaseUrl, bearerToken, httpClient)
		);
	}

	[SetBearerTokenFor]
	private void SetBearerTokenForTextbase(
		string? value)
	{
		Textbase.ClientApplicationsClient.BearerToken = value;
		Textbase.ClientApplicationLocalesClient.BearerToken = value;
		Textbase.ClientApplicationTextResourcesClient.BearerToken = value;
		Textbase.FormalitiesClient.BearerToken = value;
		Textbase.LocalesClient.BearerToken = value;
		Textbase.PresentationsClient.BearerToken = value;
		Textbase.TextResourcesClient.BearerToken = value;
		Textbase.TranslationsClient.BearerToken = value;
		Textbase.FlatTranslationsClient.BearerToken = value;
	}

	[SetHttpClientFor]
	private void SetHttpClientForTextbase(
		HttpClient value)
	{
		Textbase.ClientApplicationsClient.HttpClient = value;
		Textbase.ClientApplicationLocalesClient.HttpClient = value;
		Textbase.ClientApplicationTextResourcesClient.HttpClient = value;
		Textbase.FormalitiesClient.HttpClient = value;
		Textbase.LocalesClient.HttpClient = value;
		Textbase.PresentationsClient.HttpClient = value;
		Textbase.TextResourcesClient.HttpClient = value;
		Textbase.TranslationsClient.HttpClient = value;
		Textbase.FlatTranslationsClient.HttpClient = value;
	}

	[SetTimeoutFor]
	private void SetTimeoutForTextbase(
		TimeSpan value)
	{
		Textbase.ClientApplicationsClient.Timeout = value;
		Textbase.ClientApplicationLocalesClient.Timeout = value;
		Textbase.ClientApplicationTextResourcesClient.Timeout = value;
		Textbase.FormalitiesClient.Timeout = value;
		Textbase.LocalesClient.Timeout = value;
		Textbase.PresentationsClient.Timeout = value;
		Textbase.TextResourcesClient.Timeout = value;
		Textbase.TranslationsClient.Timeout = value;
		Textbase.FlatTranslationsClient.Timeout = value;
	}

	[SetInitialRetryDelayFor]
	private void SetInitialRetryDelayForTextbase(
		TimeSpan value)
	{
		Textbase.ClientApplicationsClient.InitialRetryDelay = value;
		Textbase.ClientApplicationLocalesClient.InitialRetryDelay = value;
		Textbase.ClientApplicationTextResourcesClient.InitialRetryDelay = value;
		Textbase.FormalitiesClient.InitialRetryDelay = value;
		Textbase.LocalesClient.InitialRetryDelay = value;
		Textbase.PresentationsClient.InitialRetryDelay = value;
		Textbase.TextResourcesClient.InitialRetryDelay = value;
		Textbase.TranslationsClient.InitialRetryDelay = value;
		Textbase.FlatTranslationsClient.InitialRetryDelay = value;
	}

	[SetMaxRetryDelayFor]
	private void SetMaxRetryDelayForTextbase(
		TimeSpan value)
	{
		Textbase.ClientApplicationsClient.MaxRetryDelay = value;
		Textbase.ClientApplicationLocalesClient.MaxRetryDelay = value;
		Textbase.ClientApplicationTextResourcesClient.MaxRetryDelay = value;
		Textbase.FormalitiesClient.MaxRetryDelay = value;
		Textbase.LocalesClient.MaxRetryDelay = value;
		Textbase.PresentationsClient.MaxRetryDelay = value;
		Textbase.TextResourcesClient.MaxRetryDelay = value;
		Textbase.TranslationsClient.MaxRetryDelay = value;
		Textbase.FlatTranslationsClient.MaxRetryDelay = value;
	}

	[SetMaxRetriesFor]
	private void SetMaxRetriesForTextbase(
		int value)
	{
		Textbase.ClientApplicationsClient.MaxRetries = value;
		Textbase.ClientApplicationLocalesClient.MaxRetries = value;
		Textbase.ClientApplicationTextResourcesClient.MaxRetries = value;
		Textbase.FormalitiesClient.MaxRetries = value;
		Textbase.LocalesClient.MaxRetries = value;
		Textbase.PresentationsClient.MaxRetries = value;
		Textbase.TextResourcesClient.MaxRetries = value;
		Textbase.TranslationsClient.MaxRetries = value;
		Textbase.FlatTranslationsClient.MaxRetries = value;
	}

	[AddCustomRequestHeaderTo]
	private void AddCustomRequestHeaderToTextbase(
		string key,
		string value)
	{
		Textbase.ClientApplicationsClient.CustomRequestHeaders[key] = value;
		Textbase.ClientApplicationLocalesClient.CustomRequestHeaders[key] = value;
		Textbase.ClientApplicationTextResourcesClient.CustomRequestHeaders[key] = value;
		Textbase.FormalitiesClient.CustomRequestHeaders[key] = value;
		Textbase.LocalesClient.CustomRequestHeaders[key] = value;
		Textbase.PresentationsClient.CustomRequestHeaders[key] = value;
		Textbase.TextResourcesClient.CustomRequestHeaders[key] = value;
		Textbase.TranslationsClient.CustomRequestHeaders[key] = value;
		Textbase.FlatTranslationsClient.CustomRequestHeaders[key] = value;
	}

	[RemoveCustomRequestHeaderFrom]
	private void RemoveCustomRequestHeaderFromTextbase(
		string key)
	{
		Textbase.ClientApplicationsClient.CustomRequestHeaders.Remove(key);
		Textbase.ClientApplicationLocalesClient.CustomRequestHeaders.Remove(key);
		Textbase.ClientApplicationTextResourcesClient.CustomRequestHeaders.Remove(key);
		Textbase.FormalitiesClient.CustomRequestHeaders.Remove(key);
		Textbase.LocalesClient.CustomRequestHeaders.Remove(key);
		Textbase.PresentationsClient.CustomRequestHeaders.Remove(key);
		Textbase.TextResourcesClient.CustomRequestHeaders.Remove(key);
		Textbase.TranslationsClient.CustomRequestHeaders.Remove(key);
		Textbase.FlatTranslationsClient.CustomRequestHeaders.Remove(key);
	}
}
