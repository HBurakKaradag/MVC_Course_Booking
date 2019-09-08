using BookingSystem.Domain.Entity.BaseEntity;

namespace BookingSystem.Domain.Entity
{
    public class Menu : EntityBase
    {
        public Menu()
        {
            this.IsActive = true;
        }

        public int ParentId { get; set; }
        public string Title { get; set; }
        public int Order { get; set; }
        public string IconClass { get; set; }
        public string Url { get; set; }
        public bool IsActive { get; set; }
    }
}