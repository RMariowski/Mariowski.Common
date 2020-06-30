using System;

namespace Mariowski.Common.Extensions
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Gets unix timestamp in seconds based on date time.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>Unix timestamp in seconds.</returns>
        public static long ToUnixTimestamp(this DateTime @this)
            => ((DateTimeOffset)@this).ToUnixTimeSeconds();
    }
}