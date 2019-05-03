using System;
using Mariowski.Common.DataSource.Entities;

namespace Mariowski.Common.DataSource.UnitTests.Implementations
{
    public class TimestampableEntityImpl : TimestampableEntity<int>
    {
        public TimestampableEntityImpl() { }

        public TimestampableEntityImpl(DateTime createdAt, DateTime? updatedAt = null) 
            : base(createdAt, updatedAt) { }
    }
}