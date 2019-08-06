using BookingSystem.Domain.Entity.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Domain.Entity
{
    public class User : EntityBase
    {
        public User()
        {
            this.IsActive = true;
            this.Deleted = false;
            this.EmailConfirmation = false;
        }

        public string Name { get; set; }
        public string LastName { get; set; }
        public string EMail { get; set; }
        public string Avatar { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public bool Deleted { get; set; }
        public bool EmailConfirmation { get; set; }
        public string TokenId { get; set; }
    }
}