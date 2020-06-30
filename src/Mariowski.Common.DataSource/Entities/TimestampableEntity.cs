using System;

namespace Mariowski.Common.DataSource.Entities
{
    [Serializable]
    public abstract class TimestampableEntity<TPrimaryKey> : Entity<TPrimaryKey>, ITimestampable
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// Creates a new instance of <see cref="T:TimestampableEntity"/> with dates set to <see cref="P:DateTime.UtcNow"/>.
        /// </summary>
        protected TimestampableEntity()
        {
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = CreatedAt;
        }

        /// <summary>
        /// Creates a new instance of <see cref="T:TimestampableEntity"/> with specified <see cref="T:DateTime"/>.
        /// </summary>
        /// <param name="createdAt">Entity creation date.</param>
        /// <param name="updatedAt">Entity last update date.</param>
        protected TimestampableEntity(DateTime createdAt, DateTime? updatedAt = null)
        {
            CreatedAt = createdAt;
            UpdatedAt = updatedAt ?? CreatedAt;
        }
    }
}