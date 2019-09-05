namespace BookingSystem.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class updated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HotelDefinitions", "CityId", c => c.Int(nullable: false));
            AddColumn("dbo.HotelDefinitions", "DistrictId", c => c.Int(nullable: false));
            AddColumn("dbo.HotelDefinitions", "Address", c => c.String());
        }

        public override void Down()
        {
            DropColumn("dbo.HotelDefinitions", "Address");
            DropColumn("dbo.HotelDefinitions", "DistrictId");
            DropColumn("dbo.HotelDefinitions", "CityId");
        }
    }
}