namespace TestServer.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tests", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Tests", "UserId");
            AddForeignKey("dbo.Tests", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tests", "UserId", "dbo.Users");
            DropIndex("dbo.Tests", new[] { "UserId" });
            DropColumn("dbo.Tests", "UserId");
        }
    }
}
