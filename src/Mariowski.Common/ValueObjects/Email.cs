using System;
using Mariowski.Common.Exceptions;
using Mariowski.Common.Validators;

namespace Mariowski.Common.ValueObjects
{
    public class Email : IEquatable<Email>
    {
        private readonly string _value;

        /// <summary>
        /// Creates a new instance of email value object.
        /// </summary>
        /// <param name="value">Email as string</param>
        /// <exception cref="InvalidEmailException">The <paramref name="value"/> argument is not valid email.</exception>
        public Email(string value)
        {
            if (!MailAddressValidator.IsValid(value))
                throw new InvalidEmailException(value);

            _value = value.ToLowerInvariant();
        }

        /// <summary>
        /// Converts string to email value object.
        /// </summary>
        /// <param name="value">Email as string</param>
        /// <exception cref="InvalidEmailException">The <paramref name="value"/> argument is not valid email.</exception>
        /// <returns>New instance of email value object.</returns>
        public static implicit operator Email(string value)
            => new Email(value);

        /// <summary>
        /// Converts email value object to string.
        /// </summary>
        /// <param name="email">Email value object</param>
        /// <returns>Email as string</returns>
        public static implicit operator string(Email email)
            => email._value;

        /// <inheritdoc />
        public bool Equals(Email other)
            => _value == other._value;

        /// <inheritdoc />
        public override bool Equals(object obj)
            => obj is Email other && Equals(other);

        /// <inheritdoc />
        public override int GetHashCode()
            => _value.GetHashCode();
    }
}