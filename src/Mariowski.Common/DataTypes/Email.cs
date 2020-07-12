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
            => other != null && _value == other._value;

        /// <inheritdoc />
        public override bool Equals(object obj)
            => obj is Email other && Equals(other);

        /// <inheritdoc />
        public override int GetHashCode()
            => _value.GetHashCode();
    }
}