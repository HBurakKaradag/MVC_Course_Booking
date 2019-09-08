using System;

namespace BookingSystem.Core.Interfaces
{
    public interface ICacheManager
    {
        T Get<T>(string key);

        T GetFromCache<T>(string key, Func<T> callBackFunc, DateTimeOffset? expTime = null);

        void Set(string key, object data, DateTimeOffset? expTime);

        void Remove(string key);
    }
}