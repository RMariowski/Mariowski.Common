namespace Mariowski.Common.DataTypes
{
    public abstract class ValueObject<T> where T : ValueObject<T>
    {
        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((T)obj);
        }

        /// <summary>
        /// Indicates whether the values of two specified <see cref="T:ValueObject"></see> objects are equal.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns>True if <paramref name="left">left</paramref> and <paramref name="right">right</paramref> are equal; otherwise, false.</returns>
        public static bool operator ==(ValueObject<T> left, ValueObject<T> right)
        {
            if (left is null)
                return right is null;
            return !(right is null) && left.Equals((T)right);
        }

        /// <summary>
        /// Indicates whether the values of two specified <see cref="T:ValueObject"></see> objects are not equal.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns>true if <paramref name="left">left</paramref> and <paramref name="right">right</paramref> are not equal; otherwise, false.</returns>
        public static bool operator !=(ValueObject<T> left, ValueObject<T> right)
            => !(left == right);

        /// <summary>
        /// Determines whether the specified <see cref="T:ValueObject"></see> is equal to the current <see cref="T:ValueObject"></see>.
        /// </summary>
        /// <param name="other"><see cref="T:ValueObject"></see> to compare.</param>
        /// <returns>True if <paramref name="other">other</paramref> is equal to the current <see cref="T:ValueObject"></see>.</returns>
        public abstract bool Equals(T other);

        /// <inheritdoc />
        public abstract override int GetHashCode();
    }
}