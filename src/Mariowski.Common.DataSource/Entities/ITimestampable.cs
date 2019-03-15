using System;

namespace Mariowski.Common.DataSource.Entities
{
    public interface ITimestampable
    {
        DateTime CreatedAt { get; set; }
        DateTime UpdatedAt { get; set; }
    }
}