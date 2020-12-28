using Mariowski.Common.DataSource.Entities;

namespace Mariowski.Common.LiteDb.Tests.Dummy
{
    public class SubDummyEntity : Entity<int>
    {
        public string Bar { get; set; }
    }
}