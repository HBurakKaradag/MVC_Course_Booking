using BookingSystem.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Data.Context
{
    public class EFBookingContext : DbContext
    {
        public EFBookingContext()
            : base("BookingContext")
        {
        }

        public DbSet<Menu> Menus { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}