using System;

namespace FEFTwiddler.Model
{
    public class ConvoyItem : Item
    {
        public ConvoyItem(byte[] raw) : base(raw)
        {
            if (raw.Length != 7) throw new ArgumentException("Convoy items must be 7 bytes");
        }

        public static ConvoyItem Create()
        {
            return new ConvoyItem(new byte[7] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });
        }

        // One byte (0x00)
        // Always 00?

        // This is shifted one byte compared to ShopItem and InventoryItem. Thanks, Obama
        public new Enums.Item ItemID
        {
            get
            {
                return (Enums.Item)(_raw[0x01] + _raw[0x02] * 0x100);
            }
            set
            {
                _raw[0x01] = (byte)(value);
                _raw[0x02] = (byte)((ushort)value >> 8);
            }
        }

        /// <summary>
        /// A reference to the name of the weapon in the weapon name block.
        /// This value is two greater than the corresponding ID in the weapon name block for some reason (probably to differentiate it from 0).
        /// This returns the value as seen in the weapon name block (i.e. two is subtracted from it on get).
        /// </summary>
        public byte WeaponNameID
        {
            get { return (byte)(_raw[0x03] - 2); }
            set { _raw[0x03] = (byte)(value + 2); }
        }
        public bool IsNamed
        {
            get { return _raw[0x03] > 0; }
        }

        /// <summary>
        /// Doubles as forge level. 0x41 and up, I think
        /// </summary>
        public byte Uses
        {
            get
            {
                return _raw[0x04];
            }
            set
            {
                _raw[0x04] = value;
            }
        }

        public byte Quantity
        {
            get
            {
                return _raw[0x05];
            }
            set
            {
                _raw[0x05] = value;
            }
        }

        // One byte (0x06)
        // Always 00? Unless quantity goes over 255, lol

        public override string ToString()
        {
            return string.Format("{0} x{1}", ItemID.ToString(), Quantity.ToString());
        }
    }
}
