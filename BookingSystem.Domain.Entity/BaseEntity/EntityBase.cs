using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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