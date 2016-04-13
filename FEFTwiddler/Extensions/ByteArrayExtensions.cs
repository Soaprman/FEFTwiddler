using System;

namespace FEFTwiddler.Extensions
{
    public static class ByteArrayExtensions
    {
        /// <summary>
        /// Perform an AND NOT operation on all bytes in the first array with all bytes in the second array.
        /// </summary>
        /// <exception cref="ArgumentException">If thoseBytes is null</exception>
        public static byte[] AndNot(this byte[] theseBytes, byte[] thoseBytes)
        {
            if (thoseBytes == null) throw new ArgumentException("thoseBytes is null");
            var len = Math.Min(theseBytes.Length, thoseBytes.Length);
            for (var i = 0; i < len; i++)
            {
                theseBytes[i] = (byte)(theseBytes[i] & ~thoseBytes[i]);
            }
            return theseBytes;
        }

        /// <summary>
        /// Perform an OR operation on all bytes in the first array with all bytes in the second array.
        /// </summary>
        /// <exception cref="ArgumentException">If thoseBytes is null</exception>
        public static byte[] Or(this byte[] theseBytes, byte[] thoseBytes)
        {
            if (thoseBytes == null) throw new ArgumentException("thoseBytes is null");
            var len = Math.Min(theseBytes.Length, thoseBytes.Length);
            for (var i = 0; i < len; i++)
            {
                theseBytes[i] = (byte)(theseBytes[i] | thoseBytes[i]);
            }
            return theseBytes;
        }
    }
}
