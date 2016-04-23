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

        // One byte (0x00)
        // Always 0x01

        // Four unknown bytes (0x01 through 0x04)
        // Probably time elapsed

        public Enums.Chapter CurrentChapter
        {
            get { return (Enums.Chapter)_raw[0x05]; }
            set { _raw[0x05] = (byte)value; }
        }

        // One byte (0x06)
        // DeathPenalty

        /// <summary>
        /// is 0xF0 on all but my less than ch 6 saves (where it is 0x00)
        /// </summary>
        public byte Unknown_0x07
        {
            get { return _raw[0x07]; }
            set { _raw[0x07] = value; }
        }

        /// <summary>
        /// is 0x00 on my less than ch 6 saves
        /// is 0xC3 on my BR and battle prep saves
        /// is 0xC7 on my RV save
        /// </summary>
        public byte Unknown_0x08
        {
            get { return _raw[0x08]; }
            set { _raw[0x08] = value; }
        }

        // Two unknown bytes (0x09 through 0x0A)
        // 0x09 is always 00 from what I've seen
        // 0x0A is UserRegion.Unknown_Block2_0xE9

        public Enums.Game Game
        {
            get { return (Enums.Game)_raw[0x0B]; }
            set { _raw[0x0B] = (byte)value; }
        }

        public Enums.Difficulty Difficulty
        {
            get { return (Enums.Difficulty)_raw[0x0C]; }
            set { _raw[0x0C] = (byte)value; }
        }

        // Two unknown bytes (0x0D through 0x0E)
        // 0x0D is probably IsBattlePrepSave (0x01 on battle prep saves, 0x00 otherwise)
        // 0x0E is always 00 from what I've seen

        public string AvatarName
        {
            get { return Utils.TypeConverter.ToString(_raw.Skip(0x0F).Take(0x18).ToArray()); }
            set { Array.Copy(Utils.TypeConverter.ToByteArray(value, 0x4B), 0x00, _raw, 0x0F, 0x18); }
        }

        // Two unknown bytes (0x27 through 0x28)
        // Probably always 00 00, separates avatar and chapter names

        // The length here is a total guess
        // Chapter Name (0x29 through 0xBD)
        public string ChapterName
        {
            get { return Utils.TypeConverter.ToString(_raw.Skip(0x29).Take(0x96).ToArray()); }
            set { Array.Copy(Utils.TypeConverter.ToByteArray(value, 0x4B), 0x00, _raw, 0x29, 0x96); }
        }

        // One unknown byte (0xBF)
        // Probably always 00
    }
}
