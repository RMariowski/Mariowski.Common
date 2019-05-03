using Mariowski.Common.DataSource.Entities;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Mariowski.Common.DataSource.Repositories
{
    public class InMemoryRepository<TEntity, TPrimaryKey> : GenericRepository<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        private readonly ConcurrentDictionary<TPrimaryKey, TEntity> _memory
            = new ConcurrentDictionary<TPrimaryKey, TEntity>();

        /// <inheritdoc />
        /// <exception cref="T:System.ArgumentNullException"><paramref name="entity"/> is null.</exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="entity"/> is transient.</exception>
        /// <exception cref="T:System.InvalidOperationException">Entity with key of <paramref name="entity"/> is already added.</exception>
        public override TEntity Insert(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Entity cannot be null.");

            if (entity.IsTransient())
                throw new ArgumentException("Cannot insert transient entity to in-memory repository.", nameof(entity));

            if (_memory.Any(e => e.Key.Equals(entity.Id)))
            {
                throw new InvalidOperationException(
                    $"Cannot add entity with id {entity.Id} that is already added.");
            }

            // FIXME: When TryAdd() return false, should exception be thrown?
            _memory.TryAdd(entity.Id, entity);

            return entity;
        }

        /// <inheritdoc />
        public override IQueryable<TEntity> GetAllIncluding(
            params Expression<Func<TEntity, object>>[] propertySelectors)
        {
            // NOTE: There's no such a thing as "including properties" in memory repo.
            // So "propertySelectors" can be ignored.

            return _memory.Values.AsQueryable();
        }

        /// <inheritdoc />
        /// <exception cref="T:System.ArgumentNullException"><paramref name="entity"/> is null.</exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="entity"/> is transient.</exception>
        /// <exception cref="T:System.Collections.Generic.KeyNotFoundException">Entity to replace, with key of <paramref name="entity"/> not found in memory.</exception>
        public override TEntity Update(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Entity cannot be null.");

            if (entity.IsTransient())
                throw new ArgumentException("Cannot update transient entity.", nameof(entity));

            var currentEntity = FirstOrDefaultById(entity.Id);
            if (currentEntity == null)
                throw new KeyNotFoundException($"Cannot find entity to replace by id: {entity.Id}.");

            // FIXME: When TryUpdate() return false, should exception be thrown?
            _memory.TryUpdate(entity.Id, entity, currentEntity);

            return entity;
        }

        /// <inheritdoc />
        /// <exception cref="T:System.ArgumentNullException"><paramref name="entity"/> is null.</exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="entity"/> is transient.</exception>
        public override void Delete(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Entity cannot be null.");

            if (entity.IsTransient())
                throw new ArgumentException("Cannot delete transient entity.", nameof(entity));

            // FIXME: When TryRemove() return false, should exception be thrown?
            _memory.TryRemove(entity.Id, out _);
        }
    }
}