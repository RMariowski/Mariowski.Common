using System;
using FluentAssertions;
using Mariowski.Common.Extensions;
using Xunit;

namespace Mariowski.Common.UnitTests.Extensions
{
    public class GuidExtensionsTests
    {
        [Theory]
        [InlineData("087cd284-4e96-42bb-870c-8f9177ed8246", "hNJ8CJZOu0KHDI-Rd-2CRg")]
        [InlineData("2d5b5b6b-e3a8-462f-89d2-a0d3244a8703", "a1tbLajjL0aJ0qDTJEqHAw")]
        [InlineData("f026dc2a-5fce-4e74-808e-2ab2575e83a9", "Ktwm8M5fdE6AjiqyV16DqQ")]
        [InlineData("626be544-b7fd-45e6-9b8d-9dc5386947d0", "ROVrYv235kWbjZ3FOGlH0A")]
        [InlineData("55ff0274-e7e9-4181-b24f-8f77f4e5d9eb", "dAL_VenngUGyT4939OXZ6w")]
        public void EncodeBase64String_ShouldEncodeGuidToBase64String(Guid guid, string expected)
        {
            string base64String = guid.EncodeBase64String();

            base64String.Should().Be(expected);
        }
    }
}