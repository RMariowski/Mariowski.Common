using System.Text;

namespace Mariowski.Common.Extensions
{
    public static class StringExtensions
    {
        #region Convert

        #region Convert to Byte

        /// <summary>
        /// Converts the string representation of a number to its <see cref="T:System.Byte"></see> equivalent.
        /// </summary>
        /// <param name="s">A string that contains a number to convert. The string is interpreted using the <see cref="F:System.Globalization.NumberStyles.Integer"></see> style.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="s">s</paramref> is null.</exception>
        /// <exception cref="T:System.FormatException"><paramref name="s">s</paramref> is not of the correct format.</exception>
        /// <exception cref="T:System.OverflowException"><paramref name="s">s</paramref> represents a number less than <see cref="F:System.Byte.MinValue"></see> or greater than <see cref="F:System.Byte.MaxValue"></see>.</exception>
        /// <returns>A byte value that is equivalent to the number contained in <paramref name="s">s</paramref>.</returns>
        public static byte ToByte(this string s)
            => byte.Parse(s);

        #endregion

        #region Convert to Short

        /// <summary>
        /// Converts the string representation of a number to its 16-bit signed integer equivalent.
        /// </summary>
        /// <param name="s">A string containing a number to convert.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="s">s</paramref> is null.</exception>
        /// <exception cref="T:System.FormatException"><paramref name="s">s</paramref> is not in the correct format.</exception>
        /// <exception cref="T:System.OverflowException"><paramref name="s">s</paramref> represents a number less than <see cref="F:System.Int16.MinValue"></see> or greater than <see cref="F:System.Int16.MaxValue"></see>.</exception>
        /// <returns>A 16-bit signed integer equivalent to the number contained in <paramref name="s">s</paramref>.</returns>
        public static short ToShort(this string s)
            => short.Parse(s);

        #endregion

        #region Convert to Int

        /// <summary>
        /// Converts the string representation of a number to its 32-bit signed integer equivalent.
        /// </summary>
        /// <param name="s">A string containing a number to convert.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="s">s</paramref> is null.</exception>
        /// <exception cref="T:System.FormatException"><paramref name="s">s</paramref> is not in the correct format.</exception>
        /// <exception cref="T:System.OverflowException"><paramref name="s">s</paramref> represents a number less than <see cref="F:System.Int32.MinValue"></see> or greater than <see cref="F:System.Int32.MaxValue"></see>.</exception>
        /// <returns>A 32-bit signed integer equivalent to the number contained in <paramref name="s">s</paramref>.</returns>
        public static int ToInt(this string s)
            => int.Parse(s);

        #endregion

        #region Convert to Long

        /// <summary>
        /// Converts the string representation of a number to its 64-bit signed integer equivalent.
        /// </summary>
        /// <param name="s">A string containing a number to convert.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="s">s</paramref> is null.</exception>
        /// <exception cref="T:System.FormatException"><paramref name="s">s</paramref> is not in the correct format.</exception>
        /// <exception cref="T:System.OverflowException"><paramref name="s">s</paramref> represents a number less than <see cref="F:System.Int64.MinValue"></see> or greater than <see cref="F:System.Int64.MaxValue"></see>.</exception>
        /// <returns>A 64-bit signed integer equivalent to the number contained in <paramref name="s">s</paramref>.</returns>
        public static long ToLong(this string s)
            => long.Parse(s);

        #endregion

        #region Convert to Float

        /// <summary>
        /// Converts the string representation of a number to its single-precision floating-point number equivalent.
        /// </summary>
        /// <param name="s">A string that contains a number to convert.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="s">s</paramref> is null.</exception>
        /// <exception cref="T:System.FormatException"><paramref name="s">s</paramref> does not represent a number in a valid format.</exception>
        /// <exception cref="T:System.OverflowException"><paramref name="s">s</paramref> represents a number less than <see cref="F:System.Single.MinValue"></see> or greater than <see cref="F:System.Single.MaxValue"></see>.</exception>
        /// <returns>A single-precision floating-point number equivalent to the numeric value or symbol specified in <paramref name="s">s</paramref>.</returns>
        public static float ToFloat(this string s)
            => float.Parse(s);

        #endregion

        #region Convert to Byte Array

        /// <summary>
        /// Encodes all the characters in the specified string into a sequence of bytes.
        /// </summary>
        /// <param name="s">The string containing the characters to encode.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="s">s</paramref> is null.</exception>
        /// <exception cref="T:System.Text.EncoderFallbackException">A fallback occurred (see Character Encoding in the .NET for complete explanation)   -and-  <see cref="P:System.Text.Encoding.EncoderFallback"></see> is set to <see cref="T:System.Text.EncoderExceptionFallback"></see>.</exception>
        /// <returns>A byte array containing the results of encoding the specified set of characters.</returns>
        public static byte[] ToAsciiByteArray(this string s)
            => Encoding.ASCII.GetBytes(s);

        /// <summary>
        /// Encodes all the characters in the specified string into a sequence of bytes.
        /// </summary>
        /// <param name="s">The string containing the characters to encode.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="s">s</paramref> is null.</exception>
        /// <exception cref="T:System.Text.EncoderFallbackException">A fallback occurred (see Character Encoding in the .NET for complete explanation)   -and-  <see cref="P:System.Text.Encoding.EncoderFallback"></see> is set to <see cref="T:System.Text.EncoderExceptionFallback"></see>.</exception>
        /// <returns>A byte array containing the results of encoding the specified set of characters.</returns>
        public static byte[] ToUtf8ByteArray(this string s)
            => Encoding.UTF8.GetBytes(s);

        #endregion

        #endregion
    }
}