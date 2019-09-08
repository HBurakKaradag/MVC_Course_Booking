using System;

namespace BookingSystem.Core.Interfaces
{
    /// <summary>
    /// @Kodluyoruz-MVC-Bootcamp  13.07.2019 – 09.09.2019
    /// H.Burak Karadağ
    /// CacheManager interface implementasyonu
    /// ************************
    /// Get             > Cache üzerinden Key ile değer' getirir. Cachke üzerinde belirtilen key değeri yoksa null döndürür.
    /// Set             > Cache üzerine belirtilen key ile parametrede gönderilen data'yı ekler
    /// GetFromCache    > Cache üzerinde Key değeri yoksa callBackFunc oalrak gönderilen methodu çalıştırıp dataya erişir.
    ///                     Datayı geri dönmeden Memory üzerine set eder.
    /// ************************
    /// </summary>
    public interface ICacheManager
    {
        T Get<T>(string key);

        T GetFromCache<T>(string key, Func<T> callBackFunc, DateTimeOffset? expTime = null);

        void Set(string key, object data, DateTimeOffset? expTime);

        void Remove(string key);
    }
}