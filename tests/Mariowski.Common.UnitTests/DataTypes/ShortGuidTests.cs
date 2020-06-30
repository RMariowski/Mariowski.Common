using FluentAssertions;
using Mariowski.Common.DataTypes;
using System;
using System.Collections.Generic;
using Xunit;

// ReSharper disable EqualExpressionComparison
// ReSharper disable SuspiciousTypeConversion.Global

namespace Mariowski.Common.UnitTests.DataTypes
{
    public class ShortGuidTests
    {
        [Fact]
        public void Empty_ShouldBeTheSameAsDefaultValue()
        {
            ShortGuid defaultGuid = default;

            (ShortGuid.Empty.Guid == defaultGuid.Guid).Should().BeTrue();
            (ShortGuid.Empty.Value == defaultGuid.Value).Should().BeTrue();
            (ShortGuid.Empty == defaultGuid).Should().BeTrue();
        }

        [Fact]
        public void Empty_ShouldBeDifferentFromGeneratedShortGuid()
        {
            var shortGuid = ShortGuid.NewShortGuid();

            (ShortGuid.Empty.Guid != shortGuid.Guid).Should().BeTrue();
            (ShortGuid.Empty.Value != shortGuid.Value).Should().BeTrue();
            (ShortGuid.Empty != shortGuid).Should().BeTrue();
        }

        [Fact]
        public void Equals_ShouldCorrectlyDetermineEqualityWithOtherObject()
        {
            var guid = new Guid("9a717c34-ed19-4446-8752-ccd020ed9638");
            var shortGuid = new ShortGuid(guid);
            var shortGuid2 = new ShortGuid(guid);

            shortGuid.Equals(shortGuid).Should().BeTrue();
            shortGuid.Equals(guid).Should().BeTrue();
            shortGuid.Equals(123).Should().BeFalse();
            shortGuid.Equals(shortGuid2).Should().BeTrue();
            shortGuid.Equals(shortGuid2.Value).Should().BeTrue();
        }

        [Fact]
        public void GetHashCode_ShouldReturnTheSameHashCodeAsGuid()
        {
            var shortGuid = ShortGuid.NewShortGuid();

            (shortGuid.GetHashCode() == shortGuid.Guid.GetHashCode()).Should().BeTrue();
        }

        [Fact]
        public void NewShortGuid_ShouldReturnUniqueShortGuids()
        {
            const int guidsToGenerate = 10000;
            var shortGuids = new List<ShortGuid>(guidsToGenerate);

            for (int i = 0; i < guidsToGenerate; i++)
            {
                var shortGuid = ShortGuid.NewShortGuid();
                shortGuids.Exists(g => g == shortGuid).Should().BeFalse();
                shortGuids.Add(shortGuid);
            }
        }

        [Fact]
        public void EqualityOperator_ShouldCorrectlyDetermineEquality()
        {
            var shortGuid = ShortGuid.NewShortGuid();
            var shortGuidCopy = shortGuid;
            var shortGuid2 = ShortGuid.NewShortGuid();

            (shortGuid.Guid == shortGuidCopy.Guid).Should().BeTrue();
            (shortGuid.Value == shortGuidCopy.Value).Should().BeTrue();
            (shortGuid == shortGuidCopy).Should().BeTrue();
            (shortGuid.Guid == shortGuid2.Guid).Should().BeFalse();
            (shortGuid.Value == shortGuid2.Value).Should().BeFalse();
            (shortGuid == shortGuid2).Should().BeFalse();
        }

        [Fact]
        public void InequalityOperator_ShouldCorrectlyDetermineInequality()
        {
            var shortGuid = ShortGuid.NewShortGuid();
            var shortGuidCopy = shortGuid;
            var shortGuid2 = ShortGuid.NewShortGuid();

            (shortGuid.Guid != shortGuid2.Guid).Should().BeTrue();
            (shortGuid.Value != shortGuid2.Value).Should().BeTrue();
            (shortGuid != shortGuid2).Should().BeTrue();
            (shortGuid.Guid != shortGuidCopy.Guid).Should().BeFalse();
            (shortGuid.Value != shortGuidCopy.Value).Should().BeFalse();
            (shortGuid != shortGuidCopy).Should().BeFalse();
        }

        [Fact]
        public void ToString_ShouldReturnValueOfShortGuid()
        {
            var shortGuid = ShortGuid.NewShortGuid();

            (shortGuid.ToString() == shortGuid.Value).Should().BeTrue();
        }

        [Fact]
        public void ShortGuidOperator_ShouldImplicitlyConvertsGuidToShortGuid()
        {
            ShortGuid shortGuid = Guid.NewGuid();

            shortGuid.Should().NotBeNull().And.NotBeSameAs(ShortGuid.Empty);
        }

        [Fact]
        public void ShortGuidOperator_ShouldImplicitlyConvertsStringToShortGuid()
        {
            ShortGuid shortGuid = "eRy4yaDYjkuYob4YnHKtnA";

            shortGuid.Should().NotBeNull().And.NotBeSameAs(ShortGuid.Empty);
        }

        [Fact]
        public void GuidOperator_ShouldImplicitlyConvertsShortGuidToGuid()
        {
            Guid guid = new ShortGuid("eRy4yaDYjkuYob4YnHKtnA");

            guid.Should().NotBeEmpty();
        }

        [Fact]
        public void StringOperator_ShouldImplicitlyConvertsShortGuidToString()
        {
            string value = new ShortGuid(new Guid("9a717c34-ed19-4446-8752-ccd020ed9638"));

            value.Should().NotBeNullOrWhiteSpace();
        }
    }
}