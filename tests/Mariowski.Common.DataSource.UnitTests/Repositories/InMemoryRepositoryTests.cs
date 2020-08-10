using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Mariowski.Common.DataSource.UnitTests.Implementations;
using Xunit;

namespace Mariowski.Common.DataSource.UnitTests.Repositories
{
    public partial class InMemoryRepositoryTests
    {
        private readonly InMemoryRepositoryImpl _repository;

        public InMemoryRepositoryTests()
        {
            _repository = new InMemoryRepositoryImpl();
        }

        [Fact]
        public void InsertOrUpdate_ShouldAddEntity_WhenDoesNotExists()
        {
            var entity = new EntityImpl(1);
            _repository.Insert(entity);

            _repository.InsertOrUpdate(entity);

            _repository.GetAll().Any(e => e.Id == entity.Id && e.Value == entity.Value).Should().BeTrue();
        }

        [Fact]
        public void InsertOrUpdate_ShouldUpdateEntity_WhenExists()
        {
            var newValue = Guid.NewGuid();
            var entity = new EntityImpl(1);
            _repository.Insert(entity);
            entity.Value = newValue;

            _repository.InsertOrUpdate(entity);

            _repository.GetAll().Any(e => e.Id == entity.Id && e.Value == newValue).Should().BeTrue();
        }

        [Fact]
        public async Task InsertOrUpdateAsync_ShouldAddEntity_WhenDoesNotExists()
        {
            var entity = new EntityImpl(1);
            _repository.Insert(entity);

            await _repository.InsertOrUpdateAsync(entity);

            _repository.GetAll().Any(e => e.Id == entity.Id && e.Value == entity.Value).Should().BeTrue();
        }

        [Fact]
        public async Task InsertOrUpdateAsync_ShouldUpdateEntity_WhenExists()
        {
            var newValue = Guid.NewGuid();
            var entity = new EntityImpl(1);
            _repository.Insert(entity);
            entity.Value = newValue;

            await _repository.InsertOrUpdateAsync(entity);

            _repository.GetAll().Any(e => e.Id == entity.Id && e.Value == newValue).Should().BeTrue();
        }

        [Fact]
        public void GetAllIncluding_GetAllShouldReturnAddedEntitiesFromMemory()
        {
            _repository.Insert(new EntityImpl(1));
            _repository.Insert(new EntityImpl(2));
            _repository.Insert(new EntityImpl(3));

            _repository.Count().Should().Be(3);
            var array = _repository.GetAll().ToArray();
            array[0].Id.Should().Be(1);
            array[1].Id.Should().Be(2);
            array[2].Id.Should().Be(3);
        }

        [Fact]
        public void GetById_Should()
        {
            const int id = 54;

            _repository.Insert(new EntityImpl(23));
            _repository.Insert(new EntityImpl(id));
            _repository.Insert(new EntityImpl(1));

            var entity = _repository.GetById(id);

            entity.Should().NotBeNull();
            entity.Id.Should().Be(id);
        }

        [Fact]
        public void Update_ShouldReplaceEntityInMemory()
        {
            _repository.Insert(new EntityImpl(1));
            var newEntity = new EntityImpl(1);

            _repository.Count().Should().Be(1);
            _repository.Update(newEntity);
            _repository.Count().Should().Be(1);
            _repository.GetAll().First().Value.Should().Be(newEntity.Value);
        }

        [Fact]
        public void Delete_DeleteEntityShouldRemoveEntityFromMemory()
        {
            var entity = new EntityImpl(1);

            _repository.Insert(entity);
            _repository.Delete(entity);
            _repository.Count().Should().Be(0);
        }

        [Fact]
        public void Delete_DeleteNotAddedEntityShouldDoNothing()
        {
            var entity = new EntityImpl(1);
            var entity2 = new EntityImpl(2);

            _repository.Insert(entity);
            _repository.Delete(entity2);
            _repository.Count().Should().Be(1);
        }

        [Fact]
        public void Update_UpdateNullShouldThrowException()
        {
            Assert.Throws<ArgumentNullException>(() => _repository.Update((EntityImpl)null));
        }

        [Fact]
        public void Update_UpdateTransientEntityShouldThrowException()
        {
            var entity = new EntityImpl();

            Assert.Throws<ArgumentException>(() => _repository.Update(entity));
        }

        [Fact]
        public void Update_UpdateEntityWithIdThatIsNotAddedShouldThrowException()
        {
            var entity = new EntityImpl(1);

            Assert.Throws<KeyNotFoundException>(() => _repository.Update(entity));
        }

        [Fact]
        public void Delete_DeleteNullShouldThrowException()
        {
            Assert.Throws<ArgumentNullException>(() => _repository.Delete((EntityImpl)null));
        }

        [Fact]
        public void Delete_DeleteTransientEntity1ShouldThrowException()
        {
            var entity = new EntityImpl();

            Assert.Throws<ArgumentException>(() => _repository.Delete(entity));
        }
    }
}