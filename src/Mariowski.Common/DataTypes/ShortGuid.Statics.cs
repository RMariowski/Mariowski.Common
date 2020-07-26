using System;
using Mariowski.Common.Extensions;

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

        /// <inheritdoc cref="GuidExtensions.EncodeBase64String(System.Guid,char,char)"></inheritdoc>
        public static string Encode(Guid guid)
            => guid.EncodeBase64String();

        /// <inheritdoc cref="StringExtensions.DecodeBase64ToGuid(string)"></inheritdoc>
        public static Guid Decode(string encoded)
            => encoded.DecodeBase64ToGuid();

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