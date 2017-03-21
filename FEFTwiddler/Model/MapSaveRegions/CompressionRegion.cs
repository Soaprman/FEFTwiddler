using System;

namespace FEFTwiddler.Model.MapSaveRegions
{
    public class CompressionRegion
    {
        public CompressionRegion(byte[] raw)
        {
            _raw = raw;
        }

        private byte[] _raw;
        public byte[] Raw
        {
            get { return _raw; }
        }

        public const int Offset = 0x000000C0;
        public const int Length = 0x84;

        // EDNI (four bytes) (0x00 through 0x03)

        public int UserOffset
        {
            get { return BitConverter.ToInt32(_raw, 0x04); }
            set
            {
                _raw[0x04] = (byte)(value);
                _raw[0x05] = (byte)(value >> 8);
                _raw[0x06] = (byte)(value >> 16);
                _raw[0x07] = (byte)(value >> 24);
            }
        }

        public int PERSOffset
        {
            get { return BitConverter.ToInt32(_raw, 0x08); }
            set
            {
                _raw[0x08] = (byte)(value);
                _raw[0x09] = (byte)(value >> 8);
                _raw[0x0A] = (byte)(value >> 16);
                _raw[0x0B] = (byte)(value >> 24);
            }
        }

        public int LK08Offset
        {
            get { return BitConverter.ToInt32(_raw, 0x0C); }
            set
            {
                _raw[0x0C] = (byte)(value);
                _raw[0x0D] = (byte)(value >> 8);
                _raw[0x0E] = (byte)(value >> 16);
                _raw[0x0F] = (byte)(value >> 24);
            }
        }

        public int BattlefieldOffset
        {
            get { return BitConverter.ToInt32(_raw, 0x10); }
            set
            {
                _raw[0x10] = (byte)(value);
                _raw[0x11] = (byte)(value >> 8);
                _raw[0x12] = (byte)(value >> 16);
                _raw[0x13] = (byte)(value >> 24);
            }
        }

        public int ShopOffset
        {
            get { return BitConverter.ToInt32(_raw, 0x14); }
            set
            {
                _raw[0x14] = (byte)(value);
                _raw[0x15] = (byte)(value >> 8);
                _raw[0x16] = (byte)(value >> 16);
                _raw[0x17] = (byte)(value >> 24);
            }
        }

        public int UnitOffset
        {
            get { return BitConverter.ToInt32(_raw, 0x18); }
            set
            {
                _raw[0x18] = (byte)(value);
                _raw[0x19] = (byte)(value >> 8);
                _raw[0x1A] = (byte)(value >> 16);
                _raw[0x1B] = (byte)(value >> 24);
            }
        }

        public int WeaponNameOffset
        {
            get { return BitConverter.ToInt32(_raw, 0x1C); }
            set
            {
                _raw[0x1C] = (byte)(value);
                _raw[0x1D] = (byte)(value >> 8);
                _raw[0x1E] = (byte)(value >> 16);
                _raw[0x1F] = (byte)(value >> 24);
            }
        }

        public int ConvoyOffset
        {
            get { return BitConverter.ToInt32(_raw, 0x20); }
            set
            {
                _raw[0x20] = (byte)(value);
                _raw[0x21] = (byte)(value >> 8);
                _raw[0x22] = (byte)(value >> 16);
                _raw[0x23] = (byte)(value >> 24);
            }
        }

        public int MyCastleOffset
        {
            get { return BitConverter.ToInt32(_raw, 0x24); }
            set
            {
                _raw[0x24] = (byte)(value);
                _raw[0x25] = (byte)(value >> 8);
                _raw[0x26] = (byte)(value >> 16);
                _raw[0x27] = (byte)(value >> 24);
            }
        }

        public int MapOffset
        {
            get { return BitConverter.ToInt32(_raw, 0x28); }
            set
            {
                _raw[0x28] = (byte)(value);
                _raw[0x29] = (byte)(value >> 8);
                _raw[0x2A] = (byte)(value >> 16);
                _raw[0x2B] = (byte)(value >> 24);
            }
        }

        // Eighty-eight unknown bytes (0x2C through 0x83)
        // These are always 00s I think
    }
}
