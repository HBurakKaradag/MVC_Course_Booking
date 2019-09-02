using BookingSystem.Core.Extensions;

namespace BookingSystem.Domain.WebUI.Hotel
{
    public class HotelDefinitionVM : IModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Title { get; set; }

        public string Url
        {
            get
            {
                return Name.Titilize();
            }
        }

        public int HoteTypeId { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }
    }
}