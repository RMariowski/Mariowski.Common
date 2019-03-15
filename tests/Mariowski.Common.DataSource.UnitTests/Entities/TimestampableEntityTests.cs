using FluentAssertions;
using Mariowski.Common.DataSource.Entities;
using System;
using Xunit;

namespace Mariowski.Common.DataSource.UnitTests.Entities
{
    public class TimestampableEntityTests
    {
        private class ClassToTest : TimestampableEntity<int>
        {
            public ClassToTest() { }
            public ClassToTest(DateTime createdAt, DateTime? updatedAt = null) : base(createdAt, updatedAt) { }
        }
        
        [Fact]
        public void TimestampableEntity_Ctor_CreatingInstanceWithoutArgumentsShouldHaveCorrectDates()
        {
            var entity = new ClassToTest();
            entity.CreatedAt.Should().BeIn(DateTimeKind.Utc).And.BeCloseTo(DateTime.UtcNow, 500);
            entity.UpdatedAt.Should().BeSameDateAs(entity.CreatedAt);
        }

        [Fact]
        public void TimestampableEntity_Ctor_CreatingInstanceWithArgumentsShouldHaveCorrectDates()
        {
            var createdAt = DateTime.UtcNow;
            var entity = new ClassToTest(createdAt);
            entity.CreatedAt.Should().BeIn(DateTimeKind.Utc).And.BeCloseTo(DateTime.UtcNow, 500);
            entity.UpdatedAt.Should().BeSameDateAs(entity.CreatedAt);

            var updatedAt = DateTime.UtcNow.AddDays(1);
            entity = new ClassToTest(createdAt, updatedAt);
            entity.CreatedAt.Should().BeIn(DateTimeKind.Utc).And.BeCloseTo(DateTime.UtcNow, 500);
            entity.UpdatedAt.Should().NotBeSameDateAs(entity.CreatedAt);
            entity.UpdatedAt.Should().BeIn(DateTimeKind.Utc).And.BeCloseTo(DateTime.UtcNow.AddDays(1), 500);
        }
    }
}