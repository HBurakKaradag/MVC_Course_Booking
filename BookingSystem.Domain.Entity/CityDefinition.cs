using BookingSystem.Core;
using BookingSystem.Domain.Entity.BaseEntity;
using System.Collections;
using System.Collections.Generic;

namespace BookingSystem.Domain.Entity
{
    public class CityDefinition : EntityBase
    {
        public CityDefinition()
        {
            this.IsActive = true;
        }

        public string Name { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }

        public virtual ICollection<DistrictDefinition> Districts { get; set; }
    }
}