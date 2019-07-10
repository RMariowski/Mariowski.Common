using System.Text.RegularExpressions;

namespace Mariowski.Common.Validators
{
    public static class MailAddressValidator
    {
        #region IsValid

        /// <summary>
        /// Checks whatever value has mail address format.
        /// </summary>
        /// <param name="value">Value to check</param>
        /// <returns>True if value has mail address format, false otherwise.</returns>
        public static bool IsValid(string value)
        {
            if (value == null)
                return false;

            const string pattern = "^([0-9a-zA-Z]" + //Start with a digit or alphabetical
                                   @"([\+\-_\.][0-9a-zA-Z]+)*" + // No continuous or ending +-_. chars in mail address
                                   ")+" +
                                   @"@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,17})$";
            return Regex.IsMatch(value, pattern);
        }

        #endregion
    }
}