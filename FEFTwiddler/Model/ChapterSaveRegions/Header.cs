using System;
using System.Linq;

namespace FEFTwiddler.Model.ChapterSaveRegions
{
    public class Header
    {
        public Header(byte[] raw)
        {
            _raw = raw;
        }

        private byte[] _raw;
        public byte[] Raw
        {
            get { return _raw; }
        }

        public const int Offset = 0x00000000;
        public const int Length = 0xC0;

        // Eleven unknown bytes (0x00 through 0x0B)

        public Enums.Game Game
        {
            get { return (Enums.Game)_raw[0x0C]; }
            set { _raw[0x0C] = (byte)value; }
        }

        // Three unknown bytes (0x0D through 0x0F)

        public string AvatarName
        {
            get { return Utils.TypeConverter.ToString(_raw.Skip(0x0F).Take(0x18).ToArray()); }
            set { Array.Copy(Utils.TypeConverter.ToByteArray(value, 0x0C), 0x00, _raw, 0x0F, 0x18); }
        }

        // 153 unknown bytes (0x27 through 0xBF)
    }
}
