using System;
using System.Collections.Generic;
using FluentAssertions;
using Mariowski.Common.DataTypes;
using Xunit;

namespace Mariowski.Common.UnitTests.DataTypes
{
    public partial class ShortGuidTests
    {
        [Fact]
        public void Empty_ShouldBeTheSameAsShortGuidCreatedWithGuidEmpty()
        {
            var createdEmptyGuid = new ShortGuid(Guid.Empty);

            (ShortGuid.Empty.Guid == createdEmptyGuid.Guid).Should().BeTrue();
            (ShortGuid.Empty == createdEmptyGuid).Should().BeTrue();
        }

        [Fact]
        public void NewShortGuid_ShouldCreateUniqueShortGuids()
        {
            const int shortGuidsToGenerate = 10_000;
            var shortGuids = new List<ShortGuid>(shortGuidsToGenerate) { ShortGuid.Empty };

            for (int i = 0; i < shortGuidsToGenerate; i++)
            {
                var shortGuid = ShortGuid.NewShortGuid();

                shortGuids.Should().NotContain(shortGuid);

                shortGuids.Add(shortGuid);
            }
        }

        [Theory]
        [InlineData("087cd284-4e96-42bb-870c-8f9177ed8246", "hNJ8CJZOu0KHDI-Rd-2CRg")]
        [InlineData("2d5b5b6b-e3a8-462f-89d2-a0d3244a8703", "a1tbLajjL0aJ0qDTJEqHAw")]
        [InlineData("f026dc2a-5fce-4e74-808e-2ab2575e83a9", "Ktwm8M5fdE6AjiqyV16DqQ")]
        [InlineData("626be544-b7fd-45e6-9b8d-9dc5386947d0", "ROVrYv235kWbjZ3FOGlH0A")]
        [InlineData("55ff0274-e7e9-4181-b24f-8f77f4e5d9eb", "dAL_VenngUGyT4939OXZ6w")]
        public void Encode_ShouldEncodeGuid(Guid guid, string expected)
        {
            string shortGuid = ShortGuid.Encode(guid);

            shortGuid.Should().Be(expected);
        }

        [Theory]
        [InlineData("hNJ8CJZOu0KHDI-Rd-2CRg", "087cd284-4e96-42bb-870c-8f9177ed8246")]
        [InlineData("a1tbLajjL0aJ0qDTJEqHAw", "2d5b5b6b-e3a8-462f-89d2-a0d3244a8703")]
        [InlineData("Ktwm8M5fdE6AjiqyV16DqQ", "f026dc2a-5fce-4e74-808e-2ab2575e83a9")]
        [InlineData("ROVrYv235kWbjZ3FOGlH0A", "626be544-b7fd-45e6-9b8d-9dc5386947d0")]
        [InlineData("dAL_VenngUGyT4939OXZ6w", "55ff0274-e7e9-4181-b24f-8f77f4e5d9eb")]
        public void Decode_ShouldDecodeStringFormOfShortGuid(string encoded, Guid expected)
        {
            var guid = ShortGuid.Decode(encoded);

            guid.Should().Be(expected);
        }

        [Theory]
        [InlineData("hNJ8CJZOu0KHDI-Rd-2CRg", "hNJ8CJZOu0KHDI-Rd-2CRg", true)]
        [InlineData("hNJ8CJZOu0KHDI-Rd-2CRg", "a1tbLajjL0aJ0qDTJEqHAw", false)]
        public void EqualityOperator_ShouldDetermineEqualityWithOtherShortGuid(string encoded,
            string encoded2, bool expected)
        {
            var shortGuid = new ShortGuid(encoded);
            var shortGuid2 = new ShortGuid(encoded2);

            bool areEqual = shortGuid == shortGuid2;

            areEqual.Should().Be(expected);
        }

        [Theory]
        [InlineData("hNJ8CJZOu0KHDI-Rd-2CRg", "hNJ8CJZOu0KHDI-Rd-2CRg", false)]
        [InlineData("hNJ8CJZOu0KHDI-Rd-2CRg", "a1tbLajjL0aJ0qDTJEqHAw", true)]
        public void InequalityOperator_ShouldDetermineInequalityWithOtherShortGuid(string encoded,
            string encoded2, bool expected)
        {
            var shortGuid = new ShortGuid(encoded);
            var shortGuid2 = new ShortGuid(encoded2);

            bool areEqual = shortGuid != shortGuid2;

            areEqual.Should().Be(expected);
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
    }
}