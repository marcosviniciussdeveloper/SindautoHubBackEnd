using System;
using Microsoft.Extensions.Caching.Distributed;
using SindautoHub.Application.Interface;

public class RedisCacheService : ICacheService
{
    private readonly IDistributedCache _cache;
    private readonly DistributedCacheEntryOptions _options;

    public RedisCacheService(IDistributedCache cache)
    {
        _cache = cache;
        _options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(3600),
            SlidingExpiration = TimeSpan.FromSeconds(1600)
        };
    }



    public async Task<string> GetAsync(string key)
    {
       return await _cache.GetStringAsync(key);
    }

    public async Task RemoveAsync(string key)
    {
         await  _cache.RemoveAsync(key);
    }

    public async Task SetAsync(string key, string value)
    {
        await _cache.SetStringAsync(key, value , _options);
    }

    public Task SetAsync(string key, string value, DistributedCacheEntryOptions options, CancellationToken ct = default)
    
       => _cache.SetStringAsync(key, value, options ?? _options, ct);
    }


