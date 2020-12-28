﻿using FluentAssertions;
using Mariowski.Common.DataTypes;
using System;
using Xunit;

namespace Mariowski.Common.Tests.DataTypes
{
    public partial class ShortGuidTests
    {
        [Theory]
        [InlineData("hNJ8CJZOu0KHDI-Rd-2CRg", "087cd284-4e96-42bb-870c-8f9177ed8246")]
        [InlineData("a1tbLajjL0aJ0qDTJEqHAw", "2d5b5b6b-e3a8-462f-89d2-a0d3244a8703")]
        [InlineData("Ktwm8M5fdE6AjiqyV16DqQ", "f026dc2a-5fce-4e74-808e-2ab2575e83a9")]
        [InlineData("ROVrYv235kWbjZ3FOGlH0A", "626be544-b7fd-45e6-9b8d-9dc5386947d0")]
        [InlineData("dAL_VenngUGyT4939OXZ6w", "55ff0274-e7e9-4181-b24f-8f77f4e5d9eb")]
        public void Ctor_ShouldAcceptAndDecodeStringFormOfShortGuid(string encoded, Guid expected)
        {
            var shortGuid = new ShortGuid(encoded);

            shortGuid.Guid.Should().Be(expected);
        }

        [Theory]
        [InlineData("087cd284-4e96-42bb-870c-8f9177ed8246", "hNJ8CJZOu0KHDI-Rd-2CRg")]
        [InlineData("2d5b5b6b-e3a8-462f-89d2-a0d3244a8703", "a1tbLajjL0aJ0qDTJEqHAw")]
        [InlineData("f026dc2a-5fce-4e74-808e-2ab2575e83a9", "Ktwm8M5fdE6AjiqyV16DqQ")]
        [InlineData("626be544-b7fd-45e6-9b8d-9dc5386947d0", "ROVrYv235kWbjZ3FOGlH0A")]
        [InlineData("55ff0274-e7e9-4181-b24f-8f77f4e5d9eb", "dAL_VenngUGyT4939OXZ6w")]
        public void Ctor_ShouldAcceptAndEncodeGuid(Guid guid, string expected)
        {
            var shortGuid = new ShortGuid(guid);

            shortGuid.Value.Should().Be(expected);
        }

        [Theory]
        [InlineData("hNJ8CJZOu0KHDI-Rd-2CRg", "hNJ8CJZOu0KHDI-Rd-2CRg", true)]
        [InlineData("hNJ8CJZOu0KHDI-Rd-2CRg", "a1tbLajjL0aJ0qDTJEqHAw", false)]
        public void Equals_ShouldDetermineEqualityWithOtherShortGuid(string encoded, string encoded2,
            bool expected)
        {
            var shortGuid = new ShortGuid(encoded);
            var shortGuid2 = new ShortGuid(encoded2);

            bool areEqual = shortGuid.Equals(shortGuid2);

            areEqual.Should().Be(expected);
        }
    }
}