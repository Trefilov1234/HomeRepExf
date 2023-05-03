namespace TestServer.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserAttemptResult : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserAttempts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        TestId = c.Int(nullable: false),
                        Attempt = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tests", t => t.TestId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.TestId);
            
            CreateTable(
                "dbo.UserResults",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        TestId = c.Int(nullable: false),
                        Result = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tests", t => t.TestId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.TestId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserResults", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserResults", "TestId", "dbo.Tests");
            DropForeignKey("dbo.UserAttempts", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserAttempts", "TestId", "dbo.Tests");
            DropIndex("dbo.UserResults", new[] { "TestId" });
            DropIndex("dbo.UserResults", new[] { "UserId" });
            DropIndex("dbo.UserAttempts", new[] { "TestId" });
            DropIndex("dbo.UserAttempts", new[] { "UserId" });
            DropTable("dbo.UserResults");
            DropTable("dbo.UserAttempts");
        }
    }
}
