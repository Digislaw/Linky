namespace Linky.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDailyCounters : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DailyCounters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Day = c.DateTime(nullable: false),
                        Clicks = c.Int(nullable: false),
                        LinkId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Links", t => t.LinkId, cascadeDelete: true)
                .Index(t => t.LinkId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DailyCounters", "LinkId", "dbo.Links");
            DropIndex("dbo.DailyCounters", new[] { "LinkId" });
            DropTable("dbo.DailyCounters");
        }
    }
}
