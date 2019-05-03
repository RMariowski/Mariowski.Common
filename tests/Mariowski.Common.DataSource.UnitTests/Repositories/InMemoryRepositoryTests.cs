using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Mariowski.Common.DataSource.UnitTests.Implementations;
using Xunit;

namespace Mariowski.Common.DataSource.UnitTests.Repositories
{
    public class InMemoryRepositoryTests
    {
        #region Valid

        [Fact]
        public void InMemoryRepository_Insert_InsertNewEntityShouldAddEntityToMemory()
        {
            var repository = new InMemoryRepositoryImpl();
            var entity = new EntityImpl(1);

            repository.Insert(entity);
            repository.Count().Should().BeGreaterThan(0);
            repository.GetAll().Any(e => e.Id == entity.Id && e.Value == entity.Value).Should().BeTrue();
        }

        [Fact]
        public void InMemoryRepository_GetAllIncluding_GetAllShouldReturnAddedEntitiesFromMemory()
        {
            var repository = new InMemoryRepositoryImpl();
            repository.Insert(new EntityImpl(1));
            repository.Insert(new EntityImpl(2));
            repository.Insert(new EntityImpl(3));

            repository.Count().Should().Be(3);
            var array = repository.GetAll().ToArray();
            array[0].Id.Should().Be(1);
            array[1].Id.Should().Be(2);
            array[2].Id.Should().Be(3);
        }

        [Fact]
        public void InMemoryRepository_Update_UpdateShouldReplaceEntityInMemory()
        {
            var repository = new InMemoryRepositoryImpl();
            repository.Insert(new EntityImpl(1));
            var newEntity = new EntityImpl(1);

            repository.Count().Should().Be(1);
            repository.Update(newEntity);
            repository.Count().Should().Be(1);
            repository.GetAll().First().Value.Should().Be(newEntity.Value);
        }

        [Fact]
        public void InMemoryRepository_Delete_DeleteEntityShouldRemoveEntityFromMemory()
        {
            var repository = new InMemoryRepositoryImpl();
            var entity = new EntityImpl(1);

            repository.Insert(entity);
            repository.Delete(entity);
            repository.Count().Should().Be(0);
        }

        [Fact]
        public void InMemoryRepository_Delete_DeleteNotAddedEntityShouldDoNothing()
        {
            var repository = new InMemoryRepositoryImpl();
            var entity = new EntityImpl(1);
            var entity2 = new EntityImpl(2);

            repository.Insert(entity);
            repository.Delete(entity2);
            repository.Count().Should().Be(1);
        }

        #endregion

        #region Invalid

        [Fact]
        public void InMemoryRepository_Insert_InsertNullShouldThrowException()
        {
            var repository = new InMemoryRepositoryImpl();

            Assert.Throws<ArgumentNullException>(() => repository.Insert(null));
        }

        [Fact]
        public void InMemoryRepository_Insert_InsertTransientEntityShouldThrowException()
        {
            var repository = new InMemoryRepositoryImpl();
            var entity = new EntityImpl();

            Assert.Throws<ArgumentException>(() => repository.Insert(entity));
        }

        [Fact]
        public void InMemoryRepository_Insert_InsertEntityWithIdThatIsAlreadyAddedShouldThrowException()
        {
            var repository = new InMemoryRepositoryImpl();
            var entity = new EntityImpl(1);
            var entity2 = new EntityImpl(entity.Id);

            repository.Insert(entity);
            Assert.Throws<InvalidOperationException>(() => repository.Insert(entity2));
        }

        [Fact]
        public void InMemoryRepository_Update_UpdateNullShouldThrowException()
        {
            var repository = new InMemoryRepositoryImpl();

            Assert.Throws<ArgumentNullException>(() => repository.Update(null));
        }

        [Fact]
        public void InMemoryRepository_Update_UpdateTransientEntityShouldThrowException()
        {
            var repository = new InMemoryRepositoryImpl();
            var entity = new EntityImpl();

            Assert.Throws<ArgumentException>(() => repository.Update(entity));
        }

        [Fact]
        public void InMemoryRepository_Update_UpdateEntityWithIdThatIsNotAddedShouldThrowException()
        {
            var repository = new InMemoryRepositoryImpl();
            var entity = new EntityImpl(1);

            Assert.Throws<KeyNotFoundException>(() => repository.Update(entity));
        }

        [Fact]
        public void InMemoryRepository_Delete_DeleteNullShouldThrowException()
        {
            var repository = new InMemoryRepositoryImpl();

            Assert.Throws<ArgumentNullException>(() => repository.Delete((EntityImpl)null));
        }

        [Fact]
        public void InMemoryRepository_Delete_DeleteTransientEntity1ShouldThrowException()
        {
            var repository = new InMemoryRepositoryImpl();
            var entity = new EntityImpl();

            Assert.Throws<ArgumentException>(() => repository.Delete(entity));
        }

        #endregion
    }
}