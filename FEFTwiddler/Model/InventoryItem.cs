using System;

namespace FEFTwiddler.Model
{
    public class InventoryItem : Item
    {
        public InventoryItem(byte[] raw) : base(raw)
        {
            if (raw.Length != 4) throw new ArgumentException("Inventory items must be 4 bytes");
        }

        public static InventoryItem FromID(Enums.Item itemId)
        {
            var raw = new byte[] { (byte)itemId, (byte)((ushort)itemId >> 8), 0x00, 0x00 };
            return new InventoryItem(raw);
        }

        /// <summary>
        /// A reference to the name of the weapon in the weapon name block.
        /// This value is two greater than the corresponding ID in the weapon name block for some reason (probably to differentiate it from 0).
        /// This returns the value as seen in the weapon name block (i.e. two is subtracted from it on get).
        /// </summary>
        public byte WeaponNameID
        {
            get { return (byte)(_raw[2] - 2); }
            set { _raw[2] = (byte)(value + 2); }
        }
        public bool IsNamed
        {
            get { return _raw[2] > 0; }
        }

        /// <summary>
        /// Doubles as forge level. 0x41 and up, I think
        /// </summary>
        public byte Uses
        {
            get
            {
                return (byte)((int)_raw[3] & ~0x40);
            }
            set
            {
                _raw[3] = (byte)(value | ((IsEquipped) ? (0x40) : (0x00)));
            }
        }
        
        public bool IsEquipped
        {
            get
            {
                return (_raw[3] & 0x40) == 0x40;
            }
            set
            {
                _raw[3] = (byte)(Uses | ((value) ? (0x40) : (~0x40)));
            }
        }

        public string Hex()
        {
            return string.Format("{0:X2}{1:X2}{2:X2}{3:X2}",
                new object[] { _raw[0], _raw[1], _raw[2], _raw[3]});
        }

        public void Reparse(byte[] raw)
        {
            if (raw.Length != 4) throw new ArgumentException("Inventory items must be 4 bytes");
            _raw = raw;
        }
    }
}
