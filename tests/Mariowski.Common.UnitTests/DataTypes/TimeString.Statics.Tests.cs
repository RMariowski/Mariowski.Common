using System;
using FluentAssertions;
using Mariowski.Common.DataTypes;
using Xunit;

namespace Mariowski.Common.UnitTests.DataTypes
{
    public partial class TimeStringTests
    {
        [Theory]
        [InlineData("1d", 86400000)]
        [InlineData("1day", 86400000)]
        [InlineData("1days", 86400000)]
        [InlineData("1h", 3600000)]
        [InlineData("1hour", 3600000)]
        [InlineData("1hours", 3600000)]
        [InlineData("1s", 1000)]
        [InlineData("1sec", 1000)]
        [InlineData("1secs", 1000)]
        [InlineData("1m", 60000)]
        [InlineData("1min", 60000)]
        [InlineData("1mins", 60000)]
        [InlineData("1ms", 1)]
        [InlineData("1", 1)]
        public void Parse_ShouldConvertAllTimeUnitsToTimeSpan(string timeUnit, double expected)
        {
            var timeSpan = TimeString.Parse(timeUnit);

            timeSpan.TotalMilliseconds.Should().Be(expected);
        }

        [Theory]
        [InlineData("1h30m", 5400000)]
        [InlineData("12h 45m", 45900000)]
        [InlineData("1 h 4 m", 3840000)]
        [InlineData("1d 12h 34m 20s", 131660000)]
        [InlineData("80h", 288000000)]
        [InlineData("3000ms", 3000)]
        [InlineData("20mins", 1200000)]
        [InlineData("3days 20hours 36mins 17secs 156ms", 333377156)]
        public void Parse_ShouldConvertTimeStringToTimeSpan(string timeString, double expected)
        {
            var timeSpan = TimeString.Parse(timeString);

            timeSpan.TotalMilliseconds.Should().Be(expected);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Parse_ShouldThrowArgumentException_WhenTimeStringIsNullOrWhiteSpace(string timeString)
        {
            void Act() => _ = TimeString.Parse(timeString);

            Assert.Throws<ArgumentException>(Act);
        }

        [Theory]
        [InlineData("1test")]
        [InlineData("day")]
        [InlineData("huor")]
        [InlineData("ms 10")]
        public void Parse_ShouldThrowFormatException_WhenHasUnknownFormat(string timeString)
        {
            void Act() => _ = TimeString.Parse(timeString);

            Assert.Throws<FormatException>(Act);
        }

        [Theory]
        [InlineData("1h30m", "1h30m", true)]
        [InlineData("1h30m", "1h31m", false)]
        public void EqualityOperator_ShouldDetermineEqualityWithOtherTimeString(string value, string value2,
            bool expected)
        {
            var timeString = new TimeString(value);
            var timeString2 = new TimeString(value2);

            bool areEqual = timeString == timeString2;

            areEqual.Should().Be(expected);
        }

        [Theory]
        [InlineData("1h30m", "1h30m", false)]
        [InlineData("1h30m", "1h31m", true)]
        public void InequalityOperator_ShouldDetermineEqualityWithOtherTimeString(string value, string value2,
            bool expected)
        {
            var timeString = new TimeString(value);
            var timeString2 = new TimeString(value2);

            bool areEqual = timeString != timeString2;

            areEqual.Should().Be(expected);
        }
    }
}