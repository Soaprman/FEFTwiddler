using System.Collections.Generic;

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
    }
}
