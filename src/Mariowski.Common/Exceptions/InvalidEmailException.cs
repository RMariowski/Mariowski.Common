namespace Mariowski.Common.Exceptions
{
    public class InvalidEmailException : CodeableException
    {
        public override string Code => "invalid_email";

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidEmailException"/> class with
        /// a specified invalid value for message.
        /// </summary>
        /// <param name="value">Invalid value</param>
        public InvalidEmailException(string value)
            : base($"{value} is not valid email address")
        {
        }
    }
}