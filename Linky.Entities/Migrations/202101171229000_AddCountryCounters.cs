namespace Linky.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCountryCounters : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CountryCounters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CountryName = c.String(nullable: false, maxLength: 80),
                        Clicks = c.Int(nullable: false),
                        LinkId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Links", t => t.LinkId, cascadeDelete: true)
                .Index(t => t.LinkId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CountryCounters", "LinkId", "dbo.Links");
            DropIndex("dbo.CountryCounters", new[] { "LinkId" });
            DropTable("dbo.CountryCounters");
        }
    }
}
