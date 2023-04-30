using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Question>().HasRequired(de => de.Test).WithMany(de => de.Questions).HasForeignKey(de => de.TestId);
            modelBuilder.Entity<Test>().HasRequired(de => de.User).WithMany(de => de.Tests).HasForeignKey(de => de.UserId);
            base.OnModelCreating(modelBuilder);
        }
    }
}
