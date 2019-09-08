namespace BookingSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AttributesIEntitityImplementaion : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Menus", "CreateDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Menus", "CreateDate");
        }
    }
}
