using System;

namespace Mariowski.Common.DataTypes
{
    public partial class ShortGuid
    {
        /// <summary>
        /// A read-only instance of the <see cref="T:ShortGuid"></see> structure
        /// whose guid is all zeros and value is null.
        /// </summary>
        public static readonly ShortGuid Empty = new ShortGuid(Guid.Empty);

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ShortGuid"></see> structure.
        /// </summary>
        /// <returns>A new ShortGuid object.</returns>
        public static ShortGuid NewShortGuid()
            => new ShortGuid(Guid.NewGuid());

        /// <summary>
        /// Creates a new instance of a <see cref="T:ShortGuid"></see> using the
        /// <see cref="T:System.Guid">Guid</see>.
        /// </summary>
        /// <param name="guid">The <see cref="T:System.Guid">Guid</see> to encode</param>
        /// <returns>Base64 version of <see cref="T:System.Guid"></see></returns>
        public static string Encode(Guid guid)
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
        /// <param name="encoded">The base64 encoded string of a <see cref="T:System.Guid">Guid</see>.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="encoded">encoded</paramref> is null.</exception>
        /// <exception cref="T:System.FormatException">The length of <paramref name="encoded">encoded</paramref>, ignoring white-space characters, is not zero or a multiple of 4.   -or-   The format of <paramref name="encoded">encoded</paramref> is invalid. <paramref name="encoded">encoded</paramref> contains a non-base-64 character, more than two padding characters, or a non-white space-character among the padding characters.</exception>
        /// <returns>A new <see cref="T:System.Guid">Guid</see>.</returns>
        public static Guid Decode(string encoded)
        {
            if (encoded is null)
                throw new ArgumentNullException(nameof(encoded), "Value cannot be null.");

            encoded = encoded
                .Replace("_", "/")
                .Replace("-", "+");
            var buffer = Convert.FromBase64String(encoded + "==");
            return new Guid(buffer);
        }

        /// <summary>
        /// Indicates whether the values of two specified <see cref="T:ShortGuid"></see> objects are equal.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns>True if <paramref name="left">left</paramref> and <paramref name="right">right</paramref> are equal; otherwise, false.</returns>
        public static bool operator ==(ShortGuid left, ShortGuid right)
        {
            if (left is null)
                return right is null;
            return !(right is null) && left.Equals(right);
        }

        /// <summary>
        /// Indicates whether the values of two specified <see cref="T:ShortGuid"></see> objects are not equal.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns>true if <paramref name="left">left</paramref> and <paramref name="right">right</paramref> are not equal; otherwise, false.</returns>
        public static bool operator !=(ShortGuid left, ShortGuid right)
            => !(left == right);

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
            => shortGuid.Guid;
    }
}