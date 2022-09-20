using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonEntities.Helpers
{
    public class CacheHelper
    {
        private readonly IMemoryCache _memoryCache;
        private MemoryCacheEntryOptions cacheExpiryOptions = new MemoryCacheEntryOptions
        {
            AbsoluteExpiration = DateTime.Now.AddSeconds(50),
            Priority = CacheItemPriority.High,
            SlidingExpiration = TimeSpan.FromSeconds(20)
        };
        public CacheHelper(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

      
        public T GetData<T>(string key)
        {
            try
            {
                T item = (T)_memoryCache.Get(key);
                return item;
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public bool SetData<T>(string key, T value, DateTimeOffset expirationTime)
        {
            bool res = true;
            try
            {
                if (!string.IsNullOrEmpty(key))
                {
                    _memoryCache.Set(key, value, expirationTime);
                }
            }
            catch (Exception e)
            {
                throw;
            }
            return res;
        }
        public bool RemoveData(string key)
        {
            try
            {
                if (!string.IsNullOrEmpty(key))
                {
                     _memoryCache.Remove(key);
                }
            }
            catch (Exception e)
            {
                throw;
            }
            return false;
        }
    }
}
