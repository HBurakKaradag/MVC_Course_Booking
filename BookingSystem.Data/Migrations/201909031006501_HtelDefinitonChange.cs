namespace BookingSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HtelDefinitonChange : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HotelDefinitions", "HotelTypeId", c => c.Int(nullable: false));
            DropColumn("dbo.HotelDefinitions", "HoteTypeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.HotelDefinitions", "HoteTypeId", c => c.Int(nullable: false));
            DropColumn("dbo.HotelDefinitions", "HotelTypeId");
        }
    }
}
