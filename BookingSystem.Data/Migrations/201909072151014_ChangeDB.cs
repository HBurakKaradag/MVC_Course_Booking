namespace BookingSystem.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class ChangeDB : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.HotelRooms", "HotelId");
            AddForeignKey("dbo.HotelRooms", "HotelId", "dbo.HotelDefinitions", "Id", cascadeDelete: true);
        }

        public override void Down()
        {
            DropForeignKey("dbo.HotelRooms", "HotelId", "dbo.HotelDefinitions");
            DropIndex("dbo.HotelRooms", new[] { "HotelId" });
        }
    }
}