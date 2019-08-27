namespace BookingSystem.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class HotelRoomTypes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HotelRoomTypes",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    HotelId = c.Int(nullable: false),
                    Name = c.String(),
                    MaxCapacity = c.Int(nullable: false),
                    ShowCaseImage = c.String(),
                    IsActive = c.Boolean(nullable: false),
                    IsDeleted = c.Boolean(nullable: false),
                    CreateDate = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            AddColumn("dbo.HotelTypes", "CreateDate", c => c.DateTime(nullable: false));
        }

        public override void Down()
        {
            DropColumn("dbo.HotelTypes", "CreateDate");
            DropTable("dbo.HotelRoomTypes");
        }
    }
}