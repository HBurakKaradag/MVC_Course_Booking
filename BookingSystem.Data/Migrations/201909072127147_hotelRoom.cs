namespace BookingSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hotelRoom : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.HotelRoomTypes", newName: "HotelRooms");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.HotelRooms", newName: "HotelRoomTypes");
        }
    }
}
