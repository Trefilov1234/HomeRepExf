using System.Data.Entity;
using TestServer.Domain.Entities;


namespace TestServer.Context
{
    public class TestContext : DbContext
    {
        public TestContext() : base("DbConnection") { }

        public DbSet<Test> Tests { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<UserResult> UserResults { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Question>().HasRequired(de => de.Test).WithMany(de => de.Questions).HasForeignKey(de => de.TestId);
            modelBuilder.Entity<Test>().HasRequired(de => de.User).WithMany(de => de.Tests).HasForeignKey(de => de.UserId);
            modelBuilder.Entity<UserResult>().HasRequired(de => de.User).WithMany(de => de.UserResults).HasForeignKey(de => de.UserId).WillCascadeOnDelete(false);
            modelBuilder.Entity<UserResult>().HasRequired(de => de.Test).WithMany(de => de.UserResults).HasForeignKey(de => de.TestId).WillCascadeOnDelete(false);
            base.OnModelCreating(modelBuilder);
        }
    }
}
