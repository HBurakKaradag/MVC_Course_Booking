using BookingSystem.Core.Extensions;

namespace BookingSystem.Domain.WebUI.Hotel
{
    public class HotelDefinitionVM : IModel
    {
        public int Id { get; set; }

        public string HotelName { get; set; }
        public string Title { get; set; }

        public string Url { get; set; }
        //{
        //    get
        //    {
        //        return HotelName.Titilize();
        //    }
        //}

        public int HoteTypeId { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }
    }
}