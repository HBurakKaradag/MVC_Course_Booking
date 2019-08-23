using BookingSystem.Domain.Entity;
using System.Data.Entity;

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

        public DbSet<HotelType> HotelTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}