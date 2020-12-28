namespace Mariowski.Common.EntityFramework.Tests
{
    public class DummyRepository : EfGenericRepository<TestContext, DummyEntity, int>
    {
        public DummyRepository(TestContext dbContext)
            : base(dbContext)
        {
        }
    }
}