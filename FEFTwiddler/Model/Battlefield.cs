using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEFTwiddler.Model
{
    public class Battlefield
    {
        private byte[] _raw;
        public byte[] Raw
        {
            get { return _raw; }
        }

        public Battlefield(byte[] raw)
        {
            if (raw.Length != 0x21) throw new ArgumentException("Battlefield Entries must be 33 (0x21) bytes");
            _raw = raw;
        }

        // TODO: Figure out what this value *really* is and finish the enum for it
        public byte BattlefieldID
        {
            get { return _raw[0x01]; }
            set { _raw[0x01] = value; }
        }

        public Enums.Chapter ChapterID
        {
            get { return (Enums.Chapter)_raw[0x02]; }
            set { _raw[0x02] = (byte)value; }
        }

        public Enums.BattlefieldStatus BattlefieldStatus
        {
            get { return (Enums.BattlefieldStatus)_raw[0x03]; }
            set { _raw[0x03] = (byte)value; }
        }
    }
}
