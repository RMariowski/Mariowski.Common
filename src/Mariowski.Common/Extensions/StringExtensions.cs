using System;
using System.Buffers.Text;
using System.Runtime.InteropServices;
using System.Text;

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

        /// <summary>
        /// Decodes the given base64 string to <see cref="T:System.Guid"></see>.
        /// </summary>
        /// <param name="encoded">The base64 encoded string of a <see cref="T:System.Guid">Guid</see>.</param>
        /// <param name="replaceUnderscoreWith">Byte character that will replace underscore '_'.</param>
        /// <param name="replaceDashWith">Byte character that will replace dash '-'.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="encoded">encoded</paramref> is null.</exception>
        /// <exception cref="T:System.FormatException">The length of <paramref name="encoded">encoded</paramref>, ignoring white-space characters, is not zero or a multiple of 4.   -or-   The format of <paramref name="encoded">encoded</paramref> is invalid. <paramref name="encoded">encoded</paramref> contains a non-base-64 character, more than two padding characters, or a non-white space-character among the padding characters.</exception>
        /// <returns>A new <see cref="T:System.Guid">Guid</see>.</returns>
        public static Guid DecodeBase64ToGuid(this string encoded, byte replaceUnderscoreWith = (byte)'/',
            byte replaceDashWith = (byte)'+')
        {
            if (encoded is null)
                throw new ArgumentNullException(nameof(encoded), "Value cannot be null.");

            const int encodedLength = 22;
            if (encoded.Length > encodedLength)
                throw new FormatException(""); // TODO

            var encodedChars = encoded.AsSpan();
            Span<byte> base64 = stackalloc byte[24];

            // Restore replaced characters.
            for (var i = 0; i < encodedLength; i++)
            {
                base64[i] = encodedChars[i] switch
                {
                    '_' => replaceUnderscoreWith,
                    '-' => replaceDashWith,
                    _ => (byte)encodedChars[i]
                };
            }

            // Restore '==' padding.
            base64[22] = (byte)'=';
            base64[23] = (byte)'=';

            Span<byte> guidBytes = stackalloc byte[16];
            Base64.DecodeFromUtf8(base64, guidBytes, out _, out _);

            var final = new Guid(guidBytes);
            return final;
        }
    }
}