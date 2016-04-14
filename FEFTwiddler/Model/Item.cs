using System;

namespace FEFTwiddler.Model
{
    public abstract class Item
    {
        private byte[] _bytes;

        public Item(byte[] bytes)
        {
            _bytes = bytes;

            ReparseID(_bytes[0], _bytes[1]);
        }

        public void ReparseID(byte low, byte high)
        {
            ItemID = (Enums.Item)(low + high * 0x100);
        }

        public Enums.Item ItemID { get; set; }
    }
}
