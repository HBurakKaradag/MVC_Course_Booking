using System;

namespace BookingSystem.Core.CustomAttribute
{
    /// <summary>
    /// @Kodluyoruz-MVC-Bootcamp  13.07.2019 – 09.09.2019
    /// H.Burak Karadağ
    /// EntityModel ve ViewModel üzerinde yapılan Mapping işleminde
    /// Ignore edilecek property'lerin belirlenmesi için kullanılır.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class MapIgnoreAttribute : Attribute
    {
    }
}