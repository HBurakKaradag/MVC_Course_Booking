using BookingSystem.Core;
using BookingSystem.Domain.Entity.BaseEntity;

namespace BookingSystem.Domain.Entity
{
    public class Attributes : EntityBase
    {
        public Attributes()
        {
            this.IsActive = true;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public AttributeType AttributeType { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
    }
}