using System;

namespace BookingSystem.Core.CustomAttribute
{
    /// <summary>
    /// @Kodluyoruz-MVC-Bootcamp  13.07.2019 – 09.09.2019
    /// H.Burak Karadağ
    /// Spesifik method'lar üzerinde AuditLog'un Ignore edilmesini sağlayan Attribute
    /// ActionFilter üzerinde methodun AuditLog'a düşmemesi için method bu attribute ile işaretlenir.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class AuditLogIgnore : Attribute
    {
    }
}