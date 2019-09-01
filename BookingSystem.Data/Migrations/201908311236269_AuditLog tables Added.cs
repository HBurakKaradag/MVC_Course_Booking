namespace BookingSystem.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AuditLogtablesAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AuditLogs",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    SessionId = c.String(),
                    ControllerName = c.String(),
                    ActionName = c.String(),
                    CreateDateTime = c.DateTime(nullable: false),
                    CreateDate = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.Id);
        }

        public override void Down()
        {
            DropTable("dbo.AuditLogs");
        }
    }
}