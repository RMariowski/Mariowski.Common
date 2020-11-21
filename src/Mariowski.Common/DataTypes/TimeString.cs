using System;

namespace Mariowski.Common.DataTypes
{
    public partial record TimeString
    {
        public string Value { get; }

        public TimeSpan TimeSpan { get; }

        /// <summary>
        /// Creates a new instance of TimeString.
        /// </summary>
        /// <param name="value">String to parse</param>
        /// <exception cref="ArgumentException"><paramref name="value">value</paramref> is null or white space</exception>
        /// <exception cref="T:System.FormatException"><paramref name="value">value</paramref> has unknown format</exception>
        public TimeString(string value)
        {
            TimeSpan = Parse(value);
            Value = value;
        }

        /// <summary>
        /// To satisfy EF
        /// </summary>
        protected TimeString()
        {
        }
    }
}