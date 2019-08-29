namespace BookingSystem.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class update : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Attributes",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                    Description = c.String(),
                    AttributeType = c.Int(nullable: false),
                    IsActive = c.Boolean(nullable: false),
                    IsDelete = c.Boolean(nullable: false),
                    CreateDate = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.Id);
        }

        public override void Down()
        {
            DropTable("dbo.Attributes");
        }
    }
}