using System;

namespace FEFTwiddler.Model
{
    public class ShopItem : Item
    {
        public ShopItem(byte[] bytes) : base(bytes)
        {
            if (bytes.Length != 4) throw new ArgumentException("Shop items must be 4 bytes");
            CurrentNumberForSale = bytes[2];
            MaxNumberForSale = bytes[3];
        }

        public byte CurrentNumberForSale { get; set; }
        public byte MaxNumberForSale { get; set; }
    }
}
