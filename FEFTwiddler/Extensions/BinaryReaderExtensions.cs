using System.Linq;
using System.IO;

namespace FEFTwiddler.Extensions
{
    public static class BinaryReaderExtensions
    {
        /// <summary>
        /// Read until hitting the value.
        /// </summary>
        /// <remarks>What could possibly go wrong?</remarks>
        /// <exception cref="EndOfStreamException">The value is not found.</exception>
        public static void AdvanceToValue(this BinaryReader br, byte[] value)
        {
            if (value.Length == 0) return;

            try
            {
                byte[] match = new byte[value.Length];
                int position = 0;

                while (true)
                {
                    var curByte = br.ReadByte();

                    if (curByte == value[position])
                    {
                        match[position] = curByte;
                        if (Enumerable.SequenceEqual(match, value)) return;
                        position++;
                    }
                    else
                    {
                        match = new byte[value.Length];
                        position = 0;
                    }

                    if (position >= value.Length) position = 0;
                }
            }
            catch (EndOfStreamException)
            {
                throw new EndOfStreamException("Value not found");
            }
        }
    }
}
