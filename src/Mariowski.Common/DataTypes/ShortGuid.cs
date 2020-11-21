using System;

namespace Mariowski.Common.DataTypes
{
    public partial record ShortGuid
    {
        public string Value { get; }

        public Guid Guid { get; }

        /// <summary>
        /// Creates a ShortGuid from a base64 encoded string.
        /// </summary>
        /// <param name="encoded">The encoded guid as a base64 string.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="encoded">encoded</paramref> is null.</exception>
        /// <exception cref="T:System.FormatException">The length of <paramref name="encoded">encoded</paramref>, ignoring white-space characters, is not zero or a multiple of 4.   -or-   The format of <paramref name="encoded">encoded</paramref> is invalid. <paramref name="encoded">encoded</paramref> contains a non-base-64 character, more than two padding characters, or a non-white space-character among the padding characters.</exception>
        public ShortGuid(string encoded)
        {
            Guid = Decode(encoded);
            Value = encoded;
        }

        /// <summary>
        /// Creates a ShortGuid from a Guid.
        /// </summary>
        /// <param name="guid">The Guid to encode.</param>
        public ShortGuid(Guid guid)
        {
            Value = Encode(guid);
            Guid = guid;
        }
    }
}