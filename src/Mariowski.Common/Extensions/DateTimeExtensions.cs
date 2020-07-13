using System;

namespace Mariowski.Common.Extensions
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Gets unix timestamp in seconds based on date time.
        /// </summary>
        /// <param name="dateTime">The date time to act on.</param>
        /// <returns>Unix timestamp in seconds.</returns>
        public static long ToUnixTimestamp(this DateTime dateTime)
            => ((DateTimeOffset)dateTime).ToUnixTimeSeconds();
    }
}