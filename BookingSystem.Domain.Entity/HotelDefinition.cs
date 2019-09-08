using BookingSystem.Domain.Entity.BaseEntity;
using System.Collections;
using System.Collections.Generic;

namespace BookingSystem.Domain.Entity
{
    public class HotelDefinition : EntityBase
    {
        public HotelDefinition()
        {
            this.IsActive = true;
            this.IsDeleted = false;
        }

        public string HotelName { get; set; }
        public string Title { get; set; }

        public string Url { get; set; }

        public int HotelTypeId { get; set; }

        public int CityId { get; set; }

        public int DistrictId { get; set; }

        public string Address { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<HotelAttribute> HotelAttributes { get; set; }

        public virtual ICollection<HotelRoom> HotelRooms { get; set; }
    }
}