namespace Mariowski.Common.LiteDb.Tests
{
    public class TestDbContext : LiteDbContext
    {
        public TestDbContext(string connectionString) 
            : base(connectionString)
        {
        }
    }
}