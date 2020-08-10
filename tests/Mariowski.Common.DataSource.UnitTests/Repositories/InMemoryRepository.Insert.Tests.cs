using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Mariowski.Common.DataSource.UnitTests.Implementations;
using Xunit;

namespace Mariowski.Common.DataSource.UnitTests.Repositories
{
    public partial class InMemoryRepositoryTests
    {
        [Fact]
        public void Insert_ShouldAddEntity()
        {
            var entity = new EntityImpl(1);

            _repository.Insert(entity);

            _repository.GetAll().Any(e => e.Id == entity.Id && e.Value == entity.Value).Should().BeTrue();
        }

        [Fact]
        public void Insert_ShouldThrowArgumentNullException_WhenEntityIsNull()
        {
            EntityImpl entity = null;

            void Act() => _repository.Insert(entity);

            Assert.Throws<ArgumentNullException>(Act);
        }

        [Fact]
        public void Insert_ShouldThrowArgumentException_WhenEntityIsTransient()
        {
            var entity = new EntityImpl();

            void Act() => _repository.Insert(entity);

            Assert.Throws<ArgumentException>(Act);
        }

        [Fact]
        public void Insert_ShouldThrowInvalidOperationException_WhenEntityAlreadyExists()
        {
            const int id = 1;
            var entity = new EntityImpl(id);
            var entity2 = new EntityImpl(id);
            _repository.Insert(entity);

            void Act() => _repository.Insert(entity2);

            Assert.Throws<InvalidOperationException>(Act);
        }

        [Fact]
        public async Task InsertAsync_ShouldAddEntity()
        {
            var entity = new EntityImpl(1);

            await _repository.InsertAsync(entity);

            _repository.GetAll().Any(e => e.Id == entity.Id && e.Value == entity.Value).Should().BeTrue();
        }

        [Fact]
        public async Task InsertAsync_ShouldThrowArgumentNullException_WhenEntityIsNull()
        {
            EntityImpl entity = null;

            Task Act() => _repository.InsertAsync(entity);

            await Assert.ThrowsAsync<ArgumentNullException>(Act);
        }

        [Fact]
        public async Task InsertAsync_ShouldThrowArgumentException_WhenEntityIsTransient()
        {
            var entity = new EntityImpl();

            Task Act() => _repository.InsertAsync(entity);

            await Assert.ThrowsAsync<ArgumentException>(Act);
        }

        [Fact]
        public async Task InsertAsync_ShouldThrowInvalidOperationException_WhenEntityAlreadyExists()
        {
            const int id = 1;
            var entity = new EntityImpl(id);
            var entity2 = new EntityImpl(id);
            _repository.Insert(entity);

            Task Act() => _repository.InsertAsync(entity2);

            await Assert.ThrowsAsync<InvalidOperationException>(Act);
        }

        [Fact]
        public void Insert_ShouldAddEntities()
        {
            var entity = new EntityImpl(1);
            var entity2 = new EntityImpl(2);

            _repository.Insert(new[] { entity, entity2 });

            _repository.GetAll().Any(e => e.Id == entity.Id && e.Value == entity.Value).Should().BeTrue();
            _repository.GetAll().Any(e => e.Id == entity2.Id && e.Value == entity2.Value).Should().BeTrue();
        }

        [Fact]
        public async Task InsertAsync_ShouldAddEntities()
        {
            var entity = new EntityImpl(1);
            var entity2 = new EntityImpl(2);

            await _repository.InsertAsync(new[] { entity, entity2 });

            _repository.GetAll().Any(e => e.Id == entity.Id && e.Value == entity.Value).Should().BeTrue();
            _repository.GetAll().Any(e => e.Id == entity2.Id && e.Value == entity2.Value).Should().BeTrue();
        }
    }
}