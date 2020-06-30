using System;

namespace Mariowski.Common.DataTypes
{
    public struct ShortGuid
    {
        /// <summary>
        /// A read-only instance of the <see cref="T:ShortGuid"></see> structure
        /// whose guid is all zeros and value is null.
        /// </summary>
        public static readonly ShortGuid Empty = default(ShortGuid);

        private Guid _guid;
        public Guid Guid
        {
            get => _guid;
            set
            {
                if (value == _guid)
                    return;

                _guid = value;
                _value = Encode(value);
            }
        }

        private string _value;
        public string Value
        {
            get => _value;
            set
            {
                if (value == _value)
                    return;

                _value = value;
                _guid = Decode(value);
            }
        }

        /// <summary>
        /// Creates a ShortGuid from a base64 encoded string.
        /// </summary>
        /// <param name="s">The encoded guid as a base64 string.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="s">s</paramref> is null.</exception>
        /// <exception cref="T:System.FormatException">The length of <paramref name="s">s</paramref>, ignoring white-space characters, is not zero or a multiple of 4.   -or-   The format of <paramref name="s">s</paramref> is invalid. <paramref name="s">s</paramref> contains a non-base-64 character, more than two padding characters, or a non-white space-character among the padding characters.</exception>
        public ShortGuid(string s)
        {
            _guid = Decode(s);
            _value = s;
        }

        /// <summary>
        /// Creates a ShortGuid from a Guid.
        /// </summary>
        /// <param name="guid">The Guid to encode.</param>
        public ShortGuid(Guid guid)
        {
            _value = Encode(guid);
            _guid = guid;
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
            switch (obj)
            {
                case ShortGuid shortGuid:
                    return _guid.Equals(shortGuid._guid);

                case Guid guid:
                    return _guid.Equals(guid);

                case string @string:
                    return _value.Equals(@string);

                default:
                    return false;
            }
        }

        // ReSharper disable NonReadonlyMemberInGetHashCode
        /// <summary>
        /// Returns the HashCode of underlying Guid.
        /// </summary>
        /// <returns>HashCode of Guid.</returns>
        public override int GetHashCode()
            => _guid.GetHashCode();

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ShortGuid"></see> structure.
        /// </summary>
        /// <returns>A new ShortGuid object.</returns>
        public static ShortGuid NewShortGuid()
            => new ShortGuid(Guid.NewGuid());

        /// <summary>
        /// Indicates whether the values of two specified <see cref="T:ShortGuid"></see> objects are equal.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns>True if <paramref name="left">left</paramref> and <paramref name="right">right</paramref> are equal; otherwise, false.</returns>
        public static bool operator ==(ShortGuid left, ShortGuid right)
            => left._guid == right._guid;

        /// <summary>
        /// Indicates whether the values of two specified <see cref="T:ShortGuid"></see> objects are not equal.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns>true if <paramref name="left">left</paramref> and <paramref name="right">right</paramref> are not equal; otherwise, false.</returns>
        public static bool operator !=(ShortGuid left, ShortGuid right)
            => !(left == right);

        /// <summary>
        /// Creates a new instance of a <see cref="T:System.Guid"></see> using the string value, 
        /// then returns the base64 encoded version of the <see cref="T:System.Guid"></see>.
        /// </summary>
        /// <param name="value">An actual <see cref="T:System.Guid">Guid</see> string.</param>
        /// <returns>Base64 version of <see cref="T:System.Guid"></see></returns>
        private static string Encode(string value)
        {
            var guid = new Guid(value);
            return Encode(guid);
        }

        /// <summary>
        /// Creates a new instance of a <see cref="T:System.Guid"></see> using the
        /// <see cref="T:System.Guid">Guid</see>.
        /// </summary>
        /// <param name="guid">The <see cref="T:System.Guid">Guid</see> to encode</param>
        /// <returns>Base64 version of <see cref="T:System.Guid"></see></returns>
        private static string Encode(Guid guid)
        {
            string encoded = Convert.ToBase64String(guid.ToByteArray());
            encoded = encoded
                .Replace("/", "_")
                .Replace("+", "-");
            return encoded.Substring(0, 22);
        }

        /// <summary>
        /// Decodes the given base64 string to <see cref="T:System.Guid"></see>.
        /// </summary>
        /// <param name="s">The base64 encoded string of a <see cref="T:System.Guid">Guid</see>.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="s">s</paramref> is null.</exception>
        /// <exception cref="T:System.FormatException">The length of <paramref name="s">s</paramref>, ignoring white-space characters, is not zero or a multiple of 4.   -or-   The format of <paramref name="s">s</paramref> is invalid. <paramref name="s">s</paramref> contains a non-base-64 character, more than two padding characters, or a non-white space-character among the padding characters.</exception>
        /// <returns>A new <see cref="T:System.Guid">Guid</see>.</returns>
        private static Guid Decode(string s)
        {
            if (s == null)
                throw new ArgumentNullException(nameof(s), "Value cannot be null.");

            s = s
                .Replace("_", "/")
                .Replace("-", "+");
            var buffer = Convert.FromBase64String(s + "==");
            return new Guid(buffer);
        }

        /// <summary>
        /// Returns the base64 encoded <see cref="T:ShortGuid">ShortGuid</see> as a string.
        /// </summary>
        /// <returns>The value of <see cref="T:ShortGuid"></see>.</returns>
        public override string ToString()
            => _value;

        /// <summary>
        /// Implicitly converts the <see cref="T:System.Guid">Guid</see>
        /// to a <see cref="T:ShortGuid">ShortGuid</see>.
        /// </summary>
        /// <param name="guid">The guid value of <see cref="T:ShortGuid">ShortGuid</see>.</param>
        /// <returns>A new <see cref="T:ShortGuid">ShortGuid</see> object.</returns>
        public static implicit operator ShortGuid(Guid guid)
            => new ShortGuid(guid);

        /// <summary>
        /// Implicitly converts the string to a <see cref="T:ShortGuid">ShortGuid</see>.
        /// </summary>
        /// <param name="value">The string value of <see cref="T:ShortGuid">ShortGuid</see>.</param>
        /// <returns>A new <see cref="T:ShortGuid">ShortGuid</see> object.</returns>
        public static implicit operator ShortGuid(string value)
            => new ShortGuid(value);

        /// <summary>
        /// Implicitly converts the <see cref="T:ShortGuid">ShortGuid</see>
        /// to it's <see cref="T:System.Guid">Guid</see> equivalent.
        /// </summary>
        /// <param name="shortGuid"><see cref="T:ShortGuid">ShortGuid</see> to convert.</param>
        /// <returns><see cref="T:System.Guid">Guid</see> equivalent of <see cref="T:ShortGuid">ShortGuid</see>.</returns>
        public static implicit operator Guid(ShortGuid shortGuid)
            => shortGuid._guid;

        /// <summary>
        /// Implicitly converts the <see cref="T:ShortGuid">ShortGuid</see>
        /// to it's string equivalent.
        /// </summary>
        /// <param name="shortGuid"><see cref="T:ShortGuid">ShortGuid</see> to convert.</param>
        /// <returns>String equivalent of <see cref="T:ShortGuid">ShortGuid</see>.</returns>
        public static implicit operator string(ShortGuid shortGuid)
            => shortGuid._value;
    }
}