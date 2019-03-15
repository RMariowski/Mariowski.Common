using FluentAssertions;
using Mariowski.Common.DataSource.Entities;
using Xunit;

// ReSharper disable ConditionIsAlwaysTrueOrFalse
// ReSharper disable EqualExpressionComparison
// ReSharper disable SuspiciousTypeConversion.Global

namespace Mariowski.Common.DataSource.UnitTests.Entities
{
    public class EntityTests
    {
        private class ClassToTest : Entity<int> { }

        [Fact]
        public void Entity_IsTransient_EntityWithoutIdShouldBeTransient()
        {
            var entity = new ClassToTest();
            entity.IsTransient().Should().BeTrue();
        }

        [Fact]
        public void Entity_IsTransient_EntityWithDefaultIdShouldBeTransient()
        {
            var entity = new ClassToTest { Id = 0 };
            entity.IsTransient().Should().BeTrue();
        }

        [Fact]
        public void Entity_IsTransient_EntityWithCorrectIdShouldNotBeTransient()
        {
            var entity = new ClassToTest { Id = 1 };
            entity.IsTransient().Should().BeFalse();
        }

        [Fact]
        public void Entity_Equals_ShouldCorrectlyDetermineEqualityWithOtherObject()
        {
            var entity = new ClassToTest();
            entity.Equals(entity).Should().BeTrue();
            entity.Equals(null).Should().BeFalse();
            entity.Equals("").Should().BeFalse();
            entity.Equals(123).Should().BeFalse();

            var entity2 = new ClassToTest();
            entity.Equals(entity2).Should().BeFalse();

            entity.Id = entity2.Id = 1;
            entity.Equals(entity2).Should().BeTrue();
        }

        [Fact]
        public void Entity_GetHashCode_ShouldReturnTheSameHashCodeAsId()
        {
            var entity = new ClassToTest { Id = 1 };
            (entity.Id.GetHashCode() == 1.GetHashCode()).Should().BeTrue();
        }

        [Fact]
        public void Entity_EqualityOperator_ShouldCorrectlyDetermineEquality()
        {
            var entity = new ClassToTest();
            (entity == entity).Should().BeTrue();
            (entity == null).Should().BeFalse();

            var entity2 = new ClassToTest();
            (entity == entity2).Should().BeFalse();

            entity.Id = entity2.Id = 1;
            (entity == entity2).Should().BeTrue();
        }

        [Fact]
        public void Entity_InequalityOperator_ShouldCorrectlyDetermineInequality()
        {
            var entity = new ClassToTest();
            (entity != entity).Should().BeFalse();
            (entity != null).Should().BeTrue();

            var entity2 = new ClassToTest();
            (entity != entity2).Should().BeTrue();

            entity.Id = entity2.Id = 1;
            (entity != entity2).Should().BeFalse();
        }

        [Fact]
        public void Entity_ToString_ShouldReturnStringInSpecifiedFormat()
        {
            var entity = new ClassToTest();
            entity.ToString().Should().Be($"[{typeof(ClassToTest).Name} {entity.Id}]");
        }
    }
}