﻿using System;

namespace Mariowski.Common.DataTypes
{
    public partial class ShortGuid : ValueObject<ShortGuid>, IEquatable<ShortGuid>
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
        /// Determines whether the specified <see cref="T:ShortGuid"></see> is equal to the current <see cref="T:ShortGuid"></see>.
        /// </summary>
        /// <param name="other"><see cref="T:ShortGuid"></see> to compare.</param>
        /// <returns>True if <paramref name="other">other</paramref> is equal to the current <see cref="T:ShortGuid"></see>.</returns>
        public override bool Equals(ShortGuid other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Guid.Equals(other.Guid) && _value == other._value;
        }

        /// <inheritdoc />
        public override string ToString()
            => _value;

        /// <inheritdoc />
        public override int GetHashCode()
            => Guid.GetHashCode();
    }
}