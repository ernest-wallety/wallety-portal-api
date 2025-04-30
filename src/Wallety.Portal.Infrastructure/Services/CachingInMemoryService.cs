using Microsoft.Extensions.Caching.Memory;
using Wallety.Portal.Core.Services;

namespace Wallety.Portal.Infrastructure.Services
{
    public class CachingInMemoryService(IMemoryCache memoryCache) : ICachingInMemoryService
    {
        private readonly IMemoryCache _memoryCache = memoryCache;

        public T Get<T>(string key)
        {
            return _memoryCache.Get<T>(key);
        }

        public void Set<T>(string key, T value, TimeSpan? expirationTime = null)
        {
            if (expirationTime == null)
                _memoryCache.Set(key, value);
            else
                _memoryCache.Set(key, value, expirationTime.Value);
        }

        public void Remove(string key)
        {
            _memoryCache.Remove(key);
        }

        public void Clear()
        {
            _memoryCache.Dispose();
        }
    }
}
