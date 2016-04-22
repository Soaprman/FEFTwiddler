using System;
using System.Collections.Generic;
using System.Linq;

namespace FEFTwiddler.Extensions
{
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Wraps this object in an IEnumerable
        /// </summary>
        public static IEnumerable<T> Yield<T>(this T item)
        {
            yield return item;
        }

        /// <summary>
        /// Returns a random elements from this collection
        /// </summary>
        public static T RandomElement<T>(this IEnumerable<T> collection, Random rng)
        {
            var index = rng.Next(0, collection.Count());
            return collection.ElementAt(index);
        }
    }
}
