﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Mariowski.Common.DataSource.Entities;

namespace Mariowski.Common.DataSource.Repositories
{
    public interface IGenericRepository<TEntity, TPrimaryKey> : IRepository
        where TEntity : class, IEntity<TPrimaryKey>
    {
        /// <summary>
        /// Inserts a new entity.
        /// </summary>
        /// <param name="entity">Entity to insert.</param>
        /// <returns>Entity.</returns>
        TEntity Insert(TEntity entity);

        /// <summary>
        /// Inserts a new entity.
        /// </summary>
        /// <param name="entity">Entity to insert.</param>
        /// <returns>Entity.</returns>
        Task<TEntity> InsertAsync(TEntity entity);

        /// <summary>
        /// Inserts new entities.
        /// </summary>
        /// <param name="entities">Entities to insert.</param>
        void Insert(IEnumerable<TEntity> entities);

        /// <summary>
        /// Inserts new entities.
        /// </summary>
        /// <param name="entities">Entities to insert.</param>
        Task InsertAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// Inserts or updates given entity depending on Id's value.
        /// </summary>
        /// <param name="entity">Entity.</param>
        /// <returns>Entity.</returns>
        TEntity InsertOrUpdate(TEntity entity);

        /// <summary>
        /// Inserts or updates given entity depending on Id's value.
        /// </summary>
        /// <param name="entity">Entity.</param>
        /// <returns>Entity.</returns>
        Task<TEntity> InsertOrUpdateAsync(TEntity entity);

        /// <summary>
        /// Inserts or updates given entities depending on theirs Id's value.
        /// </summary>
        /// <param name="entities">Entities.</param>
        void InsertOrUpdate(IEnumerable<TEntity> entities);

        /// <summary>
        /// Inserts or updates given entities depending on theirs Id's value.
        /// </summary>
        /// <param name="entities">Entities.</param>
        Task InsertOrUpdateAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// Used to get a <see cref="T:System.Linq.IQueryable"/> that is used to retrieve entities from entire set/table.
        /// </summary>
        /// <returns><see cref="T:System.Linq.IQueryable"/> to be used to select entities from data source.</returns>
        IQueryable<TEntity> GetAll();

        /// <summary>
        /// Used to get a <see cref="T:System.Linq.IQueryable"/> that is used to retrieve entities from entire set/table.
        /// </summary>
        /// <param name="propertySelectors">A list of include expressions.</param>
        /// <returns><see cref="T:System.Linq.IQueryable"/> to be used to select entities from data source.</returns>
        IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] propertySelectors);

        /// <summary>
        /// Gets an entity with given primary key.
        /// </summary>
        /// <param name="id">Primary key of the entity to get.</param>
        /// <returns>Entity.</returns>
        TEntity GetById(TPrimaryKey id);

        /// <summary>
        /// Gets an entity with given primary key.
        /// </summary>
        /// <param name="id">Primary key of the entity to get.</param>
        /// <returns>Entity.</returns>
        Task<TEntity> GetByIdAsync(TPrimaryKey id);
        
        /// <summary>
        /// Gets entities with given primary key.
        /// </summary>
        /// <param name="ids">Primary key of the entities to get.</param>
        /// <returns>Entities.</returns>
        IEnumerable<TEntity> GetByIds(IEnumerable<TPrimaryKey> ids);

        /// <summary>
        /// Gets entities with given primary key.
        /// </summary>
        /// <param name="ids">Primary key of the entities to get.</param>
        /// <returns>Entities.</returns>
        Task<IEnumerable<TEntity>> GetByIdsAsync(IEnumerable<TPrimaryKey> ids);

        /// <summary>
        /// Gets exactly one entity with given predicate.
        /// Throws exception if no entity or more than one entity.
        /// </summary>
        /// <param name="predicate">Predicate to filter entities.</param>
        /// <returns>Entity.</returns>
        TEntity Single(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Gets exactly one entity with given predicate.
        /// Throws exception if no entity or more than one entity.
        /// </summary>
        /// <param name="predicate">Predicate to filter entities.</param>
        /// <returns>Entity.</returns>
        Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Gets an entity with given primary key or null if not found.
        /// </summary>
        /// <param name="id">Primary key of the entity to get.</param>
        /// <returns>Entity or null.</returns>
        TEntity FirstOrDefaultById(TPrimaryKey id);

        /// <summary>
        /// Gets an entity with given primary key or null if not found.
        /// </summary>
        /// <param name="id">Primary key of the entity to get.</param>
        /// <returns>Entity or null.</returns>
        Task<TEntity> FirstOrDefaultByIdAsync(TPrimaryKey id);

        /// <summary>
        /// Gets an entity with given predicate or null if not found.
        /// </summary>
        /// <param name="predicate">Predicate to filter entities.</param>
        /// <returns>Entity or null.</returns>
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Gets an entity with given predicate or null if not found.
        /// </summary>
        /// <param name="predicate">Predicate to filter entities.</param>
        /// <returns>Entity or null.</returns>
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Checks whatever any entity matches <paramref name="predicate"/>.
        /// </summary>
        /// <param name="predicate">Predicate to filter entities.</param>
        /// <returns>True if any entity matches predicate, false otherwise.</returns>
        bool Any(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Checks whatever any entity matches <paramref name="predicate"/>.
        /// </summary>
        /// <param name="predicate">Predicate to filter entities.</param>
        /// <returns>True if any entity matches predicate, false otherwise.</returns>
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Updates an existing entity.
        /// </summary>
        /// <param name="entity">Entity to update.</param>
        /// <returns>Entity.</returns>
        TEntity Update(TEntity entity);

        /// <summary>
        /// Updates an existing entity.
        /// </summary>
        /// <param name="entity">Entity to update.</param>
        /// <returns>Entity.</returns>
        Task<TEntity> UpdateAsync(TEntity entity);

        /// <summary>
        /// Updates existing entities.
        /// </summary>
        /// <param name="entities">Entities to update.</param>
        void Update(IEnumerable<TEntity> entities);

        /// <summary>
        /// Updates existing entities.
        /// </summary>
        /// <param name="entities">Entities to update.</param>
        Task UpdateAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// Deletes an entity.
        /// </summary>
        /// <param name="entity">Entity to be deleted.</param>
        void Delete(TEntity entity);

        /// <summary>
        /// Deletes an entity.
        /// </summary>
        /// <param name="entity">Entity to be deleted.</param>
        Task DeleteAsync(TEntity entity);

        /// <summary>
        /// Deletes entities.
        /// </summary>
        /// <param name="entities">Entities to be deleted.</param>
        void Delete(IEnumerable<TEntity> entities);

        /// <summary>
        /// Deletes entities.
        /// </summary>
        /// <param name="entities">Entities to be deleted.</param>
        Task DeleteAsync(IEnumerable<TEntity> entities);
        
        /// <summary>
        /// Deletes many entities by function.
        /// </summary>
        /// <param name="predicate">Predicate to filter entities.</param>
        void Delete(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Deletes many entities by function.
        /// </summary>
        /// <param name="predicate">Predicate to filter entities.</param>
        Task DeleteAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Deletes an entity by primary key.
        /// </summary>
        /// <param name="id">Primary key of the entity.</param>
        void DeleteById(TPrimaryKey id);

        /// <summary>
        /// Deletes an entity by primary key.
        /// </summary>
        /// <param name="id">Primary key of the entity.</param>
        Task DeleteByIdAsync(TPrimaryKey id);

        /// <summary>
        /// Deletes entities by primary key.
        /// </summary>
        /// <param name="ids">Primary key of the entities.</param>
        void DeleteByIds(IEnumerable<TPrimaryKey> ids);

        /// <summary>
        /// Deletes entities by primary key.
        /// </summary>
        /// <param name="ids">Primary key of the entities.</param>
        Task DeleteByIdsAsync(IEnumerable<TPrimaryKey> ids);
        
        /// <summary>
        /// Gets count of all entities in this repository.
        /// </summary>
        /// <returns>Count of entities.</returns>
        int Count();

        /// <summary>
        /// Gets count of all entities in this repository.
        /// </summary>
        /// <returns>Count of entities.</returns>
        Task<int> CountAsync();

        /// <summary>
        /// Gets count of all entities in this repository based on given <paramref name="predicate"/>.
        /// </summary>
        /// <param name="predicate">A method to filter count.</param>
        /// <returns>Count of entities.</returns>
        int Count(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Gets count of all entities in this repository based on given <paramref name="predicate"/>.
        /// </summary>
        /// <param name="predicate">A method to filter count.</param>
        /// <returns>Count of entities.</returns>
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Gets long count of all entities in this repository.
        /// </summary>
        /// <returns>Long count of entities.</returns>
        long LongCount();

        /// <summary>
        /// Gets long count of all entities in this repository.
        /// </summary>
        /// <returns>Long count of entities.</returns>
        Task<long> LongCountAsync();

        /// <summary>
        /// Gets long count of all entities in this repository based on given <paramref name="predicate"/>.
        /// </summary>
        /// <param name="predicate">A method to filter count.</param>
        /// <returns>Long count of entities.</returns>
        long LongCount(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Gets long count of all entities in this repository based on given <paramref name="predicate"/>.
        /// </summary>
        /// <param name="predicate">A method to filter count.</param>
        /// <returns>Long count of entities.</returns>
        Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate);
    }
}