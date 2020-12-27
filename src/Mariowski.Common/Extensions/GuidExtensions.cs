using System;
using System.Buffers.Text;
using System.Runtime.InteropServices;

namespace Mariowski.Common.Extensions
{
    public static class GuidExtensions
    {
        /// <summary>
        /// Encodes GUID to Base64 string.
        /// <note>
        /// Inspired by https://www.stevejgordon.co.uk/using-high-performance-dotnetcore-csharp-techniques-to-base64-encode-a-guid
        /// </note>
        /// </summary>
        /// <param name="guid">The <see cref="T:System.Guid">Guid</see> to encode.</param>
        /// <param name="replaceForwardSlashWith">Character that will replace forward slash '/'.</param>
        /// <param name="replacePlusWith">Character that will replace plus '+'.</param>
        /// <returns>Base64 version of <see cref="T:System.Guid"></see>.</returns>
        public static string EncodeBase64String(this Guid guid, char replaceForwardSlashWith = '_',
            char replacePlusWith = '-')
        {
            Span<byte> guidBytes = stackalloc byte[16];
            MemoryMarshal.TryWrite(guidBytes, ref guid);

            Span<byte> encodedBytes = stackalloc byte[24];
            Base64.EncodeToUtf8(guidBytes, encodedBytes, out _, out _);

            Span<char> chars = stackalloc char[22];

            const byte forwardSlashByte = (byte)'/';
            const byte plusByte = (byte)'+';

            // Replace any characters which are not URL safe.
            // Skip the final two bytes as these will be '==' padding we don't need.
            for (var i = 0; i < 22; i++)
            {
                chars[i] = encodedBytes[i] switch
                {
                    forwardSlashByte => replaceForwardSlashWith,
                    plusByte => replacePlusWith,
                    _ => (char)encodedBytes[i]
                };
            }

#if NETSTANDARD2_0 // TODO: Delete when Unity will support at least .net standard 2.1
            var final = new string(chars.ToArray());
#else
            var final = new string(chars);
#endif
            return final;
        }
    }
}