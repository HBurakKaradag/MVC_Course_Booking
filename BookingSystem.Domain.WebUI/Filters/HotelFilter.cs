using System.ComponentModel;

namespace BookingSystem.Domain.WebUI.Filters
{
    public class HotelFilter
    {
        [DisplayName("Hotel Name")]
        public string HotelName { get; set; }

        [DisplayName("Hotel Type")]
        public int HotelTypeId { get; set; }
    }
}