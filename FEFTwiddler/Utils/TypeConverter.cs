using System;
using System.Text;

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
            byteArray = Encoding.Unicode.GetBytes(str);
            return byteArray;
        }

        /// <summary>
        /// Converts an array of two-byte characters to a string.
        /// </summary>
        public static string ToString(byte[] byteArray)
        {
            int length = 0;
            while (BitConverter.ToInt16(byteArray, length) != 0)
                length += 2;
            return Encoding.Unicode.GetString(byteArray, 0, length);
        }
    }
}
