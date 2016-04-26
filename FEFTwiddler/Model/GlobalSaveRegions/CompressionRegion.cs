using System;

namespace FEFTwiddler.Model.GlobalSaveRegions
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

        public const int Offset = 0x00000000;
        public const int Length = 0x84;

        // EDNI (four bytes) (0x00 through 0x03)

        public int Region1Offset
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

        public int LogbookRegionOffset
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

        // Four unknown bytes (0x0C through 0x0F)
        // These are always 00s I think
    }
}
