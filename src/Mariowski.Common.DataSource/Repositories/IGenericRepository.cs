using System;
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
        /// <param name="entity">Inserted entity</param>
        /// <returns>Entity</returns>
        TEntity Insert(TEntity entity);

        /// <summary>
        /// Inserts a new entity and gets it's Id.
        /// </summary>
        /// <param name="entity">Entity.</param>
        /// <returns>Id of the entity.</returns>
        TPrimaryKey InsertAndGetId(TEntity entity);

        /// <summary>
        /// Inserts or updates given entity depending on Id's value.
        /// </summary>
        /// <param name="entity">Entity.</param>
        /// <returns>Entity.</returns>
        TEntity InsertOrUpdate(TEntity entity);

        /// <summary>
        /// Inserts or updates given entity depending on Id's value. Also returns Id of the entity.
        /// </summary>
        /// <param name="entity">Entity.</param>
        /// <returns>Id of the entity.</returns>
        TPrimaryKey InsertOrUpdateAndGetId(TEntity entity);


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
        Task<TEntity> FirstOrDefaultAsyncById(TPrimaryKey id);

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
        /// <param name="entity">Entity.</param>
        /// <returns>Entity.</returns>
        TEntity Update(TEntity entity);


        /// <summary>
        /// Deletes an entity.
        /// </summary>
        /// <param name="entity">Entity to be deleted.</param>
        void Delete(TEntity entity);

        /// <summary>
        /// Deletes many entities by function.
        /// </summary>
        /// <param name="predicate">Predicate to filter entities.</param>
        void Delete(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Deletes an entity by primary key.
        /// </summary>
        /// <param name="id">Primary key of the entity.</param>
        void DeleteById(TPrimaryKey id);


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