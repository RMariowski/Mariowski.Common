using System;
using Mariowski.Common.Markers;

namespace Mariowski.Common.Exceptions
{
    public abstract class CodeableException : Exception, ICodeable
    {
        public abstract string Code { get; }

        /// <inheritdoc />
        protected CodeableException(string message)
            : base(message)
        {
        }
    }
}