using System;

namespace Mariowski.Common.Exceptions
{
    public abstract class ValueObjectException : Exception
    {
        public abstract string Code { get; }

        /// <inheritdoc />
        protected ValueObjectException(string message)
            : base(message)
        {
        }
    }
}