
namespace SindautoHub.Application.Interface;
public interface ICacheService
{
    Task SetAsync (string key, string value);

    Task<string> GetAsync (string key);
    
    Task RemoveAsync (string key);
}