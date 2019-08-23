using System.ComponentModel;

namespace BookingSystem.Domain.WebUI.Filters
{
    public class HotelTypeFilter
    {
        [DisplayName("Title Info")]
        public string Title { get; set; }

        [DisplayName("Active Records")]
        public bool? IsActive { get; set; }
    }
}