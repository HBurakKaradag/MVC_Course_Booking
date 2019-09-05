namespace BookingSystem.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class hotel_definitionadded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HotelDefinitions",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                    Title = c.String(),
                    Url = c.String(),
                    HoteTypeId = c.Int(nullable: false),
                    IsActive = c.Boolean(nullable: false),
                    IsDeleted = c.Boolean(nullable: false),
                    CreateDate = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            AddColumn("dbo.HotelRoomTypes", "RoomTypeId", c => c.Int(nullable: false));
            AddColumn("dbo.HotelRoomTypes", "RoomCapacity", c => c.Int(nullable: false));
            DropColumn("dbo.HotelRoomTypes", "Name");
            DropColumn("dbo.HotelRoomTypes", "MaxCapacity");
            DropColumn("dbo.HotelRoomTypes", "ShowCaseImage");
        }

        public override void Down()
        {
            AddColumn("dbo.HotelRoomTypes", "ShowCaseImage", c => c.String());
            AddColumn("dbo.HotelRoomTypes", "MaxCapacity", c => c.Int(nullable: false));
            AddColumn("dbo.HotelRoomTypes", "Name", c => c.String());
            DropColumn("dbo.HotelRoomTypes", "RoomCapacity");
            DropColumn("dbo.HotelRoomTypes", "RoomTypeId");
            DropTable("dbo.HotelDefinitions");
        }
    }
}