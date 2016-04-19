using System;

namespace FEFTwiddler.Model
{
    public class ChapterHistoryEntry
    {
        private byte[] _raw;
        public byte[] Raw
        {
            get { return _raw; }
        }

        public ChapterHistoryEntry(byte[] raw)
        {
            if (raw.Length != 0x10) throw new ArgumentException("Chapter History Entries must be 16 (0x10) bytes");
            _raw = raw;
        }

        public Enums.Chapter ChapterID
        {
            get { return (Enums.Chapter)_raw[0x1]; }
            set { _raw[0x1] = (byte)value; }
        }

        public byte TurnCount
        {
            get { return _raw[0x2]; }
            set { _raw[0x2] = value; }
        }

        public Enums.Character HeroCharacterID_1
        {
            get { return (Enums.Character)(ushort)((_raw[0x9] * 0x100) + _raw[0x8]); }
            set
            {
                _raw[0x8] = (byte)(((ushort)value) & 0xFF);
                _raw[0x9] = (byte)(((ushort)value >> 8) & 0xFF);
            }
        }

        public Enums.Class HeroClassID_1
        {
            get { return (Enums.Class)_raw[0xA]; }
            set { _raw[0xA] = (byte)value; }
        }

        public Enums.Character HeroCharacterID_2
        {
            get { return (Enums.Character)(ushort)((_raw[0xD] * 0x100) + _raw[0xC]); }
            set
            {
                _raw[0xC] = (byte)(((ushort)value) & 0xFF);
                _raw[0xD] = (byte)(((ushort)value >> 8) & 0xFF);
            }
        }

        public Enums.Class HeroClassID_2
        {
            get { return (Enums.Class)_raw[0xE]; }
            set { _raw[0xE] = (byte)value; }
        }

        public override string ToString()
        {
            return ChapterID.ToString() + " / Turns: " + TurnCount.ToString() + " / Heroes: " + HeroCharacterID_1.ToString() + ", " + HeroCharacterID_2.ToString();
        }
    }
}
