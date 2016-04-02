using System;

namespace FEFTwiddler.Model
{
    public abstract class Item
    {
        private byte[] _bytes;

        public Item(byte[] bytes)
        {
            _bytes = bytes;

            ItemID = (Enums.Item)(_bytes[0] + _bytes[1] * 0x100);
        }

        public Enums.Item ItemID { get; set; }
    }
}
