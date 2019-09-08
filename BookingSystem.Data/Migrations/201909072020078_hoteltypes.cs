namespace BookingSystem.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class hoteltypes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HotelRoomTypes", "MaxCapacity", c => c.Int(nullable: false));
            AddColumn("dbo.HotelRoomTypes", "ImageUrl", c => c.String());
            AddColumn("dbo.HotelRoomTypes", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.HotelRoomTypes", "RoomCapacity");
        }

        public override void Down()
        {
            AddColumn("dbo.HotelRoomTypes", "RoomCapacity", c => c.Int(nullable: false));
            DropColumn("dbo.HotelRoomTypes", "Price");
            DropColumn("dbo.HotelRoomTypes", "ImageUrl");
            DropColumn("dbo.HotelRoomTypes", "MaxCapacity");
        }
    }
}