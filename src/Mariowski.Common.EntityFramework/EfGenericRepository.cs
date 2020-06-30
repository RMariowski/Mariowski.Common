using Mariowski.Common.DataSource.Entities;
using Mariowski.Common.DataSource.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Mariowski.Common.EntityFramework
{
    public abstract class EfGenericRepository<TDbContext, TEntity, TPrimaryKey> : GenericRepository<TEntity, TPrimaryKey>
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
        /// <param name="entity">Inserted entity.</param>
        /// <returns>Entity.</returns>
        public override TEntity Insert(TEntity entity)
            => Table.Add(entity).Entity;

        /// <summary>
        /// Used to get a <see cref="T:System.Linq.IQueryable"/> that is used to retrieve entities from entire set/table.
        /// </summary>
        /// <param name="propertySelectors">A list of include expressions.</param>
        /// <returns><see cref="T:System.Linq.IQueryable"/> to be used to select entities from data source.</returns>
        public override IQueryable<TEntity> GetAllIncluding(
            params Expression<Func<TEntity, object>>[] propertySelectors)
        {
            var query = Table.AsNoTracking().AsQueryable();
            return propertySelectors == null ? query : propertySelectors.Aggregate(
                query, (current, propertySelector) => current.Include(propertySelector));
        }



        /// <summary>
        /// Updates an existing entity.
        /// </summary>
        /// <param name="entity">Entity.</param>
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
    }
}