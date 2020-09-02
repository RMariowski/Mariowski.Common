using FluentAssertions;
using Xunit;

namespace Mariowski.Common.UnitTests.DataTypes
{
    public class ValueObjectTests
    {
        [Theory]
        [InlineData("My Test", "My Test", true)]
        [InlineData("Test", "My Test", false)]
        public void Equals_ShouldDetermineEqualityWithOtherObject(string value, string value2, bool expected)
        {
            var obj = new VObj(value);
            var obj2 = (object)new VObj(value2);

            bool areEqual = obj.Equals(obj2);

            areEqual.Should().Be(expected);
        }

        [Theory]
        [InlineData("My Test", "My Test", true)]
        [InlineData("Test", "My Test", false)]
        [InlineData("My Test", "Test", false)]
        public void EqualityOperator_ShouldDetermineEquality(string value, string value2, bool expected)
        {
            var obj = new VObj(value);
            var obj2 = new VObj(value2);

            bool areEqual = obj == obj2;

            areEqual.Should().Be(expected);
        }

        [Theory]
        [InlineData("My Test", "My Test", false)]
        [InlineData("Test", "My Test", true)]
        [InlineData("My Test", "Test", true)]
        public void InequalityOperator_ShouldDetermineInequality(string value, string value2, bool expected)
        {
            var obj = new VObj(value);
            var obj2 = new VObj(value2);

            bool areEqual = obj != obj2;

            areEqual.Should().Be(expected);
        }
    }
}