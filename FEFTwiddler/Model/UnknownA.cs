using System;

namespace FEFTwiddler.Model
{
    /// <summary>
    /// An unknown four-byte block that inhabits the My Castle region
    /// </summary>
    public class UnknownA
    {
        private byte[] _raw;
        public byte[] Raw
        {
            get { return _raw; }
        }

        public UnknownA(byte[] raw)
        {
            if (raw.Length != 0x04) throw new ArgumentException("UnknownA Entries must be 4 (0x04) bytes");
            _raw = raw;
        }
    }
}
