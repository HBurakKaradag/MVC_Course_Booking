using BookingSystem.Domain.Entity.BaseEntity;

namespace BookingSystem.Domain.Entity
{
    public class HotelRoomTypes : EntityBase
    {
        public HotelRoomTypes()
        {
            this.IsActive = true;
            this.IsDeleted = false;
        }

        public int HotelId { get; set; }

        public string Name { get; set; }

        public int MaxCapacity { get; set; }

        public string ShowCaseImage { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }
    }
}