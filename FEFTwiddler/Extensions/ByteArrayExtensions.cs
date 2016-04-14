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

        public static bool TryParseHex(this byte[] target, String hex)
        {
            if(target.Length * 2 != hex.Length)
            { return false; }

            byte[] temp = new byte[target.Length];

            for (int x = 0; x < target.Length; x++)
            {
                if(! Byte.TryParse(hex.Substring(x * 2, 2), System.Globalization.NumberStyles.HexNumber, null, out temp[x]))
                {
                    return false;
                }
            }

            for(int x = 0; x < target.Length; x++)
            {
                target[x] = temp[x];
            }

            return true;
        }
    }
}
