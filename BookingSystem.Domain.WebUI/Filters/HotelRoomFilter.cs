using System.ComponentModel;

namespace BookingSystem.Domain.WebUI.Filters
{
    public class HotelRoomFilter
    {
        [DisplayName("Hotel Info")]
        public int HotelId { get; set; }
    }
}