namespace FEFTwiddler.Model
{
    public abstract class Item
    {
        protected byte[] _raw;
        public byte[] Raw
        {
            get { return _raw; }
        }

        public Item(byte[] raw)
        {
            _raw = raw;
        }

        public Enums.Item ItemID
        {
            get
            {
                return (Enums.Item)(_raw[0] + _raw[1] * 0x100);
            }
            set
            {
                _raw[0] = (byte)(((int)value) & 0xFF);        // I am so sorry
                _raw[1] = (byte)((((int)value) >> 8) & 0xFF); // So very sorry
            }
        }
    }
}
