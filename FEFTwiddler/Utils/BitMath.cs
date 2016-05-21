using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEFTwiddler.Utils
{
    public class BitMath
    {
        /// <summary>
        /// Get the minimum number of bytes needed to hold the given number of bits
        /// </summary>
        public static int ByteCountNeededForBitCount(int bitCount)
        {
            return (bitCount / 8) + (bitCount % 8 != 0 ? 1 : 0);
        }
    }
}
