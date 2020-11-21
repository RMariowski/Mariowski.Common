using Mariowski.Common.Exceptions;

namespace Mariowski.Common.DataTypes
{
    public partial record Email
    {
        public string Value { get; }

        /// <summary>
        /// Creates a new instance of email value object.
        /// </summary>
        /// <param name="value">Email as string</param>
        /// <exception cref="InvalidEmailException">The <paramref name="value"/> argument is not valid email.</exception>
        public Email(string value)
        {
            if (!IsValid(value))
                throw new InvalidEmailException(value);

            Value = value.ToLowerInvariant();
        }

        /// <summary>
        /// To satisfy EF
        /// </summary>
        protected Email()
        {
        }
    }
}