
using Microsoft.Extensions.Caching.Distributed;

namespace SindautoHub.Application.Interface;
public interface ICacheService 
{
    Task SetAsync (string key, string value);

    Task<string> GetAsync (string key);
    
    Task RemoveAsync (string key);

     Task SetAsync(string key, string value, DistributedCacheEntryOptions options, CancellationToken ct = default);
}