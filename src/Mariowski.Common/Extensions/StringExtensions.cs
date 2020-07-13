using System.Text;

// ReSharper disable UnusedMember.Global

namespace Mariowski.Common.Extensions
{
    public static class StringExtensions
    {
        /// <inheritdoc cref="System.Byte.Parse(string)"></inheritdoc>
        public static byte ToByte(this string s)
            => byte.Parse(s);

        /// <inheritdoc cref="System.Int16.Parse(string)"></inheritdoc>
        public static short ToShort(this string s)
            => short.Parse(s);

        /// <inheritdoc cref="System.UInt16.Parse(string)"></inheritdoc>
        public static ushort ToUShort(this string s)
            => ushort.Parse(s);

        /// <inheritdoc cref="System.Int32.Parse(string)"></inheritdoc>
        public static int ToInt(this string s)
            => int.Parse(s);

        /// <inheritdoc cref="System.UInt32.Parse(string)"></inheritdoc>
        public static uint ToUInt(this string s)
            => uint.Parse(s);

        /// <inheritdoc cref="System.Int64.Parse(string)"></inheritdoc>
        public static long ToLong(this string s)
            => long.Parse(s);

        /// <inheritdoc cref="System.UInt64.Parse(string)"></inheritdoc>
        public static ulong ToULong(this string s)
            => ulong.Parse(s);

        /// <inheritdoc cref="System.Single.Parse(string)"></inheritdoc>
        public static float ToFloat(this string s)
            => float.Parse(s);

        /// <inheritdoc cref="System.Double.Parse(string)"></inheritdoc>
        public static double ToDouble(this string s)
            => double.Parse(s);

        /// <inheritdoc cref="System.Text.Encoding.GetBytes(string)"></inheritdoc>
        public static byte[] ToAsciiByteArray(this string s)
            => Encoding.ASCII.GetBytes(s);

        /// <inheritdoc cref="System.Text.Encoding.GetBytes(string)"></inheritdoc>
        public static byte[] ToUtf8ByteArray(this string s)
            => Encoding.UTF8.GetBytes(s);
    }
}