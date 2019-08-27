namespace BookingSystem.Domain.WebUI.Hotel
{
    public class HotelRoomTypeVM : IModel
    {
        public int Id { get; set; }

        public int HotelId { get; set; }

        public string Name { get; set; }

        public int MaxCapacity { get; set; }

        public string ShowCaseImage { get; set; }

        public bool IsActive { get; set; }
    }
}