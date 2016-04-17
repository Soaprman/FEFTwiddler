using System;

namespace FEFTwiddler.Model
{
    public class ShopItem : Item
    {
        public ShopItem(byte[] raw) : base(raw)
        {
            if (raw.Length != 4) throw new ArgumentException("Shop items must be 4 bytes");
        }

        public byte CurrentNumberForSale
        {
            get { return _raw[2]; }
            set { _raw[2] = value; }
        }

        public byte MaxNumberForSale
        {
            get { return _raw[3]; }
            set { _raw[3] = value; }
        }
    }
}
