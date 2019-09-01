using BookingSystem.Core.CustomException;
using BookingSystem.Domain.Entity;
using System;
using System.Data.Entity;

namespace BookingSystem.Data.Context
{
    public class EFBookingContext : DbContext
    {
        public EFBookingContext()
            : base("BookingContext")
        {
        }

        public override int SaveChanges()
        {
            var result = 0;
            try
            {
                result = base.SaveChanges();
            }
            catch (Exception ex)
            {
                // özelleştir
                throw new DatabaseException("DataBaseExcetion", ex);
            }
            return result;
        }

        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<Menu> Menus { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<RoomType> RoomTypes { get; set; }

        public DbSet<HotelType> HotelTypes { get; set; }

        public DbSet<HotelRoomTypes> HotelRoomTypes { get; set; }

        public DbSet<Attributes> Attributes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}