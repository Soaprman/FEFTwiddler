using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEFTwiddler.Utils
{
    public class TypeConverter
    {
        /// <summary>
        /// Converts a string to an array of two-byte characters.
        /// </summary>
        /// <remarks>Currently only supports single-byte characters</remarks>
        public static byte[] ToByteArray(string str, int numberOfCharacters)
        {
            if (String.IsNullOrEmpty(str)) return new byte[0];

            var byteArray = new byte[numberOfCharacters * 2];
            for (var i = 0; i < str.Length; i++)
            {
                byteArray[i * 2] = (byte)str[i];
                byteArray[(i * 2) + 1] = 0x00;
            }
            return byteArray;
        }

        /// <summary>
        /// Converts an array of two-byte characters to a string.
        /// </summary>
        /// <remarks>Currently only supports single-byte characters</remarks>
        public static string ToString(byte[] byteArray)
        {
            var str = "";
            for (var i = 0; i < byteArray.Length; i += 2)
            {
                if (byteArray[i] == 0x00) return str; // 0x00 = no more characters in the string
                str += (char)byteArray[i];
            }
            return str;
        }
    }
}
