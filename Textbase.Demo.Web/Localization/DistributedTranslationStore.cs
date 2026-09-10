using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using Textbase.Integration.Localization;
using CM = Textbase.Contracts.Models;

namespace Textbase.Demo.Web.Localization;

public sealed class DistributedTranslationStore(
	IDistributedCache cache)
	: ITranslationStore
{
	private const string CacheKey = "Textbase.Demo.Web:RuntimeLocalizationSnapshot";

	public async ValueTask<CM.RuntimeLocalizationSnapshotDto?> GetAsync(
		CancellationToken cancellationToken = default)
	{
		byte[]? data = await cache.GetAsync(CacheKey, cancellationToken);
		return data is null ? null : JsonSerializer.Deserialize<CM.RuntimeLocalizationSnapshotDto>(data);
	}

	public async ValueTask SetAsync(
		CM.RuntimeLocalizationSnapshotDto snapshot,
		CancellationToken cancellationToken = default)
	{
		ArgumentNullException.ThrowIfNull(snapshot);

		byte[] data = JsonSerializer.SerializeToUtf8Bytes(snapshot);
		DistributedCacheEntryOptions options = new()
		{
			AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1)
		};

		await cache.SetAsync(CacheKey, data, options, cancellationToken);
	}
}
