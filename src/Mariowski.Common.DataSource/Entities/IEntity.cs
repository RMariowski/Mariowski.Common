namespace Mariowski.Common.DataSource.Entities
{
    public interface IEntity<out TPrimaryKey> : IIdentifiable<TPrimaryKey>
    {
        /// <summary>
        /// Checks if this entity is transient (not persisted to data source and it has not an <see cref="IIdentifiable{TId}.Id"/>).
        /// </summary>
        /// <returns>True, if this entity is transient, false otherwise.</returns>
        bool IsTransient();
    }
}