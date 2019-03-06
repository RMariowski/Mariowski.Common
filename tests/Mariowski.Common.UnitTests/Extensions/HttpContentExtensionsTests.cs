using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Mariowski.Common.Extensions;
using Xunit;

namespace Mariowski.Common.UnitTests.Extensions
{
    public class HttpContentExtensionsTests
    {
        private struct StructToTest
        {
            public string FirstName;

            // ReSharper disable once UnusedAutoPropertyAccessor.Local
            public string LastName { get; set; }
        }

        [Fact]
        public async Task HttpContentExtensions_ReadAsAsync_ShouldReadStringContentAndDeserializeIt()
        {
            const string json = "{\"FirstName\":\"Radosław\",\"LastName\":\"Mariowski\"}";
            var stringContent = new StringContent(json);

            var obj = await stringContent.ReadAsAsync<StructToTest>();
            obj.Should().NotBeNull();
            obj.FirstName.Should().NotBeNullOrWhiteSpace();
            obj.LastName.Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public async Task HttpContentExtensions_ReadAsAsync_ShouldReadByteContentAndDeserializeIt()
        {
            const string json = "{\"FirstName\":\"Radosław\",\"LastName\":\"Mariowski\"}";
            var byteArray = json.ToUtf8ByteArray();
            var stringContent = new ByteArrayContent(byteArray);

            var obj = await stringContent.ReadAsAsync<StructToTest>();
            obj.Should().NotBeNull();
            obj.FirstName.Should().NotBeNullOrWhiteSpace();
            obj.LastName.Should().NotBeNullOrWhiteSpace();
        } 
    }
}