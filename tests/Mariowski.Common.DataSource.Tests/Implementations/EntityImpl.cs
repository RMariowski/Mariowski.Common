using System;
using Mariowski.Common.DataSource.Entities;

namespace Mariowski.Common.DataSource.Tests.Implementations
{
    public sealed class EntityImpl : Entity<int>
    {
        public Guid Value { get; set; } = Guid.NewGuid();

        public EntityImpl()
        {
        }

        public EntityImpl(int id)
        {
            Id = id;
        }
    }
}