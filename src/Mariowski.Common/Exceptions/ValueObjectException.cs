using System;
using Mariowski.Common.Markers;

namespace Mariowski.Common.Exceptions
{
    public abstract class ValueObjectException : Exception, ICodeable
    {
        public abstract string Code { get; }

        /// <inheritdoc />
        protected ValueObjectException(string message)
            : base(message)
        {
        }
    }
}