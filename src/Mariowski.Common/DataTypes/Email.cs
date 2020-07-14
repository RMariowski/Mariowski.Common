using System;
using Mariowski.Common.Exceptions;

namespace Mariowski.Common.DataTypes
{
    public partial class Email : IEquatable<Email>
    {
        private readonly string _value;

        /// <summary>
        /// Creates a new instance of email value object.
        /// </summary>
        /// <param name="value">Email as string</param>
        /// <exception cref="InvalidEmailException">The <paramref name="value"/> argument is not valid email.</exception>
        public Email(string value)
        {
            if (!IsValid(value))
                throw new InvalidEmailException(value);

            _value = value.ToLowerInvariant();
        }

        /// <inheritdoc />
        public bool Equals(Email other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _value == other._value;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Email)obj);
        }

        /// <inheritdoc />
        public override string ToString()
            => _value;

        /// <inheritdoc />
        public override int GetHashCode()
            => _value != null ? _value.GetHashCode() : 0;
    }
}