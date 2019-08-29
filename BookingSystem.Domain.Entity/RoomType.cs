using BookingSystem.Domain.Entity.BaseEntity;

namespace BookingSystem.Domain.Entity
{
    public class RoomType : EntityBase
    {
        public RoomType()
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