namespace BookingSystem.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class HotelAttibutesAdded : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.HotelAttributes", "HotelId");
            AddForeignKey("dbo.HotelAttributes", "HotelId", "dbo.HotelDefinitions", "Id", cascadeDelete: true);
        }

        public override void Down()
        {
            DropForeignKey("dbo.HotelAttributes", "HotelId", "dbo.HotelDefinitions");
            DropIndex("dbo.HotelAttributes", new[] { "HotelId" });
        }
    }
}