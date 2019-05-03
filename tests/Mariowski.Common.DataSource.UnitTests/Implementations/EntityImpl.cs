using System;
using Mariowski.Common.DataSource.Entities;

namespace Mariowski.Common.DataSource.UnitTests.Implementations
{
    public sealed class EntityImpl : Entity<int>
    {
        public Guid Value { get; } = Guid.NewGuid();

        public EntityImpl()
        {
        }

        public EntityImpl(int id)
        {
            Id = id;
        }
    }
}