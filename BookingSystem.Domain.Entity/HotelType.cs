using BookingSystem.Domain.Entity.BaseEntity;

namespace BookingSystem.Domain.Entity
{
    public class HotelType : EntityBase
    {
        public HotelType()
        {
            this.IsActive = true;
            this.IsDeleted = false;
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
    }
}