using System.ComponentModel.DataAnnotations;

namespace BookingSystem.Domain.WebUI.Hotel
{
    public class HotelTypeVM : IModel
    {
        public HotelTypeVM()
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
    }
}