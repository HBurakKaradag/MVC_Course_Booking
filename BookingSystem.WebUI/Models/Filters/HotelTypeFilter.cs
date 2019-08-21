using System.ComponentModel;

namespace BookingSystem.WebUI.Models.Filters
{
    public class HotelTypeFilter
    {
        [DisplayName("Title Info")]
        public string Title { get; set; }

        [DisplayName("Active Records")]
        public bool IsActive { get; set; }
    }
}