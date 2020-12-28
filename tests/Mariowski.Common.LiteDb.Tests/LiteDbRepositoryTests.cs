using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Mariowski.Common.LiteDb.Tests.Dummy;
using Xunit;

namespace Mariowski.Common.LiteDb.Tests
{
    [Collection(DatabaseFixture.CollectionName)]
    public class LiteDbRepositoryTests
    {
        private readonly DummyRepository _repository;

        public LiteDbRepositoryTests(DatabaseFixture fixture)
        {
            _repository = new DummyRepository(fixture.TestDbContext);
        }

        [Fact]
        public void Should_insert_single_entity()
        {
            int count = _repository.Count();
            var entity = new DummyEntity();

            _repository.Insert(entity);

            _repository.Count().Should().Be(count + 1);
        }

        [Fact]
        public void Should_insert_multiple_entities()
        {
            int count = _repository.Count();
            var entities = new[] { new DummyEntity(), new DummyEntity(), new DummyEntity() };

            _repository.Insert(entities);

            _repository.Count().Should().Be(count + entities.Length);
        }

        [Fact]
        public void Should_insert_entity_when_entity_is_transient()
        {
            var entity = new DummyEntity();
            int count = _repository.Count();

            _repository.InsertOrUpdate(entity);

            _repository.Count().Should().Be(count + 1);
        }

        [Fact]
        public void Should_insert_entity_when_entity_is_not_transient_and_not_exists_in_repository()
        {
            int count = _repository.Count();
            var entity = new DummyEntity { Id = 999 };

            _repository.InsertOrUpdate(entity);

            _repository.Count().Should().Be(count + 1);
        }

        [Fact]
        public void Should_update_entity_when_entity_is_not_transient_and_exists_in_repository()
        {
            const int id = 888;
            const string expectedFoo = "Test";
            var entity = _repository.GetById(id);

            entity.Foo = expectedFoo;
            _repository.InsertOrUpdate(entity);

            entity = _repository.GetById(id);
            entity.Foo.Should().Be(expectedFoo);
        }

        [Fact]
        public void Should_return_all_entities()
        {
            int count = _repository.Count();

            var entities = _repository.GetAll().ToArray();

            entities.Should().HaveCount(count);
        }

        [Fact]
        public void Should_return_all_entities_with_includes()
        {
            int count = _repository.Count();

            var entities = _repository.GetAllIncluding(entity => entity.Sub).ToArray();

            entities.Should().HaveCount(count);
            entities.First(e => e.Id == 777).Sub.Bar.Should().NotBeEmpty();
        }

        [Fact]
        public void Should_return_entities_with_specified_ids()
        {
            const int id0 = 777;
            const int id1 = 888;

            var entities = _repository.GetByIds(new[] { id0, id1 });

            entities[0].Id.Should().Be(id0);
            entities[1].Id.Should().Be(id1);
        }

        [Fact]
        public void Should_return_empty_array_when_entities_are_not_found_by_ids()
        {
            var entities = _repository.GetByIds(new[] { 1234, 5678 });

            entities.Should().BeEmpty();
        }

        [Fact]
        public void Should_return_entity_with_specified_id()
        {
            const int id = 888;

            var entity = _repository.GetById(id);

            entity.Id.Should().Be(id);
        }

        [Fact]
        public void Should_throw_exception_when_entity_is_not_found()
        {
            const int id = 0;

            void Act() => _repository.GetById(id);

            Assert.Throws<KeyNotFoundException>(Act);
        }

        [Fact]
        public void Should_return_first_entity_with_specified_id()
        {
            const int id = 888;

            var entity = _repository.FirstOrDefaultById(id);

            entity.Id.Should().Be(id);
        }

        [Fact]
        public void Should_return_null_when_entity_is_not_found()
        {
            const int id = 0;

            var entity = _repository.FirstOrDefaultById(id);

            entity.Should().BeNull();
        }

        [Theory]
        [InlineData("Sub", true)]
        [InlineData("Bus", false)]
        public void Should_determine_whatever_entity_has_specified_string(string foo, bool expected)
        {
            bool any = _repository.Any(entity => entity.Foo == foo);

            any.Should().Be(expected);
        }

        [Fact]
        public void Should_update_entity()
        {
            const int id = 777;
            const string expectedFoo = "Test";
            var entity = _repository.GetById(id);

            entity.Foo = expectedFoo;
            _repository.Update(entity);

            entity = _repository.GetById(id);
            entity.Foo.Should().Be(expectedFoo);
        }

        [Fact]
        public void Should_delete_entity_by_its_id()
        {
            int count = _repository.Count();

            _repository.DeleteById(666);

            _repository.Count().Should().Be(count - 1);
        }

        [Fact]
        public void Should_return_entities_count_as_int()
        {
            int count = _repository.Count();

            count.Should().Be(3);
        }

        [Fact]
        public void Should_return_entities_count_as_long()
        {
            long count = _repository.LongCount();

            count.Should().Be(3);
        }
    }
}