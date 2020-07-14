using FluentAssertions;
using Mariowski.Common.DataTypes;
using Xunit;

namespace Mariowski.Common.UnitTests.DataTypes
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
        
        [Theory]
        [InlineData("1h30m", "1h30m", true)]
        [InlineData("1h30m", "1h31m", false)]
        public void Equals_ShouldDetermineEqualityWithOtherTimeString(string value, string value2, bool expected)
        {
            var timeString = new TimeString(value);
            var timeString2 = new TimeString(value2);

            bool areEqual = timeString.Equals(timeString2);

            areEqual.Should().Be(expected);
        }

        [Theory]
        [InlineData("1h30m", "1h30m", true)]
        [InlineData("1h30m", "1h31m", false)]
        public void Equals_ShouldDetermineEqualityWithOtherObject(string value, string value2, bool expected)
        {
            var timeString = new TimeString(value);
            var timeString2 = (object)new TimeString(value2);

            bool areEqual = timeString.Equals(timeString2);

            areEqual.Should().Be(expected);
        }
        
        [Theory]
        [InlineData("1h30m", "1h30m")]
        [InlineData("12h 45m", "12h 45m")]
        [InlineData("1 h 4 m", "1 h 4 m")]
        [InlineData("1d 12h 34m 20s", "1d 12h 34m 20s")]
        public void ToString_ShouldReturnTimeStringAsString(string value, string expected)
        {
            var timeString = new TimeString(value);

            var toString = timeString.ToString();

            toString.Should().Be(expected);
        }

        [Theory]
        [InlineData("1h30m", -1834574836)]
        [InlineData("12h 45m", -561500566)]
        [InlineData("1 h 4 m", -254705656)]
        [InlineData("1d 12h 34m 20s", -1954959566)]
        public void GetHashCode_ShouldReturnCalculatedHashCode(string value, int expected)
        {
            var timeString = new TimeString(value);

            int hashCode = timeString.GetHashCode();

            hashCode.Should().Be(expected);
        }
    }
}