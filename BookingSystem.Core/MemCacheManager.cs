using BookingSystem.Core.Interfaces;
using System;
using System.Runtime.Caching;

namespace BookingSystem.Core
{
    public class MemCacheManager : ICacheManager
    {
        public MemCacheManager()
        {
        }

        protected ObjectCache Cache => MemoryCache.Default;

        public T Get<T>(string key)
        {
            return (T)Cache[key];
        }

        public T GetFromCache<T>(string key, Func<T> callBackFunc, DateTimeOffset? expTime = null)
        {
            var data = (T)Cache[key];

            if (data == null)
            {
                var policy = new CacheItemPolicy
                {
                    AbsoluteExpiration = expTime.HasValue ? expTime.Value : DateTime.Now.AddMinutes(30)
                };

                data = callBackFunc();

                Cache.Set(key, data, policy);
            }

            return data;
        }

        public void Set(string key, object data, DateTimeOffset? expTime = null)
        {
            if (data == null)
            {
                return;
            }

            var policy = new CacheItemPolicy
            {
                AbsoluteExpiration = expTime ?? DateTime.Now.AddMinutes(30)
            };
            Cache.Add(new CacheItem(key, data), policy);
        }

        public void Remove(string key)
        {
            Cache.Remove(key);
        }
    }
}