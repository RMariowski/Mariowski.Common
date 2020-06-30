using FluentAssertions;
using System;
using Mariowski.Common.DataSource.UnitTests.Implementations;
using Xunit;

namespace Mariowski.Common.DataSource.UnitTests.Entities
{
    public class TimestampableEntityTests
    {
        [Fact]
        public void Ctor_CreatingInstanceWithoutArgumentsShouldHaveCorrectDates()
        {
            var entity = new TimestampableEntityImpl();

            entity.CreatedAt.Should().BeIn(DateTimeKind.Utc).And.BeCloseTo(DateTime.UtcNow, 500);
            entity.UpdatedAt.Should().BeSameDateAs(entity.CreatedAt);
        }

        [Fact]
        public void Ctor_CreatingInstanceWithArgumentsShouldHaveCorrectDates()
        {
            var createdAt = DateTime.UtcNow;
            var entity = new TimestampableEntityImpl(createdAt);
            entity.CreatedAt.Should().BeIn(DateTimeKind.Utc).And.BeCloseTo(DateTime.UtcNow, 500);
            entity.UpdatedAt.Should().BeSameDateAs(entity.CreatedAt);

            var updatedAt = DateTime.UtcNow.AddDays(1);
            entity = new TimestampableEntityImpl(createdAt, updatedAt);
            entity.CreatedAt.Should().BeIn(DateTimeKind.Utc).And.BeCloseTo(DateTime.UtcNow, 500);
            entity.UpdatedAt.Should().NotBeSameDateAs(entity.CreatedAt);
            entity.UpdatedAt.Should().BeIn(DateTimeKind.Utc).And.BeCloseTo(DateTime.UtcNow.AddDays(1), 500);
        }
    }
}