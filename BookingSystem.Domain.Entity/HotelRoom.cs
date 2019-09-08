using BookingSystem.Domain.Entity.BaseEntity;

namespace BookingSystem.Domain.Entity
{
    public class HotelRoom: EntityBase
    {
        public HotelRoom()
        {
            this.IsActive = true;
            this.IsDeleted = false;
            this.Price = 0m;
        }

        public int HotelId { get; set; }

        public int RoomTypeId { get; set; }

        public int MaxCapacity { get; set; }

        public string ImageUrl { get; set; }

        public decimal Price { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public virtual HotelDefinition Hotel { get; set; }
    }
}