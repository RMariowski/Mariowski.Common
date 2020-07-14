using System;

namespace Mariowski.Common.DataTypes
{
    public partial class ShortGuid : IEquatable<ShortGuid>
    {
        private readonly string _value;

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
            _value = encoded;
        }

        /// <summary>
        /// Creates a ShortGuid from a Guid.
        /// </summary>
        /// <param name="guid">The Guid to encode.</param>
        public ShortGuid(Guid guid)
        {
            _value = Encode(guid);
            Guid = guid;
        }

        /// <summary>
        /// Returns a value that indicates whether this instance is equal to a specified <see cref="T:ShortGuid"></see>.
        /// </summary>
        /// <param name="other">The <see cref="T:ShortGuid"></see> to compare with this instance.</param>
        /// <returns>
        /// True if <paramref name="other">other</paramref> has the same guid and value as this instance;
        /// Otherwise, false.
        /// </returns>
        public bool Equals(ShortGuid other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Guid.Equals(other.Guid) && _value == other._value;
        }

        /// <summary>
        /// Returns a value that indicates whether this instance is equal to a specified object.
        /// </summary>
        /// <param name="obj">The object to compare with this instance.</param>
        /// <returns>
        /// True if <paramref name="obj">obj</paramref> is a <see cref="T:ShortGuid"></see> that has the same value as this instance;
        /// True if <paramref name="obj">obj</paramref> is a <see cref="T:System.Guid"></see> that has the same value as this instance;
        /// True if <paramref name="obj">obj</paramref> is a string that has the same value as this instance;
        /// Otherwise, false.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((ShortGuid)obj);
        }

        /// <summary>
        /// Returns the base64 encoded <see cref="T:ShortGuid">ShortGuid</see> as a string.
        /// </summary>
        /// <returns>The value of <see cref="T:ShortGuid"></see>.</returns>
        public override string ToString()
            => _value;

        /// <summary>
        /// Returns the HashCode of underlying Guid.
        /// </summary>
        /// <returns>HashCode of Guid.</returns>
        public override int GetHashCode()
            => Guid.GetHashCode();
    }
}