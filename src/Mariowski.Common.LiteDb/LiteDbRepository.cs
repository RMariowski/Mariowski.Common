using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LiteDB;
using Mariowski.Common.DataSource.Entities;
using Mariowski.Common.DataSource.Repositories;

namespace Mariowski.Common.LiteDb
{
    public abstract class LiteDbRepository<TDbContext, TEntity, TPrimaryKey>
        : GenericRepository<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
        where TDbContext : LiteDbContext
    {
        protected LiteDbContext Context { get; }
        protected ILiteCollection<TEntity> Collection { get; }

        /// <summary>
        /// Creates instance of LiteDB Generic Repository.
        /// </summary>
        /// <param name="dbContext">Database context.</param>
        /// <param name="collectionName">Collection name, when null then entity type name.</param>
        protected LiteDbRepository(TDbContext dbContext, string collectionName = null)
        {
            Context = dbContext;

            collectionName ??= typeof(TEntity).Name;
            Collection = Context.Database.GetCollection<TEntity>(collectionName);
        }

        /// <summary>
        /// Inserts a new entity.
        /// </summary>
        /// <param name="entity">Entity to insert.</param>
        /// <returns>Entity.</returns>
        public override TEntity Insert(TEntity entity)
        {
            Collection.Insert(entity);
            return entity;
        }

        /// <summary>
        /// Inserts new entities.
        /// </summary>
        /// <param name="entities">Entities to insert.</param>
        public override void Insert(IEnumerable<TEntity> entities)
            => Collection.Insert(entities);

        /// <summary>
        /// Used to get a <see cref="T:System.Linq.IQueryable"/> that is used to retrieve entities from entire collection.
        /// </summary>
        /// <param name="propertySelectors">A list of include expressions.</param>
        /// <returns><see cref="T:System.Linq.IQueryable"/> to be used to select entities from data source.</returns>
        public override IQueryable<TEntity> GetAllIncluding(
            params Expression<Func<TEntity, object>>[] propertySelectors)
        {
            var query = Query();

            if (propertySelectors != null)
            {
                query = propertySelectors.Aggregate(query,
                    (current, includeProperty) => current.Include(includeProperty));
            }

            // TODO: Better way to convert ILiteQueryable to IQueryable
            return query.ToArray().AsQueryable();
        }

        /// <summary>
        /// Gets entities with given primary key.
        /// </summary>
        /// <param name="ids">Primary key of the entities to get.</param>
        /// <returns>Entities.</returns>
        public override TEntity[] GetByIds(IEnumerable<TPrimaryKey> ids)
            => Query().Where(e => ids.Contains(e.Id)).ToArray();

        /// <summary>
        /// Gets exactly one entity with given predicate.
        /// Throws exception if no entity or more than one entity.
        /// </summary>
        /// <param name="predicate">Predicate to filter entities.</param>
        /// <returns>Entity.</returns>
        public override TEntity Single(Expression<Func<TEntity, bool>> predicate)
            => Query().Where(predicate).Single();

        /// <summary>
        /// Gets an entity with given predicate or null if not found.
        /// </summary>
        /// <param name="predicate">Predicate to filter entities.</param>
        /// <returns>Entity or null.</returns>
        public override TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
            => Query().Where(predicate).FirstOrDefault();

        /// <summary>
        /// Checks whatever any entity matches <paramref name="predicate"/>.
        /// </summary>
        /// <param name="predicate">Predicate to filter entities.</param>
        /// <returns>True if any entity matches predicate, false otherwise.</returns>
        public override bool Any(Expression<Func<TEntity, bool>> predicate)
            => Query().Where(predicate).LongCount() > 0;

        /// <summary>
        /// Updates an existing entity.
        /// </summary>
        /// <param name="entity">Entity to update.</param>
        /// <returns>Entity.</returns>
        public override TEntity Update(TEntity entity)
        {
            Collection.Update(entity);
            return entity;
        }

        /// <summary>
        /// Updates existing entities.
        /// </summary>
        /// <param name="entities">Entities to update.</param>
        public override void Update(IEnumerable<TEntity> entities)
            => Collection.Update(entities);

        /// <summary>
        /// Deletes an entity.
        /// </summary>
        /// <param name="entity">Entity to be deleted.</param>
        public override void Delete(TEntity entity)
        {
            if (entity.IsTransient())
                return;

            DeleteById(entity.Id);
        }

        /// <summary>
        /// Deletes entities.
        /// </summary>
        /// <param name="entities">Entities to be deleted.</param>
        public override void Delete(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
                Delete(entity);
        }

        /// <summary>
        /// Deletes many entities by function.
        /// </summary>
        /// <param name="predicate">Predicate to filter entities.</param>
        public override void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            var entities = Query().Where(predicate).ToArray();
            foreach (var entity in entities)
                Delete(entity);
        }

        /// <summary>
        /// Deletes an entity by primary key.
        /// </summary>
        /// <param name="id">Primary key of the entity.</param>
        public override void DeleteById(TPrimaryKey id)
        {
            Context.Database.Execute($"DELETE {Collection.Name} WHERE _id = {id}");
        }

        /// <summary>
        /// Deletes entities by primary key.
        /// </summary>
        /// <param name="ids">Primary key of the entities.</param>
        public override void DeleteByIds(IEnumerable<TPrimaryKey> ids)
        {
            var entities = Query().Where(e => ids.Contains(e.Id)).ToArray();
            Delete(entities);
        }

        /// <summary>
        /// Gets count of all entities in this repository.
        /// </summary>
        /// <returns>Count of entities.</returns>
        public override int Count()
            => Collection.Count();

        /// <summary>
        /// Gets count of all entities in this repository based on given <paramref name="predicate"/>.
        /// </summary>
        /// <param name="predicate">A method to filter count.</param>
        /// <returns>Count of entities.</returns>
        public override int Count(Expression<Func<TEntity, bool>> predicate)
            => Query().Where(predicate).Count();

        /// <summary>
        /// Gets long count of all entities in this repository.
        /// </summary>
        /// <returns>Long count of entities.</returns>
        public override long LongCount()
            => Collection.LongCount();

        /// <summary>
        /// Gets long count of all entities in this repository based on given <paramref name="predicate"/>.
        /// </summary>
        /// <param name="predicate">A method to filter count.</param>
        /// <returns>Long count of entities.</returns>
        public override long LongCount(Expression<Func<TEntity, bool>> predicate)
            => Query().Where(predicate).LongCount();

        /// <summary>
        /// Gets <see cref="ILiteQueryable{T}"/> from collection.
        /// </summary>
        /// <returns>LiteDB's IQueryable</returns>
        protected ILiteQueryable<TEntity> Query()
            => Collection.Query();
    }
}