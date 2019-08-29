using System.ComponentModel;

namespace BookingSystem.Domain.WebUI.Filters
{
    public class RoomTypeFilter
    {
        [DisplayName("Title Info")]
        public string Title { get; set; }
    }
}