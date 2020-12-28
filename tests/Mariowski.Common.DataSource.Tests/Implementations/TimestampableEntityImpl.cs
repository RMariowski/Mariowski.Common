using System;
using Mariowski.Common.DataSource.Entities;

namespace Mariowski.Common.DataSource.Tests.Implementations
{
    public class TimestampableEntityImpl : TimestampableEntity<int>
    {
        public TimestampableEntityImpl()
        {
        }

        public TimestampableEntityImpl(DateTime createdAt, DateTime? updatedAt = null)
            : base(createdAt, updatedAt)
        {
        }
    }
}