using System;

namespace FEFTwiddler.Model
{
    /// <summary>
    /// An unknown thirty-byte block that inhabits the My Castle region
    /// </summary>
    public class UnknownB
    {
        private byte[] _raw;
        public byte[] Raw
        {
            get { return _raw; }
        }

        public UnknownB(byte[] raw)
        {
            if (raw.Length != 0x1E) throw new ArgumentException("UnknownB Entries must be 30 (0x1E) bytes");
            _raw = raw;
        }
    }
}
