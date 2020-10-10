namespace BookShop.Common.Utils
{
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Caching.Distributed;

    public static class DistributedCacheExtensions
    {
        public static async Task SetAsync<T>(this IDistributedCache distributedCache, string key, T value, DistributedCacheEntryOptions options, CancellationToken token = default)
        {
            await distributedCache.SetAsync(key, value.ToByteArray(), options, token).ConfigureAwait(false);
        }

        public static async Task<T> GetAsync<T>(this IDistributedCache distributedCache, string key, CancellationToken token = default)
            where T : class
        {
            var result = await distributedCache.GetAsync(key, token).ConfigureAwait(false);
            return result.FromByteArray<T>();
        }
    }
}
