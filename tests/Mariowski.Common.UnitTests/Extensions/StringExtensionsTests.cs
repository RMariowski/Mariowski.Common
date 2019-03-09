using FluentAssertions;
using Mariowski.Common.Extensions;
using System;
using Xunit;

namespace Mariowski.Common.UnitTests.Extensions
{
    public class StringExtensionsTests
    {
        #region Valid

        [Theory]
        [InlineData("0", byte.MinValue)]
        [InlineData("12", (byte)12)]
        [InlineData("128", (byte)128)]
        [InlineData("255", byte.MaxValue)]
        public void StringExtensions_ToByte_ShouldBeConvertedToByte(string value, byte expected)
        {
            value.ToByte().Should().Be(expected);
        }

        [Theory]
        [InlineData("-32768", short.MinValue)]
        [InlineData("-128", (short)-128)]
        [InlineData("-1234", (short)-1234)]
        [InlineData("0", (short)0)]
        [InlineData("128", (short)128)]
        [InlineData("1234", (short)1234)]
        [InlineData("32767", short.MaxValue)]
        public void StringExtensions_ToShort_ShouldBeConvertedToShortInt(string value, short expected)
        {
            value.ToShort().Should().Be(expected);
        }

        [Theory]
        [InlineData("-2147483648", int.MinValue)]
        [InlineData("-1234567890", -1234567890)]
        [InlineData("-123456789", -123456789)]
        [InlineData("-12345678", -12345678)]
        [InlineData("-1234567", -1234567)]
        [InlineData("-123456", -123456)]
        [InlineData("-12345", -12345)]
        [InlineData("-1234", -1234)]
        [InlineData("-128", -128)]
        [InlineData("0", 0)]
        [InlineData("128", 128)]
        [InlineData("1234", 1234)]
        [InlineData("12345", 12345)]
        [InlineData("123456", 123456)]
        [InlineData("1234567", 1234567)]
        [InlineData("12345678", 12345678)]
        [InlineData("123456789", 123456789)]
        [InlineData("1234567890", 1234567890)]
        [InlineData("2147483647", int.MaxValue)]
        public void StringExtensions_ToInt_ShouldBeConvertedToInt(string value, int expected)
        {
            value.ToInt().Should().Be(expected);
        }

        [Theory]
        [InlineData("-9223372036854775808", long.MinValue)]
        [InlineData("-1234567890123456789", -1234567890123456789L)]
        [InlineData("-123456789012345678", -123456789012345678L)]
        [InlineData("-12345678901234567", -12345678901234567L)]
        [InlineData("-1234567890123456", -1234567890123456L)]
        [InlineData("-123456789012345", -123456789012345L)]
        [InlineData("-12345678901234", -12345678901234L)]
        [InlineData("-1234567890123", -1234567890123L)]
        [InlineData("-123456789012", -123456789012L)]
        [InlineData("-12345678901", -12345678901L)]
        [InlineData("-1234567890", -1234567890L)]
        [InlineData("-123456789", -123456789L)]
        [InlineData("-12345678", -12345678L)]
        [InlineData("-1234567", -1234567L)]
        [InlineData("-123456", -123456L)]
        [InlineData("-12345", -12345L)]
        [InlineData("-1234", -1234L)]
        [InlineData("-128", -128L)]
        [InlineData("0", 0L)]
        [InlineData("128", 128L)]
        [InlineData("1234", 1234L)]
        [InlineData("12345", 12345L)]
        [InlineData("123456", 123456L)]
        [InlineData("1234567", 1234567L)]
        [InlineData("12345678", 12345678L)]
        [InlineData("123456789", 123456789L)]
        [InlineData("1234567890", 1234567890L)]
        [InlineData("12345678901", 12345678901L)]
        [InlineData("123456789012", 123456789012L)]
        [InlineData("1234567890123", 1234567890123L)]
        [InlineData("12345678901234", 12345678901234L)]
        [InlineData("123456789012345", 123456789012345L)]
        [InlineData("1234567890123456", 1234567890123456L)]
        [InlineData("12345678901234567", 12345678901234567L)]
        [InlineData("123456789012345678", 123456789012345678L)]
        [InlineData("1234567890123456789", 1234567890123456789L)]
        [InlineData("9223372036854775807", long.MaxValue)]
        public void StringExtensions_ToLong_ShouldBeConvertedToInt(string value, long expected)
        {
            value.ToLong().Should().Be(expected);
        }

        [Theory]
        [InlineData("-1", -1.0f)]
        [InlineData("-0,00000000000000000000", 0.0f)]
        [InlineData("-0", -0.0f)]
        [InlineData("0,00000000000000000000", 0.0f)]
        [InlineData("0", 0.0f)]
        [InlineData("1", 1.0f)]
        public void StringExtensions_ToFloat_ShouldBeConvertedToByte(string value, float expected)
        {
            value.ToFloat().Should().Be(expected);
        }

        [Fact]
        public void StringExtensions_ToAsciiByteArray_ShouldConvertStringToByteArray()
        {
            const string value = "Abc";
            var expected = new byte[] { 65, 98, 99 };

            value.ToAsciiByteArray().Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void StringExtensions_ToUtf8ByteArray_ShouldConvertStringToByteArray()
        {
            const string value = "Ąćę";
            var expected = new byte[] { 0xC4, 0x84, 0xC4, 0x87, 0xC4, 0x99 };

            value.ToUtf8ByteArray().Should().BeEquivalentTo(expected);
        }

        #endregion

        #region Invalid

        [Fact]
        public void StringExtensions_All_To_Numbers_NullValueShouldThrowArgumentNullException()
        {
            string value = null;

            Assert.Throws<ArgumentNullException>(() => value.ToByte());
            Assert.Throws<ArgumentNullException>(() => value.ToShort());
            Assert.Throws<ArgumentNullException>(() => value.ToInt());
            Assert.Throws<ArgumentNullException>(() => value.ToLong());
            Assert.Throws<ArgumentNullException>(() => value.ToFloat());
        }

        [Theory]
        [InlineData("-")]
        [InlineData("abc")]
        [InlineData("a128b")]
        [InlineData("")]
        [InlineData("     ")]
        public void StringExtensions_All_To_Numbers_InvalidValueShouldThrowFormatException(string value)
        {
            Assert.Throws<FormatException>(() => value.ToByte());
            Assert.Throws<FormatException>(() => value.ToShort());
            Assert.Throws<FormatException>(() => value.ToInt());
            Assert.Throws<FormatException>(() => value.ToLong());
            Assert.Throws<FormatException>(() => value.ToFloat());
        }

        [Theory]
        [InlineData("-128")]
        [InlineData("-1")]
        [InlineData("256")]
        [InlineData("512")]
        public void StringExtensions_ToByte_TooSmallOrTooBigValueShouldThrowOverflowException(string value)
        {
            Assert.Throws<OverflowException>(() => value.ToByte());
        }

        [Theory]
        [InlineData("-123456")]
        [InlineData("-32769")]
        [InlineData("32768")]
        [InlineData("123456")]
        public void StringExtensions_ToShort_TooSmallOrTooBigValueShouldThrowOverflowException(string value)
        {
            Assert.Throws<OverflowException>(() => value.ToShort());
        }

        [Theory]
        [InlineData("-12345678901")]
        [InlineData("-2147483649")]
        [InlineData("2147483648")]
        [InlineData("12345678901")]
        public void StringExtensions_ToInt_TooSmallOrTooBigValueShouldThrowOverflowException(string value)
        {
            Assert.Throws<OverflowException>(() => value.ToInt());
        }

        [Theory]
        [InlineData("-12345678901234567890")]
        [InlineData("-9223372036854775809")]
        [InlineData("9223372036854775808")]
        [InlineData("12345678901234567890")]
        public void StringExtensions_ToLong_TooSmallOrTooBigValueShouldThrowOverflowException(string value)
        {
            Assert.Throws<OverflowException>(() => value.ToLong());
        }

        [Theory]
        [InlineData("-1234567890123456789012345678901234567890,123456789012345678901234567890")]
        [InlineData("1234567890123456789012345678901234567890,123456789012345678901234567890")]
        public void StringExtensions_ToFloat_TooSmallOrTooBigValueShouldThrowOverflowException(string value)
        {
            Assert.Throws<OverflowException>(() => value.ToFloat());
        }

        [Fact]
        public void StringExtensions_All_To_ByteArray_NullValueShouldThrowArgumentNullException()
        {
            string value = null;

            Assert.Throws<ArgumentNullException>(() => value.ToAsciiByteArray());
            Assert.Throws<ArgumentNullException>(() => value.ToUtf8ByteArray());
        }

        #endregion
    }
}