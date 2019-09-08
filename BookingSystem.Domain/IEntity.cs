using System;
using System.ComponentModel.DataAnnotations;

namespace BookingSystem.Domain
{
    /// <summary>
    /// @Kodluyoruz-MVC-Bootcamp  13.07.2019 – 09.09.2019
    /// H.Burak Karadağ
    /// DB Katmanı için kullandığımız Entity'lerin  interface sınıfı
    /// Her tablomuzda Id veCreateData olacağını garanti ediyoruz
    /// </summary>
    public interface IEntity : IDomain
    {
        [Key]
        int Id { get; set; }

        DateTime CreateDate { get; set; }
    }
}