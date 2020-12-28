using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Mariowski.Common.EntityFramework.Tests
{
    public class EfGenericRepositoryTests
    {
        [Fact]
        public async Task InsertAndGetIdAsync_ShouldReturnIdOfInsertedEntity()
        {
            await using var context = new TestContext();
            var dummyRepository = new DummyRepository(context);

            var entity = await dummyRepository.InsertAsync(new DummyEntity { Foo = "Insert" });

            entity.Id.Should().Be(1);
        }

        [Fact]
        public async Task Should_delete_updated_entity()
        {
            await using var context = new TestContext();
            var dummyRepository = new DummyRepository(context);

            await dummyRepository.InsertAsync(new DummyEntity { Foo = "Insert" });
            await context.SaveChangesAsync();

            var entity = await dummyRepository.SingleAsync(dummy => dummy.Foo == "Insert");
            entity.Foo = "Update";
            await dummyRepository.UpdateAsync(entity);
            await context.SaveChangesAsync();

            entity = await dummyRepository.SingleAsync(dummy => dummy.Foo == "Update");
            await dummyRepository.DeleteAsync(entity);
            await context.SaveChangesAsync();

            bool deleted = !await dummyRepository.AnyAsync(x => true);
            deleted.Should().BeTrue();
        }

        [Fact]
        public async Task Should_update_entity_twice()
        {
            await using var context = new TestContext();
            var dummyRepository = new DummyRepository(context);

            await dummyRepository.InsertAsync(new DummyEntity { Foo = "Insert" });
            await context.SaveChangesAsync();

            var entity = await dummyRepository.SingleAsync(dummy => dummy.Foo == "Insert");
            entity.Foo = "Update1";
            await dummyRepository.UpdateAsync(entity);
            await context.SaveChangesAsync();

            entity = await dummyRepository.SingleAsync(dummy => dummy.Foo == "Update1");
            entity.Foo = "Update2";
            await dummyRepository.UpdateAsync(entity);
            await context.SaveChangesAsync();

            entity = await dummyRepository.SingleAsync(dummy => dummy.Foo == "Update2");
            entity.Should().NotBeNull();
        }
    }
}