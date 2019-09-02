using BookingSystem.Domain.Entity.BaseEntity;

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

        public int HoteTypeId { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }
    }
}