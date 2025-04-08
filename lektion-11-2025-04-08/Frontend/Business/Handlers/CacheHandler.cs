using Microsoft.Extensions.Caching.Memory;

namespace Business.Handlers;

public interface ICacheHandler<T>
{
    T? GetFromCache(string cacheKey);
    T SetCache(string cacheKey, T data, int minutesToCache = 10);
}

public class CacheHandler<T>(IMemoryCache cache) : ICacheHandler<T>
{
    private readonly IMemoryCache _cache = cache;

    public T? GetFromCache(string cacheKey)
    {
        if (_cache.TryGetValue(cacheKey, out T? cachedData))
            return cachedData;

        return default;
    }

    public T SetCache(string cacheKey, T data, int minutesToCache = 10)
    {
        _cache.Remove(cacheKey);
        _cache.Set(cacheKey, data, TimeSpan.FromMinutes(minutesToCache));
        return data;
    }

    public void RemoveCache(string cacheKey)
    {
        _cache.Remove(cacheKey);
    }

}