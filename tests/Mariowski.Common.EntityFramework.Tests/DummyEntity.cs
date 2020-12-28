using Mariowski.Common.DataSource.Entities;

namespace Mariowski.Common.EntityFramework.Tests
{
    public class DummyEntity : TimestampableEntity<int>
    {
        public string Foo { get; set; }
    }
}