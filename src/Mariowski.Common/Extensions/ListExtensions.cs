using System;
using System.Collections.Generic;

namespace Mariowski.Common.Extensions
{
    public static class ListExtensions
    {
        /// <summary>
        /// Real shuffle of list.
        /// </summary>
        /// <param name="list">The list to act on.</param>
        /// <param name="random">Random number generator to use.</param>
        public static void Shuffle<T>(this IList<T> list, Random random = null)
        {
            random ??= new Random();

            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                var value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}