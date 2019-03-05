using FluentAssertions;
using Mariowski.Common.Extensions;
using Newtonsoft.Json;
using Xunit;

namespace Mariowski.Common.UnitTests.Extensions
{
    public class JsonExtensionsTests
    {
        private struct StructToTest
        {
            public string FirstName;
            public string LastName { get; set; }
        }

        #region Valid

        [Fact]
        public void JsonExtensions_ToJson_And_Serialize_ShouldSerializeObjectToJson()
        {
            var obj = new StructToTest { FirstName = "Radosław", LastName = "Mariowski" };
            const string expected = "{\"FirstName\":\"Radosław\",\"LastName\":\"Mariowski\"}";

            obj.ToJson().Should().BeEquivalentTo(expected);
            obj.Serialize().Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void JsonExtensions_ToJson_And_Serialize_WithPrettyTrue_ShouldSerializeObjectToJsonWithPrettyFormating()
        {
            var obj = new StructToTest { FirstName = "Radosław", LastName = "Mariowski" };
            const string expected = @"{
  ""FirstName"": ""Radosław"",
  ""LastName"": ""Mariowski""
}";

            obj.ToJson(true).Should().BeEquivalentTo(expected);
            obj.Serialize(true).Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void JsonExtensions_Deserialize_ShouldDeserializeJsonToObject()
        {
            const string json = "{\"FirstName\":\"Radosław\",\"LastName\":\"Mariowski\"}";

            var obj = json.Deserialize<StructToTest>();
            obj.FirstName.Should().NotBeNullOrWhiteSpace();
            obj.LastName.Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public void JsonExtensions_TryDeserialize_ShouldDeserializeJsonToObject()
        {
            const string json = "{\"FirstName\":\"Radosław\",\"LastName\":\"Mariowski\"}";

            var obj = json.TryDeserialize<StructToTest>();
            obj.FirstName.Should().NotBeNullOrWhiteSpace();
            obj.LastName.Should().NotBeNullOrWhiteSpace();
        }

        #endregion

        #region Invalid

        [Theory]
        [InlineData("")]
        [InlineData("    ")]
        public void JsonExtensions_Deserialize_DeserializeShouldThrowExceptionDueToInvalidJson(string json)
        {
            Assert.Throws<JsonSerializationException>(() => json.Deserialize<StructToTest>());
        }

        [Theory]
        [InlineData("")]
        [InlineData("    ")]
        [InlineData("{\"MiddleName\":\"Radosław\",\"Flag\":\"123\"}")]
        public void JsonExtensions_TryDeserialize_ShouldDeserializeJsonToObjectEvenWithInvalidJson(string json)
        {
            var obj = json.TryDeserialize<StructToTest>();
            obj.FirstName.Should().BeNull();
            obj.LastName.Should().BeNull();
        }

        #endregion
    }
}