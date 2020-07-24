namespace Mariowski.Common.EntityFramework.UnitTests
{
    public class DummyRepository : EfGenericRepository<TestContext, DummyEntity, int>
    {
        public DummyRepository(TestContext dbContext)
            : base(dbContext)
        {
        }
    }
}