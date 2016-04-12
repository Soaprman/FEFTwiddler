using System;

namespace FEFTwiddler.Model
{
    public class ConvoyItem : Item
    {
        public ConvoyItem(byte[] bytes) : base(bytes)
        {
            if (bytes.Length != 7) throw new ArgumentException("Convoy items must be 4 bytes");
            Unknown03 = bytes[2];
            Uses = bytes[3];
            Quantity = bytes[4];
            Unknown06 = bytes[5];
            Unknown07 = bytes[6];
        }

        public byte Unknown03 { get; set; }
        /// <summary>
        /// Doubles as forge level. 0x41 and up, I think
        /// </summary>
        public byte Uses { get; set; }
        public byte Quantity { get; set; }
        public byte Unknown06 { get; set; }
        public byte Unknown07 { get; set; }
    }
}
