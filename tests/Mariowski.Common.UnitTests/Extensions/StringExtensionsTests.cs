using System;
using FluentAssertions;
using Mariowski.Common.Extensions;
using Xunit;

namespace Mariowski.Common.UnitTests.Extensions
{
    public class StringExtensionsTests
    {
        [Theory]
        [InlineData("hNJ8CJZOu0KHDI-Rd-2CRg", "087cd284-4e96-42bb-870c-8f9177ed8246")]
        [InlineData("a1tbLajjL0aJ0qDTJEqHAw", "2d5b5b6b-e3a8-462f-89d2-a0d3244a8703")]
        [InlineData("Ktwm8M5fdE6AjiqyV16DqQ", "f026dc2a-5fce-4e74-808e-2ab2575e83a9")]
        [InlineData("ROVrYv235kWbjZ3FOGlH0A", "626be544-b7fd-45e6-9b8d-9dc5386947d0")]
        [InlineData("dAL_VenngUGyT4939OXZ6w", "55ff0274-e7e9-4181-b24f-8f77f4e5d9eb")]
        public void DecodeBase64ToGuid_ShouldDecodeBase64StringToGuid(string encoded, Guid expected)
        {
            var guid = encoded.DecodeBase64ToGuid();

            guid.Should().Be(expected);
        }
    }
}