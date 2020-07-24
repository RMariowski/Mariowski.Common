using Microsoft.EntityFrameworkCore;

namespace Mariowski.Common.EntityFramework.UnitTests
{
    public class TestContext : DbContext
    {
        public DbSet<DummyEntity> Dummies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("TestDatabase");
        }
    }
}