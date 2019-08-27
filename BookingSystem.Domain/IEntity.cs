using System;
using System.ComponentModel.DataAnnotations;

namespace BookingSystem.Domain
{
    public interface IEntity
    {
        [Key]
        int Id { get; set; }

        DateTime CreateDate { get; set; }
    }
}