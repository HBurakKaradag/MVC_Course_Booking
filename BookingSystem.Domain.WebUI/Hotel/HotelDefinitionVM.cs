using BookingSystem.Core.CustomAttribute;
using BookingSystem.Core.Extensions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookingSystem.Domain.WebUI.Hotel
{
    public class HotelDefinitionVM : IModel
    {
        public HotelDefinitionVM()
        {
            HotelAttributes = new List<HotelAttributeVM>();
            HotelRooms = new List<HotelRoomVM>();
        }

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

        public int CityId { get; set; }

        public int DistrictId { get; set; }

        public string Address { get; set; }

        public List<HotelAttributeVM> HotelAttributes { get; set; }

        public List<HotelRoomVM> HotelRooms { get; set; }
        
        /// <summary>
        /// View model için Entityden farklı propery'ler içerebileceğini ve view 'a göre düzenlenebileceğinden bahsetmiştik.
        /// HotelTypes datasını ViewBag üzerinden gönderebileceğimiz gibi VM içerisinden de gönderebiliriz.
        /// </summary>
        [MapIgnore]
        public IEnumerable<HotelTypeVM> HotelTypes { get; set; }

        [MapIgnore]
        public IEnumerable<BSelectListItem> Cities { get; set; }

        [MapIgnore]
        public IEnumerable<BSelectListItem> Districts { get; set; }

        [MapIgnore]
        public List<CheckBoxListTemplate> Attributes { get; set; }
    }
}