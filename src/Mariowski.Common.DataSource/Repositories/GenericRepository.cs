using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Mariowski.Common.DataSource.Entities;

namespace Mariowski.Common.DataSource.Repositories
{
    public abstract class GenericRepository<TEntity, TPrimaryKey> : IGenericRepository<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        /// <inheritdoc />
        public abstract TEntity Insert(TEntity entity);

        /// <inheritdoc />
        public virtual Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return Task.FromResult(Insert(entity));
        }

        /// <inheritdoc />
        public virtual void Insert(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
                Insert(entity);
        }

        /// <inheritdoc />
        public virtual Task InsertAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            Insert(entities);
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public virtual TEntity InsertOrUpdate(TEntity entity)
        {
            if (entity.IsTransient())
                return Insert(entity);

            var entityFromRepository = FirstOrDefaultById(entity.Id);
            return entityFromRepository is null ? Insert(entity) : Update(entity);
        }

        /// <inheritdoc />
        public virtual async Task<TEntity> InsertOrUpdateAsync(TEntity entity,
            CancellationToken cancellationToken = default)
        {
            if (entity.IsTransient())
                return await InsertAsync(entity, cancellationToken);

            var entityFromRepository = await FirstOrDefaultByIdAsync(entity.Id, cancellationToken);
            return entityFromRepository is null
                ? await InsertAsync(entity, cancellationToken)
                : await UpdateAsync(entity, cancellationToken);
        }

        /// <inheritdoc />
        public virtual void InsertOrUpdate(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
                InsertOrUpdate(entity);
        }

        /// <inheritdoc />
        public virtual Task InsertOrUpdateAsync(IEnumerable<TEntity> entities,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            InsertOrUpdate(entities);
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public virtual IQueryable<TEntity> GetAll()
            => GetAllIncluding();

        /// <inheritdoc />
        public abstract IQueryable<TEntity> GetAllIncluding(
            params Expression<Func<TEntity, object>>[] propertySelectors);

        /// <inheritdoc />
        /// <exception cref="T:KeyNotFoundException">Entity with given <paramref name="id"></paramref> not found.</exception>
        public virtual TEntity GetById(TPrimaryKey id)
        {
            var entity = FirstOrDefaultById(id);
            if (entity is null)
            {
                throw new KeyNotFoundException(
                    $"There is no such an entity with given primary key. Entity type: {typeof(TEntity).FullName}, primary key: {id}");
            }

            return entity;
        }

        /// <inheritdoc />
        /// <exception cref="T:KeyNotFoundException">Entity with given <paramref name="id"></paramref> not found.</exception>
        public virtual async Task<TEntity> GetByIdAsync(TPrimaryKey id, CancellationToken cancellationToken = default)
        {
            var entity = await FirstOrDefaultByIdAsync(id, cancellationToken);
            if (entity is null)
            {
                throw new KeyNotFoundException(
                    $"There is no such an entity with given primary key. Entity type: {typeof(TEntity).FullName}, primary key: {id}");
            }

            return entity;
        }

        /// <inheritdoc />
        public virtual TEntity[] GetByIds(IEnumerable<TPrimaryKey> ids)
            => GetAll().Where(e => ids.Contains(e.Id)).ToArray();

        /// <inheritdoc />
        public virtual Task<TEntity[]> GetByIdsAsync(IEnumerable<TPrimaryKey> ids,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return Task.FromResult(GetByIds(ids));
        }

        /// <inheritdoc />
        public virtual TEntity Single(Expression<Func<TEntity, bool>> predicate)
            => GetAll().Single(predicate);

        /// <inheritdoc />
        public virtual Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return Task.FromResult(Single(predicate));
        }

        /// <inheritdoc />
        public virtual TEntity FirstOrDefaultById(TPrimaryKey id)
            => GetAll().FirstOrDefault(CreateEqualityExpressionForId(id));

        /// <inheritdoc />
        public virtual Task<TEntity> FirstOrDefaultByIdAsync(TPrimaryKey id,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return Task.FromResult(FirstOrDefaultById(id));
        }

        /// <inheritdoc />
        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
            => GetAll().FirstOrDefault(predicate);

        /// <inheritdoc />
        public virtual Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return Task.FromResult(FirstOrDefault(predicate));
        }

        /// <inheritdoc />
        public virtual bool Any(Expression<Func<TEntity, bool>> predicate)
            => GetAll().Any(predicate);

        /// <inheritdoc />
        public virtual Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return Task.FromResult(Any(predicate));
        }

        /// <inheritdoc />
        public abstract TEntity Update(TEntity entity);

        /// <inheritdoc />
        public virtual Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return Task.FromResult(Update(entity));
        }

        /// <inheritdoc />
        public virtual void Update(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
                Update(entity);
        }

        /// <inheritdoc />
        public virtual Task UpdateAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            Update(entities);
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public abstract void Delete(TEntity entity);

        /// <inheritdoc />
        public virtual Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            Delete(entity);
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public virtual void Delete(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
                Delete(entity);
        }

        /// <inheritdoc />
        public virtual Task DeleteAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            Delete(entities);
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public virtual void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            var entities = GetAll().Where(predicate).ToArray();
            foreach (var entity in entities)
                Delete(entity);
        }

        /// <inheritdoc />
        public virtual Task DeleteAsync(Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            Delete(predicate);
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public virtual void DeleteById(TPrimaryKey id)
        {
            var entity = FirstOrDefaultById(id);
            if (entity is null)
                return;

            Delete(entity);
        }

        /// <inheritdoc />
        public virtual async Task DeleteByIdAsync(TPrimaryKey id, CancellationToken cancellationToken = default)
        {
            var entity = await FirstOrDefaultByIdAsync(id, cancellationToken);
            if (entity is null)
                return;

            await DeleteAsync(entity, cancellationToken);
        }

        /// <inheritdoc />
        public virtual void DeleteByIds(IEnumerable<TPrimaryKey> ids)
        {
            var entities = GetAll().Where(e => ids.Contains(e.Id)).ToArray();
            Delete(entities);
        }

        /// <inheritdoc />
        public virtual Task DeleteByIdsAsync(IEnumerable<TPrimaryKey> ids,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            DeleteByIds(ids);
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public virtual int Count()
            => GetAll().Count();

        /// <inheritdoc />
        public virtual Task<int> CountAsync(CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return Task.FromResult(Count());
        }

        /// <inheritdoc />
        public virtual int Count(Expression<Func<TEntity, bool>> predicate)
            => GetAll().Where(predicate).Count();

        /// <inheritdoc />
        public virtual Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return Task.FromResult(Count(predicate));
        }

        /// <inheritdoc />
        public virtual long LongCount()
            => GetAll().LongCount();

        /// <inheritdoc />
        public virtual Task<long> LongCountAsync(CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return Task.FromResult(LongCount());
        }

        /// <inheritdoc />
        public virtual long LongCount(Expression<Func<TEntity, bool>> predicate)
            => GetAll().Where(predicate).LongCount();

        /// <inheritdoc />
        public virtual Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return Task.FromResult(LongCount(predicate));
        }

        /// <summary>
        /// Creates expression tree for <see cref="T:TPrimaryKey"/> equality.
        /// </summary>
        /// <param name="id"><see cref="T:TPrimaryKey"/> of entity.</param>
        /// <returns>Expression tree.</returns>
        protected virtual Expression<Func<TEntity, bool>> CreateEqualityExpressionForId(TPrimaryKey id)
        {
            var lambdaParam = Expression.Parameter(typeof(TEntity));

            var leftExpression = Expression.PropertyOrField(lambdaParam, "Id");

            Expression<Func<object>> closure = () => id;
            var rightExpression = Expression.Convert(closure.Body, leftExpression.Type);

            var lambdaBody = Expression.Equal(leftExpression, rightExpression);
            return Expression.Lambda<Func<TEntity, bool>>(lambdaBody, lambdaParam);
        }
    }
}