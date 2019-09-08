using System.Web;

namespace BookingSystem.Domain.WebUI.Hotel
{
    public class HotelTestFileVM
    {
        public string HotelName { get; set; }
        public HttpPostedFileBase HotelImage { get; set; }
    }
}