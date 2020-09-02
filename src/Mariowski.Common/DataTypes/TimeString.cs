using System;

namespace Mariowski.Common.DataTypes
{
    public partial class TimeString : ValueObject<TimeString>, IEquatable<TimeString>
    {
        private readonly string _value;
        
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

            _value = value;
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:TimeString"></see> is equal to the current <see cref="T:TimeString"></see>.
        /// </summary>
        /// <param name="other"><see cref="T:TimeString"></see> to compare.</param>
        /// <returns>True if <paramref name="other">other</paramref> is equal to the current <see cref="T:TimeString"></see>.</returns>
        public override bool Equals(TimeString other)
        {
            if (ReferenceEquals(null, other)) return false;
            return ReferenceEquals(this, other) || TimeSpan.Equals(other.TimeSpan);
        }

        /// <inheritdoc />
        public override string ToString()
            => _value;
        
        /// <inheritdoc />
        public override int GetHashCode()
            => TimeSpan.GetHashCode();
    }
}