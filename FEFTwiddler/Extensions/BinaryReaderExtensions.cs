using System.Linq;
using System.IO;

namespace FEFTwiddler.Extensions
{
    public static class BinaryReaderExtensions
    {
        /// <summary>
        /// Read until hitting the value.
        /// </summary>
        /// <remarks>What could possibly go wrong? As it turns out, quite a bit.</remarks>
        /// <exception cref="EndOfStreamException">The value is not found.</exception>
        public static void AdvanceToValue(this BinaryReader br, byte[] value)
        {
            if (value.Length == 0) return;

            try
            {
                byte[] match = new byte[value.Length];
                byte[][] image = new byte[value.Length][];

                for (int x = 0; x < value.Length; x++)
                {
                    image[x] = new byte[value.Length];
                    for (int y = 0; y < value.Length; y++)
                    {
                        image[x][(y + x) % value.Length] = value[y];
                    }
                }

                int offset = value.Length - 1;

                for (int x = 0; x < offset; x++)
                {
                    match[x] = br.ReadByte();
                }
                
                while (true)
                {
                    match[offset] = br.ReadByte();

                    offset = (offset + 1) % value.Length;

                    if (Enumerable.SequenceEqual(match, image[offset])) return;
                }
            }
            catch (EndOfStreamException)
            {
                throw new EndOfStreamException("Value not found");
            }
        }
    }
}
