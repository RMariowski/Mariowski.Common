﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Mariowski.Common.DataSource.Entities;
using Mariowski.Common.DataSource.Repositories;
using MongoDB.Driver;

namespace Mariowski.Common.MongoDb
{
    public abstract class MongoGenericRepository<TEntity, TPrimaryKey> : GenericRepository<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        public IMongoDatabase Database { get; }
        public IMongoCollection<TEntity> Collection { get; }

        /// <summary>
        /// Creates instance of Mongo Generic Repository.
        /// </summary>
        /// <param name="database">Mongo Database.</param>
        /// <param name="collectionName">Name of collection.</param>
        protected MongoGenericRepository(IMongoDatabase database, string collectionName)
        {
            Database = database;
            Collection = database.GetCollection<TEntity>(collectionName);
        }

        /// <summary>
        /// Inserts a new entity.
        /// </summary>
        /// <param name="entity">Entity to insert.</param>
        /// <returns>Entity.</returns>
        public override TEntity Insert(TEntity entity)
        {
            Collection.InsertOne(entity);
            return entity;
        }

        /// <summary>
        /// Inserts a new entity.
        /// </summary>
        /// <param name="entity">Entity to insert.</param>
        /// <returns>Entity.</returns>
        public override async Task<TEntity> InsertAsync(TEntity entity)
        {
            await Collection.InsertOneAsync(entity);
            return entity;
        }

        /// <summary>
        /// Inserts new entities.
        /// </summary>
        /// <param name="entities">Entities to insert.</param>
        public override void Insert(IEnumerable<TEntity> entities)
            => Collection.InsertMany(entities);

        /// <summary>
        /// Inserts new entities.
        /// </summary>
        /// <param name="entities">Entities to insert.</param>
        public override Task InsertAsync(IEnumerable<TEntity> entities)
            => Collection.InsertManyAsync(entities);

        /// <summary>
        /// Used to get a <see cref="T:System.Linq.IQueryable"/> that is used to retrieve entities from entire set/table.
        /// </summary>
        /// <param name="propertySelectors">A list of include expressions.</param>
        /// <returns><see cref="T:System.Linq.IQueryable"/> to be used to select entities from data source.</returns>
        public override IQueryable<TEntity> GetAllIncluding(
            params Expression<Func<TEntity, object>>[] propertySelectors)
        {
            // NOTE: There's no such a thing as "including properties" in mongo repo.
            // So "propertySelectors" can be ignored.

            var query = Collection.AsQueryable();
            return query;
        }

        /// <summary>
        /// Gets exactly one entity with given predicate.
        /// Throws exception if no entity or more than one entity.
        /// </summary>
        /// <param name="predicate">Predicate to filter entities.</param>
        /// <returns>Entity.</returns>
        public override Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate)
            => Collection.Find(predicate).SingleAsync();

        /// <summary>
        /// Gets an entity with given primary key or null if not found.
        /// </summary>
        /// <param name="id">Primary key of the entity to get.</param>
        /// <returns>Entity or null.</returns>
        public override Task<TEntity> FirstOrDefaultByIdAsync(TPrimaryKey id)
            => Collection.Find(CreateEqualityExpressionForId(id)).FirstOrDefaultAsync();

        /// <summary>
        /// Gets an entity with given predicate or null if not found.
        /// </summary>
        /// <param name="predicate">Predicate to filter entities.</param>
        /// <returns>Entity or null.</returns>
        public override Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
            => Collection.Find(predicate).FirstOrDefaultAsync();

        /// <summary>
        /// Checks whatever any entity matches <paramref name="predicate"/>.
        /// </summary>
        /// <param name="predicate">Predicate to filter entities.</param>
        /// <returns>True if any entity matches predicate, false otherwise.</returns>
        public override Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
            => Collection.Find(predicate).AnyAsync();

        /// <summary>
        /// Updates an existing entity.
        /// </summary>
        /// <param name="entity">Entity to update.</param>
        /// <returns>Entity.</returns>
        public override TEntity Update(TEntity entity)
        {
            if (entity is ITimestampable timestampableEntity)
                timestampableEntity.UpdatedAt = DateTime.UtcNow;

            Collection.ReplaceOne(CreateEqualityExpressionForId(entity.Id), entity);
            return entity;
        }

        /// <summary>
        /// Deletes an entity.
        /// </summary>
        /// <param name="entity">Entity to be deleted.</param>
        public override void Delete(TEntity entity)
            => Collection.DeleteOne(CreateEqualityExpressionForId(entity.Id));

        /// <summary>
        /// Deletes many entities by function.
        /// </summary>
        /// <param name="predicate">Predicate to filter entities.</param>
        public override Task DeleteAsync(Expression<Func<TEntity, bool>> predicate)
            => Collection.DeleteManyAsync(predicate);

        /// <summary>
        /// Gets count of all entities in this repository.
        /// </summary>
        /// <returns>Count of entities.</returns>
        public override async Task<int> CountAsync()
            => (int)await Collection.EstimatedDocumentCountAsync();

        /// <summary>
        /// Gets count of all entities in this repository based on given <paramref name="predicate"/>.
        /// </summary>
        /// <param name="predicate">A method to filter count.</param>
        /// <returns>Count of entities.</returns>
        public override async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
            => (int)await Collection.CountDocumentsAsync(predicate);

        /// <summary>
        /// Gets long count of all entities in this repository.
        /// </summary>
        /// <returns>Long count of entities.</returns>
        public override Task<long> LongCountAsync()
            => Collection.EstimatedDocumentCountAsync();

        /// <summary>
        /// Gets long count of all entities in this repository based on given <paramref name="predicate"/>.
        /// </summary>
        /// <param name="predicate">A method to filter count.</param>
        /// <returns>Long count of entities.</returns>
        public override Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate)
            => Collection.CountDocumentsAsync(predicate);
    }
}