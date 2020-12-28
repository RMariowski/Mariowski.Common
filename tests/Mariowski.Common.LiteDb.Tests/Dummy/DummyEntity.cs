using Mariowski.Common.DataSource.Entities;

namespace Mariowski.Common.LiteDb.Tests.Dummy
{
    public class DummyEntity : Entity<int>
    {
        public SubDummyEntity Sub { get; set; }

        public string Foo { get; set; }
    }
}