namespace BookingSystem.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class updatemig : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HotelTypes",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Title = c.String(),
                    Description = c.String(),
                    IsDeleted = c.Boolean(nullable: false),
                    IsActive = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.Id);
        }

        public override void Down()
        {
            DropTable("dbo.HotelTypes");
        }
    }
}