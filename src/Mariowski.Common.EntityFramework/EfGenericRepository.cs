using Mariowski.Common.DataSource.Entities;
using Mariowski.Common.DataSource.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Mariowski.Common.EntityFramework
{
    public abstract class EfGenericRepository<TDbContext, TEntity, TPrimaryKey>
        : GenericRepository<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
        where TDbContext : DbContext
    {
        public DbContext Context { get; }

        public DbSet<TEntity> Table => Context.Set<TEntity>();

        /// <summary>
        /// Creates instance of Entity Generic Repository.
        /// </summary>
        /// <param name="dbContext">Database context.</param>
        protected EfGenericRepository(TDbContext dbContext)
        {
            Context = dbContext;
        }

        /// <summary>
        /// Inserts a new entity.
        /// </summary>
        /// <param name="entity">Entity to insert.</param>
        /// <returns>Entity.</returns>
        public override TEntity Insert(TEntity entity)
            => Table.Add(entity).Entity;

        /// <summary>
        /// Inserts a new entity.
        /// </summary>
        /// <param name="entity">Entity to insert.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
        /// <returns>Entity.</returns>
        public override async Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            var entityEntry = await Table.AddAsync(entity, cancellationToken);
            return entityEntry.Entity;
        }

        /// <summary>
        /// Inserts new entities.
        /// </summary>
        /// <param name="entities">Entities to insert.</param>
        public override void Insert(IEnumerable<TEntity> entities)
            => Table.AddRange(entities);

        /// <summary>
        /// Inserts new entities.
        /// </summary>
        /// <param name="entities">Entities to insert.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
        public override Task InsertAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
            => Table.AddRangeAsync(entities, cancellationToken);

        /// <summary>
        /// Used to get a <see cref="T:System.Linq.IQueryable"/> that is used to retrieve entities from entire set/table.
        /// </summary>
        /// <param name="propertySelectors">A list of include expressions.</param>
        /// <returns><see cref="T:System.Linq.IQueryable"/> to be used to select entities from data source.</returns>
        public override IQueryable<TEntity> GetAllIncluding(
            params Expression<Func<TEntity, object>>[] propertySelectors)
        {
            var query = Table.AsQueryable();

            if (propertySelectors is null)
                return query;

            return propertySelectors.Aggregate(query,
                (current, propertySelector) => current.Include(propertySelector));
        }

        /// <summary>
        /// Gets entities with given primary key.
        /// </summary>
        /// <param name="ids">Primary key of the entities to get.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
        /// <returns>Entities.</returns>
        public override Task<TEntity[]> GetByIdsAsync(IEnumerable<TPrimaryKey> ids,
            CancellationToken cancellationToken = default)
            => GetAll().Where(e => ids.Contains(e.Id)).ToArrayAsync(cancellationToken);

        /// <summary>
        /// Gets exactly one entity with given predicate.
        /// Throws exception if no entity or more than one entity.
        /// </summary>
        /// <param name="predicate">Predicate to filter entities.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
        /// <returns>Entity.</returns>
        public override Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default)
            => GetAll().SingleAsync(predicate, cancellationToken);

        /// <summary>
        /// Gets an entity with given primary key or null if not found.
        /// </summary>
        /// <param name="id">Primary key of the entity to get.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
        /// <returns>Entity or null.</returns>
        public override Task<TEntity> FirstOrDefaultByIdAsync(TPrimaryKey id,
            CancellationToken cancellationToken = default)
            => GetAll().FirstOrDefaultAsync(CreateEqualityExpressionForId(id), cancellationToken);

        /// <summary>
        /// Gets an entity with given predicate or null if not found.
        /// </summary>
        /// <param name="predicate">Predicate to filter entities.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
        /// <returns>Entity or null.</returns>
        public override Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default)
            => GetAll().FirstOrDefaultAsync(predicate, cancellationToken);

        /// <summary>
        /// Checks whatever any entity matches <paramref name="predicate"/>.
        /// </summary>
        /// <param name="predicate">Predicate to filter entities.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
        /// <returns>True if any entity matches predicate, false otherwise.</returns>
        public override Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default)
            => GetAll().AnyAsync(predicate, cancellationToken);

        /// <summary>
        /// Updates an existing entity.
        /// </summary>
        /// <param name="entity">Entity to update.</param>
        /// <returns>Entity.</returns>
        public override TEntity Update(TEntity entity)
        {
            if (entity is ITimestampable timestampableEntity)
                timestampableEntity.UpdatedAt = DateTime.UtcNow;

            Context.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        /// <summary>
        /// Deletes an entity.
        /// </summary>
        /// <param name="entity">Entity to be deleted.</param>
        public override void Delete(TEntity entity)
            => Table.Remove(entity);

        /// <summary>
        /// Deletes many entities by function.
        /// </summary>
        /// <param name="predicate">Predicate to filter entities.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
        public override async Task DeleteAsync(Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default)
        {
            var entities = await GetAll().Where(predicate).ToListAsync(cancellationToken);
            foreach (var entity in entities)
                Delete(entity);
        }

        /// <summary>
        /// Deletes entities.
        /// </summary>
        /// <param name="entities">Entities to be deleted.</param>
        public override void Delete(IEnumerable<TEntity> entities)
            => Table.RemoveRange(entities);

        /// <summary>
        /// Deletes entities by primary key.
        /// </summary>
        /// <param name="ids">Primary key of the entities.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
        public override async Task DeleteByIdsAsync(IEnumerable<TPrimaryKey> ids,
            CancellationToken cancellationToken = default)
        {
            var entities = await GetAll().Where(e => ids.Contains(e.Id)).ToArrayAsync(cancellationToken);
            await DeleteAsync(entities, cancellationToken);
        }

        /// <summary>
        /// Gets count of all entities in this repository.
        /// </summary>
        /// <returns>Count of entities.</returns>
        public override Task<int> CountAsync(CancellationToken cancellationToken = default)
            => GetAll().CountAsync(cancellationToken);

        /// <summary>
        /// Gets count of all entities in this repository based on given <paramref name="predicate"/>.
        /// </summary>
        /// <param name="predicate">A method to filter count.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
        /// <returns>Count of entities.</returns>
        public override Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default)
            => GetAll().CountAsync(predicate, cancellationToken);

        /// <summary>
        /// Gets long count of all entities in this repository.
        /// </summary>
        /// <returns>Long count of entities.</returns>
        public override Task<long> LongCountAsync(CancellationToken cancellationToken = default)
            => GetAll().LongCountAsync(cancellationToken);

        /// <summary>
        /// Gets long count of all entities in this repository based on given <paramref name="predicate"/>.
        /// </summary>
        /// <param name="predicate">A method to filter count.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
        /// <returns>Long count of entities.</returns>
        public override Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default)
            => GetAll().LongCountAsync(predicate, cancellationToken);
    }
}