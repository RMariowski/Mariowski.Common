using System;
using System.Linq;
using System.Text.RegularExpressions;
using Mariowski.Common.Extensions;

namespace Mariowski.Common.DataTypes
{
    public partial class TimeString
    {
        private static readonly Regex DaysRegex;
        private static readonly Regex HoursRegex;
        private static readonly Regex MinutesRegex;
        private static readonly Regex SecondsRegex;
        private static readonly Regex MillisecondsRegex;

        /// <summary>
        /// Initializes all regular expressions that are needed to parse string.
        /// </summary>
        static TimeString()
        {
            const RegexOptions compiledWithIgnoreCase = RegexOptions.Compiled | RegexOptions.IgnoreCase;

            DaysRegex = new Regex(@"^(days|day|d)", compiledWithIgnoreCase);
            HoursRegex = new Regex(@"^(hours|hour|h)", compiledWithIgnoreCase);
            MinutesRegex = new Regex(@"^(mins|min|m)", compiledWithIgnoreCase);
            SecondsRegex = new Regex(@"^(secs|sec|s)", compiledWithIgnoreCase);
            MillisecondsRegex = new Regex(@"^(ms)", compiledWithIgnoreCase);
        }

        /// <summary>
        /// Converts time string to <see cref="T:System.TimeSpan">TimeSpan</see>.
        /// </summary>
        /// <param name="timeString">String to parse</param>
        /// <returns>Representation of <paramref name="timeString">timeString</paramref> as <see cref="T:System.TimeSpan">TimeSpan</see></returns>
        /// <exception cref="ArgumentException"><paramref name="timeString">timeString</paramref> is null or white space</exception>
        /// <exception cref="T:System.FormatException"><paramref name="timeString">timeString</paramref> has unknown format</exception>
        public static TimeSpan Parse(string timeString)
        {
            if (string.IsNullOrWhiteSpace(timeString))
                throw new ArgumentException("Is null or white space", nameof(timeString));

            var timeSpan = TimeSpan.Zero;
            var currentString = string.Empty;
            var currentNumber = string.Empty;

            void Process()
            {
                if (string.IsNullOrWhiteSpace(currentNumber))
                    throw new FormatException($"Unknown string part {currentString}");

                int value = currentNumber.ToInt();

                if (string.IsNullOrEmpty(currentString) || MillisecondsRegex.IsMatch(currentString))
                {
                    timeSpan = timeSpan.Add(TimeSpan.FromMilliseconds(value));
                }
                else if (DaysRegex.IsMatch(currentString))
                {
                    timeSpan = timeSpan.Add(TimeSpan.FromDays(value));
                }
                else if (HoursRegex.IsMatch(currentString))
                {
                    timeSpan = timeSpan.Add(TimeSpan.FromHours(value));
                }
                else if (MinutesRegex.IsMatch(currentString))
                {
                    timeSpan = timeSpan.Add(TimeSpan.FromMinutes(value));
                }
                else if (SecondsRegex.IsMatch(currentString))
                {
                    timeSpan = timeSpan.Add(TimeSpan.FromSeconds(value));
                }
                else
                {
                    throw new FormatException($"Unknown string part {currentString}");
                }

                currentString = string.Empty;
                currentNumber = string.Empty;
            }

            foreach (char c in timeString.Where(c => !char.IsWhiteSpace(c)))
            {
                if (char.IsDigit(c))
                {
                    if (!string.IsNullOrEmpty(currentString))
                        Process();

                    currentNumber += c;
                    continue;
                }

                if (char.IsLetter(c))
                    currentString += c;
            }

            Process();

            return timeSpan;
        }
    }
}