using Microsoft.Extensions.Caching.Memory;
using System;

namespace Jyz.Infrastructure
{
    /// <summary>
    /// 内存缓存
    /// </summary>
    public class MemoryCache : ICache
    {
        private readonly IMemoryCache _cache;
        public MemoryCache(IMemoryCache cache)
        {
            _cache = cache;
        }
        public bool Exists(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            return _cache.Get(key) != null;
        }
        public string Get(string key)
        {
            return _cache.Get(key)?.ToString();
        }

        public T Get<T>(string key) where T : class
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            return _cache.Get(key) as T;
        }
        public void Add(string key, object value, TimeSpan? expiresIn = null, bool isSliding = false)
        {
            if (expiresIn != null)
            {
                _cache.Set(key, value,
             new MemoryCacheEntryOptions()
             .SetSlidingExpiration((TimeSpan)expiresIn));
            }
            else
            {
                _cache.Set(key, value);
            }
        }

        public void Remove(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            _cache.Remove(key);
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            if (_cache != null)
                _cache.Dispose();
            GC.SuppressFinalize(this);
        }
        
    }
}
