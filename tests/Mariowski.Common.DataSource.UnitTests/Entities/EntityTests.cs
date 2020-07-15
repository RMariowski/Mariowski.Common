﻿using FluentAssertions;
using Mariowski.Common.DataSource.UnitTests.Implementations;
using Xunit;

namespace Mariowski.Common.DataSource.UnitTests.Entities
{
    public class EntityTests
    {
        [Fact]
        public void IsTransient_EntityWithoutIdShouldBeTransient()
        {
            var entity = new EntityImpl();

            entity.IsTransient().Should().BeTrue();
        }

        [Fact]
        public void IsTransient_EntityWithDefaultIdShouldBeTransient()
        {
            var entity = new EntityImpl(0);

            entity.IsTransient().Should().BeTrue();
        }

        [Fact]
        public void IsTransient_EntityWithCorrectIdShouldNotBeTransient()
        {
            var entity = new EntityImpl(1);

            entity.IsTransient().Should().BeFalse();
        }

        [Fact]
        public void Equals_ShouldDetermineEqualityWithOtherObject()
        {
            var entity = new EntityImpl();
            entity.Equals(entity).Should().BeTrue();
            entity.Equals(null).Should().BeFalse();
            entity.Equals("").Should().BeFalse();
            entity.Equals(123).Should().BeFalse();

            var entity2 = new EntityImpl();
            entity.Equals(entity2).Should().BeFalse();

            entity.Id = entity2.Id = 1;
            entity.Equals(entity2).Should().BeTrue();
        }

        [Fact]
        public void GetHashCode_ShouldReturnTheSameHashCodeAsId()
        {
            var entity = new EntityImpl(1);

            (entity.Id.GetHashCode() == 1.GetHashCode()).Should().BeTrue();
        }

        [Fact]
        public void EqualityOperator_ShouldDetermineEquality()
        {
            var entity = new EntityImpl();
            (entity is null).Should().BeFalse();

            var entity2 = entity;
            (entity == entity2).Should().BeTrue();

            entity2 = new EntityImpl();
            (entity == entity2).Should().BeFalse();

            entity.Id = entity2.Id = 1;
            (entity == entity2).Should().BeTrue();
        }

        [Fact]
        public void Entity_InequalityOperator_ShouldDetermineInequality()
        {
            var entity = new EntityImpl();
            (entity != null).Should().BeTrue();

            var entity2 = entity;
            (entity != entity2).Should().BeFalse();

            entity2 = new EntityImpl();
            (entity != entity2).Should().BeTrue();

            entity.Id = entity2.Id = 1;
            (entity != entity2).Should().BeFalse();
        }

        [Fact]
        public void ToString_ShouldReturnStringInSpecifiedFormat()
        {
            var entity = new EntityImpl();

            entity.ToString().Should().Be($"[{nameof(EntityImpl)} {entity.Id}]");
        }
    }
}