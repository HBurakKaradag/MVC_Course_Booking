using System.Web;

namespace BookingSystem.Domain.WebUI.Hotel
{
    public class HotelRoomVM : IModel
    {

        public HotelRoomVM()
        {
            this.Price = 0m;

        }
        public int Id { get; set; }

        public int HotelId { get; set; }

        public string HotelName { get; set; }

        public int RoomTypeId { get; set; }

        public string RoomName { get; set; }

        public int RoomCapacity { get; set; }

        public string ImageUrl { get; set; }

        public HttpPostedFileBase RoomImage { get; set; }

        public decimal Price { get; set; }
        public bool? IsActive { get; set; }

    }
}