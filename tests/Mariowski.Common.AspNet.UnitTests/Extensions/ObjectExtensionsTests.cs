using System.Threading.Tasks;
using FluentAssertions;
using Mariowski.Common.AspNet.Extensions;
using Xunit;

namespace Mariowski.Common.AspNet.UnitTests.Extensions
{
    public class ObjectExtensionsTests
    {
        [Fact]
        public async Task ToJsonContent_ShouldConvertObjectToStringContent()
        {
            var obj = new { Test = "test" };

            var content = obj.ToJsonContent();

            content.Should().NotBeNull();
            string contentAsString = await content.ReadAsStringAsync();
            contentAsString.Should().Be("{\"Test\":\"test\"}");
        }
    }
}