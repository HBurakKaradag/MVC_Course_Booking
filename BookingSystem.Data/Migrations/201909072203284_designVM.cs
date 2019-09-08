namespace BookingSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class designVM : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.HotelRooms", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.HotelRooms", "IsActive", c => c.Boolean());
        }
    }
}
