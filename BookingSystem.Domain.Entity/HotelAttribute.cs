using BookingSystem.Domain.Entity.BaseEntity;

namespace BookingSystem.Domain.Entity
{
    public class HotelAttribute : EntityBase
    {
        public HotelAttribute()
        {
            this.IsActive = true;
            this.IsDeleted = false;
        }

        public int HotelId { get; set; }

        public int AttributeId { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public virtual HotelDefinition HotelDefinition { get; set; }
    }
}