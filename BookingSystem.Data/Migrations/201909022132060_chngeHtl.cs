namespace BookingSystem.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class chngeHtl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HotelDefinitions", "HotelName", c => c.String());
            DropColumn("dbo.HotelDefinitions", "Name");
        }

        public override void Down()
        {
            AddColumn("dbo.HotelDefinitions", "Name", c => c.String());
            DropColumn("dbo.HotelDefinitions", "HotelName");
        }
    }
}