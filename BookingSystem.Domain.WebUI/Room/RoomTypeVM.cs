using System;
using System.ComponentModel.DataAnnotations;

namespace BookingSystem.Domain.WebUI.Room
{
    public class RoomTypeVM : IModel
    {
        public RoomTypeVM()
        {
            this.IsDeleted = false;
            this.IsActive = true;
        }

        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateDate { get; set; }
    }
}