using FluentAssertions;
using Mariowski.Common.DataTypes;
using Xunit;

namespace Mariowski.Common.Tests.DataTypes
{
    public partial class TimeStringTests
    {
        [Theory]
        [InlineData("1h30m", 5400000)]
        [InlineData("12h 45m", 45900000)]
        [InlineData("1 h 4 m", 3840000)]
        [InlineData("1d 12h 34m 20s", 131660000)]
        public void Ctor_ShouldAcceptValidTimeString(string value, double expected)
        {
            var timeString = new TimeString(value);

            timeString.TimeSpan.TotalMilliseconds.Should().Be(expected);
        }
    }
}