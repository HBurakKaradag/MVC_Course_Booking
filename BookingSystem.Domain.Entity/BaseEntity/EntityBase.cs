using System;

namespace BookingSystem.Domain.Entity.BaseEntity
{
    public abstract class EntityBase : IEntity
    {
        public EntityBase()
        {
            this.CreateDate = DateTime.Now;
        }

        public int Id { get; set; }

        public DateTime CreateDate { get; set; }
    }
}