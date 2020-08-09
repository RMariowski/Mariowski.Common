using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
        public virtual Task<TEntity> InsertAsync(TEntity entity)
            => Task.FromResult(Insert(entity));

        /// <inheritdoc />
        public virtual void Insert(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
                Insert(entity);
        }

        /// <inheritdoc />
        public virtual Task InsertAsync(IEnumerable<TEntity> entities)
        {
            Insert(entities);
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public virtual TEntity InsertOrUpdate(TEntity entity)
            => entity.IsTransient() ? Insert(entity) : Update(entity);

        /// <inheritdoc />
        public virtual Task<TEntity> InsertOrUpdateAsync(TEntity entity)
            => entity.IsTransient() ? InsertAsync(entity) : UpdateAsync(entity);

        /// <inheritdoc />
        public void InsertOrUpdate(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
                InsertOrUpdate(entity);
        }

        /// <inheritdoc />
        public Task InsertOrUpdateAsync(IEnumerable<TEntity> entities)
        {
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
        public virtual Task<TEntity> GetByIdAsync(TPrimaryKey id)
            => Task.FromResult(GetById(id));

        /// <inheritdoc />
        public IEnumerable<TEntity> GetByIds(IEnumerable<TPrimaryKey> ids)
            => GetAll().Where(e => ids.Contains(e.Id));

        /// <inheritdoc />
        public Task<IEnumerable<TEntity>> GetByIdsAsync(IEnumerable<TPrimaryKey> ids)
            => Task.FromResult(GetByIds(ids));

        /// <inheritdoc />
        public virtual TEntity Single(Expression<Func<TEntity, bool>> predicate)
            => GetAll().Single(predicate);

        /// <inheritdoc />
        public virtual Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate)
            => Task.FromResult(Single(predicate));

        /// <inheritdoc />
        public virtual TEntity FirstOrDefaultById(TPrimaryKey id)
            => GetAll().FirstOrDefault(e => id.Equals(e.Id));

        /// <inheritdoc />
        public virtual Task<TEntity> FirstOrDefaultByIdAsync(TPrimaryKey id)
            => Task.FromResult(FirstOrDefaultById(id));

        /// <inheritdoc />
        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
            => GetAll().FirstOrDefault(predicate);

        /// <inheritdoc />
        public virtual Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
            => Task.FromResult(FirstOrDefault(predicate));

        /// <inheritdoc />
        public virtual bool Any(Expression<Func<TEntity, bool>> predicate)
            => GetAll().Any(predicate);

        /// <inheritdoc />
        public virtual Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
            => Task.FromResult(Any(predicate));

        /// <inheritdoc />
        public abstract TEntity Update(TEntity entity);

        /// <inheritdoc />
        public virtual Task<TEntity> UpdateAsync(TEntity entity)
            => Task.FromResult(Update(entity));

        /// <inheritdoc />
        public void Update(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
                Update(entity);
        }

        /// <inheritdoc />
        public Task UpdateAsync(IEnumerable<TEntity> entities)
        {
            Update(entities);
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public abstract void Delete(TEntity entity);

        /// <inheritdoc />
        public virtual Task DeleteAsync(TEntity entity)
        {
            Delete(entity);
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public void Delete(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
                Delete(entity);
        }

        /// <inheritdoc />
        public Task DeleteAsync(IEnumerable<TEntity> entities)
        {
            Delete(entities);
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public virtual void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            var entities = GetAll().Where(predicate).ToList();
            foreach (var entity in entities)
                Delete(entity);
        }

        /// <inheritdoc />
        public virtual Task DeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
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
        public virtual async Task DeleteByIdAsync(TPrimaryKey id)
        {
            var entity = await FirstOrDefaultByIdAsync(id);
            if (entity is null)
                return;

            await DeleteAsync(entity);
        }

        /// <inheritdoc />
        public void DeleteByIds(IEnumerable<TPrimaryKey> ids)
        {
            foreach (var id in ids)
                DeleteById(id);
        }

        /// <inheritdoc />
        public Task DeleteByIdsAsync(IEnumerable<TPrimaryKey> ids)
        {
            DeleteByIds(ids);
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public virtual int Count()
            => GetAll().Count();

        /// <inheritdoc />
        public virtual Task<int> CountAsync()
            => Task.FromResult(Count());

        /// <inheritdoc />
        public virtual int Count(Expression<Func<TEntity, bool>> predicate)
            => GetAll().Where(predicate).Count();

        /// <inheritdoc />
        public virtual Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
            => Task.FromResult(Count(predicate));

        /// <inheritdoc />
        public virtual long LongCount()
            => GetAll().LongCount();

        /// <inheritdoc />
        public virtual Task<long> LongCountAsync()
            => Task.FromResult(LongCount());

        /// <inheritdoc />
        public virtual long LongCount(Expression<Func<TEntity, bool>> predicate)
            => GetAll().Where(predicate).LongCount();

        /// <inheritdoc />
        public virtual Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate)
            => Task.FromResult(LongCount(predicate));
    }
}