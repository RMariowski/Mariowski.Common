using System;
using System.Collections.Generic;

namespace Mariowski.Common.Extensions
{
    public static class ListExtensions
    {
        /// <summary>
        /// Real shuffle of list.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="random">Random number generator to use.</param>
        public static void Shuffle<T>(this IList<T> @this, Random random = null)
        {
            if (random == null)
                random = new Random();

            int n = @this.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                var value = @this[k];
                @this[k] = @this[n];
                @this[n] = value;
            }
        }
    }
}