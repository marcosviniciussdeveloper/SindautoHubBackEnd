// SindautoHub.Infrastructure/Services/RedisCacheService.cs
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using SindautoHub.Application.Interface;

namespace SindautoHub.Infrastructure.Services
{
    public class RedisCacheService : ICacheService
    {
        private readonly IDistributedCache _cache;
        private readonly DistributedCacheEntryOptions _options =
            new()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(3600),
                SlidingExpiration = TimeSpan.FromSeconds(1600)
            };

        public RedisCacheService(IDistributedCache cache) => _cache = cache;

        public Task<string> GetAsync(string key) =>
            _cache.GetStringAsync(key);

        public Task RemoveAsync(string key) =>
            _cache.RemoveAsync(key);

        public Task SetAsync(string key, string value) =>
            _cache.SetStringAsync(key, value, _options);

        public Task SetAsync(string key, string value,
            DistributedCacheEntryOptions options, CancellationToken ct = default) =>
            _cache.SetStringAsync(key, value, options ?? _options, ct);
    }
}
