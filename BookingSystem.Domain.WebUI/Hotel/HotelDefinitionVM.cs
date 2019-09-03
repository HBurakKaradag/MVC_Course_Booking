using BookingSystem.Core.Extensions;

namespace BookingSystem.Domain.WebUI.Hotel
{
    public class HotelDefinitionVM : IModel
    {
        public int Id { get; set; }

        public string HotelName { get; set; }
        public string Title { get; set; }

        public string Url
        {
            get
            {
                return HotelName.Titilize();
            }
        }

        public int HotelTypeId { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }
    }
}