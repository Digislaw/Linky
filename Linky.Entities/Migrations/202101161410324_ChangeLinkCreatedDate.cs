namespace Linky.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeLinkCreatedDate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Links", "CreatedAt", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Links", "CreatedAt", c => c.DateTime(nullable: false));
        }
    }
}
