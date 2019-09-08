namespace BookingSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class boolnulable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.HotelRooms", "IsActive", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.HotelRooms", "IsActive", c => c.Boolean(nullable: false));
        }
    }
}
