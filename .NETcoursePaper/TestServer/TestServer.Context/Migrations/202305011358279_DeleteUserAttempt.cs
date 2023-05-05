namespace TestServer.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteUserAttempt : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserAttempts", "TestId", "dbo.Tests");
            DropForeignKey("dbo.UserAttempts", "UserId", "dbo.Users");
            DropIndex("dbo.UserAttempts", new[] { "UserId" });
            DropIndex("dbo.UserAttempts", new[] { "TestId" });
            DropTable("dbo.UserAttempts");
        }
        
        public override void Down()
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
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.UserAttempts", "TestId");
            CreateIndex("dbo.UserAttempts", "UserId");
            AddForeignKey("dbo.UserAttempts", "UserId", "dbo.Users", "Id");
            AddForeignKey("dbo.UserAttempts", "TestId", "dbo.Tests", "Id");
        }
    }
}
