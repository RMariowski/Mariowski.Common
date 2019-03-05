using System;

namespace Mariowski.Common.Extensions
{
    public static class StringExtensions
    {
        #region Convert

        #region Convert to Byte

        /// <summary>
        /// Converts string to byte.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <exception cref="T:System.FormatException"><paramref name="this">this</paramref> is not a number in a valid format.</exception>
        /// <returns>An 8-bit unsigned integer that is equivalent to <paramref name="this">value</paramref>.</returns>
        public static byte ToByte(this string @this)
            => Convert.ToByte(@this);

        #endregion

        #region Convert to Int16

        /// <summary>
        /// Converts string to short int.
        /// </summary>
        /// <remarks>Same as <see cref="ToShort"/>.</remarks>
        /// <param name="this">The @this to act on.</param>
        /// <exception cref="T:System.FormatException"><paramref name="this">this</paramref> is not a number in a valid format.</exception>
        /// <returns>A 16-bit signed integer that is equivalent to <paramref name="this">value</paramref>.</returns>
        public static short ToInt16(this string @this)
            => Convert.ToInt16(@this);

        #endregion

        #region Convert to Int32

        /// <summary>
        /// Converts string to int.
        /// </summary>
        /// <remarks>Same as <see cref="ToInt"/>.</remarks>
        /// <param name="this">The @this to act on.</param>
        /// <exception cref="T:System.FormatException"><paramref name="this">this</paramref> is not a number in a valid format.</exception>
        /// <returns>A 32-bit signed integer that is equivalent to <paramref name="this">value</paramref>.</returns>
        public static int ToInt32(this string @this)
            => Convert.ToInt32(@this);

        #endregion

        #region Convert to Int64

        /// <summary>
        /// Converts string to long.
        /// </summary>
        /// <remarks>Same as <see cref="ToLong"/>.</remarks>
        /// <param name="this">The @this to act on.</param>
        /// <exception cref="T:System.FormatException"><paramref name="this">this</paramref> is not a number in a valid format.</exception>
        /// <returns>A 64-bit signed integer that is equivalent to <paramref name="this">value</paramref>.</returns>
        public static long ToInt64(this string @this)
            => Convert.ToInt64(@this);

        #endregion

        #region Convert to Short

        /// <summary>
        /// Converts string to short int.
        /// </summary>
        /// <remarks>Same as <see cref="ToInt16"/>.</remarks>
        /// <param name="this">The @this to act on.</param>
        /// <exception cref="T:System.FormatException"><paramref name="this">this</paramref> is not a number in a valid format.</exception>
        /// <returns>A 16-bit signed integer that is equivalent to <paramref name="this">value</paramref>.</returns>
        public static short ToShort(this string @this)
            => @this.ToInt16();

        #endregion

        #region Convert to Int

        /// <summary>
        /// Converts string to int.
        /// </summary>
        /// <remarks>Same as <see cref="ToInt32"/>.</remarks>
        /// <param name="this">The @this to act on.</param>
        /// <exception cref="T:System.FormatException"><paramref name="this">this</paramref> is not a number in a valid format.</exception>
        /// <returns>A 32-bit signed integer that is equivalent to <paramref name="this">value</paramref>.</returns>
        public static int ToInt(this string @this)
            => @this.ToInt32();

        #endregion

        #region Convert to Long

        /// <summary>
        /// Converts string to long.
        /// </summary>
        /// <remarks>Same as <see cref="ToInt64"/>.</remarks>
        /// <param name="this">The @this to act on.</param>
        /// <exception cref="T:System.FormatException"><paramref name="this">this</paramref> is not a number in a valid format.</exception>
        /// <returns>A 64-bit signed integer that is equivalent to <paramref name="this">value</paramref>.</returns>
        public static long ToLong(this string @this)
            => @this.ToInt64();

        #endregion

        #region Convert to Single

        /// <summary>
        /// Converts string to float.
        /// </summary>
        /// <remarks>Same as <see cref="ToFloat"/>.</remarks>
        /// <param name="this">The @this to act on.</param>
        /// <exception cref="T:System.FormatException"><paramref name="this">this</paramref> is not a number in a valid format.</exception>
        /// <returns>A single-precision floating-point number that is equivalent to the number in <paramref name="this">value</paramref>.</returns>
        public static float ToSingle(this string @this)
            => Convert.ToSingle(@this);

        #endregion

        #region Convert to Float

        /// <summary>
        /// Converts string to float.
        /// </summary>
        /// <remarks>Same as <see cref="ToSingle"/>.</remarks>
        /// <param name="this">The @this to act on.</param>
        /// <exception cref="T:System.FormatException"><paramref name="this">this</paramref> is not a number in a valid format.</exception>
        /// <returns>A single-precision floating-point number that is equivalent to the number in <paramref name="this">value</paramref>.</returns>
        public static float ToFloat(this string @this)
            => @this.ToSingle();

        #endregion

        #endregion
    }
}