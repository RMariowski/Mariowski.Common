using System.Text.RegularExpressions;
using Mariowski.Common.Exceptions;

namespace Mariowski.Common.DataTypes
{
    public partial class Email
    {
        /// <summary>
        /// Checks whatever value has mail address format.
        /// </summary>
        /// <param name="value">Value to check</param>
        /// <returns>True if value has mail address format, false otherwise.</returns>
        public static bool IsValid(string value)
        {
            if (value is null)
                return false;

            const string pattern = "^([0-9a-zA-Z]" + // Start with a digit or alphabetical
                                   @"([\+\-_\.][0-9a-zA-Z]+)*" + // No continuous or ending +-_. chars in mail address
                                   ")+" +
                                   @"@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,17})$";
            return Regex.IsMatch(value, pattern);
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
    }
}