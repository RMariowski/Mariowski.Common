using System;
using FluentAssertions;
using Mariowski.Common.Extensions;
using Xunit;

namespace Mariowski.Common.UnitTests.Extensions
{
    public class DateTimeExtensionsTests
    {
        [Theory]
        [InlineData(1970, 1, 1, 0, 0, 0, 0)]
        [InlineData(2000, 4, 5, 6, 7, 8, 954914828)]
        [InlineData(2019, 3, 3, 18, 19, 45, 1551637185)]
        public void DateTimeExtensions_ToUnixTimestamp_ShouldBeEqualToExpected(
            int year, int month, int day, int hour, int minute, int second, int expected)
        {
            var dateTime = new DateTime(year, month, day, hour, minute, second, DateTimeKind.Utc);

            dateTime.ToUnixTimestamp().Should().Be(expected, "https://www.unixtimestamp.com/ told me that");
        }
    }
}