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
            this.Configuration.LazyLoadingEnabled = true;
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

        public DbSet<HotelDefinition> HotelDefinitions { get; set; }

        public DbSet<HotelRoomTypes> HotelRoomTypes { get; set; }

        public DbSet<Attributes> Attributes { get; set; }

        public DbSet<CityDefinition> CityDefinitions { get; set; }

        public DbSet<DistrictDefinition> DistrictDefinition { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // configures one-to-many relationship
            modelBuilder.Entity<DistrictDefinition>()
                .HasRequired<CityDefinition>(s => s.CityDefinition)
                .WithMany(g => g.Districts)
                .HasForeignKey<int>(s => s.CityId);

            base.OnModelCreating(modelBuilder);
        }
    }
}