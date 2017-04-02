using System;
using System.Linq;
using FEFTwiddler.Extensions;

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

        public uint TimeElapsed
        {
            get { return BitConverter.ToUInt32(_raw, 0x01); }
            set
            {
                _raw[0x01] = (byte)(value);
                _raw[0x02] = (byte)(value >> 8);
                _raw[0x03] = (byte)(value >> 16);
                _raw[0x04] = (byte)(value >> 24);
            }
        }

        public Enums.Chapter CurrentChapter
        {
            get { return (Enums.Chapter)_raw[0x05]; }
            set { _raw[0x05] = (byte)value; }
        }

        // Classic mode is _raw[0x06] = 0x00

        /// <summary>
        /// This flag is turned on until chapter 6 if playing on Classic mode.
        /// </summary>
        public bool UnitsGoAbsentWhenKilled
        {
            get { return _raw[0x06].GetFlag(0x01); }
            set { _raw[0x06] = _raw[0x06].SetFlag(0x01, value); }
        }

        /// <summary>
        /// I've seen this on battle prep saves. It might only apply there.
        /// </summary>
        public bool UnknownDeathPenalty_Flag_0x02
        {
            get { return _raw[0x06].GetFlag(0x02); }
            set { _raw[0x06] = _raw[0x06].SetFlag(0x02, value); }
        }

        public bool UnitsReviveAfterChapter
        {
            get { return _raw[0x06].GetFlag(0x04); }
            set { _raw[0x06] = _raw[0x06].SetFlag(0x04, value); }
        }

        public bool UnitsReviveAfterTurn
        {
            get { return _raw[0x06].GetFlag(0x08); }
            set { _raw[0x06] = _raw[0x06].SetFlag(0x08, value); }
        }

        /// <summary>
        /// I've seen this on saves made at the chapter 6 branch of fate
        /// </summary>
        public bool UnknownDeathPenalty_Flag_0x10
        {
            get { return _raw[0x06].GetFlag(0x10); }
            set { _raw[0x06] = _raw[0x06].SetFlag(0x10, value); }
        }

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

        // This is a guess but it seems to fit
        // I haven't found a corresponding value in the user block though
        // I have seen _raw[0x0D] equal 0x02 on a Map save
        public bool IsBattlePrepSave
        {
            get { return _raw[0x0D].GetFlag(0x01); }
            set { _raw[0x0D] = _raw[0x0D].SetFlag(0x01, value); }
        }

        // One unknown byte (0x0E)
        // 0x0E is always 00 from what I've seen

        public string AvatarName
        {
            get { return Utils.TypeConverter.ToString(_raw.Skip(0x0F).Take(0x18).ToArray()); }
            set { Array.Copy(Utils.TypeConverter.ToByteArray(value, 0x0C), 0x00, _raw, 0x0F, 0x18); }
        }

        // Two unknown bytes (0x27 through 0x28)
        // Probably always 00 00, separates avatar and chapter names

        // The length here is a total guess
        // Chapter Name (0x29 through 0x48) (example: "Chapter 6")
        public string ChapterName1
        {
            get { return Utils.TypeConverter.ToString(_raw.Skip(0x29).Take(0x20).ToArray()); }
            set { Array.Copy(Utils.TypeConverter.ToByteArray(value, 0x10), 0x00, _raw, 0x29, 0x20); }
        }

        // Chapter Name (0x49 through 0xBD) (example: "The Path Is Yours")
        public string ChapterName2
        {
            get { return Utils.TypeConverter.ToString(_raw.Skip(0x49).Take(0x76).ToArray()); }
            set { Array.Copy(Utils.TypeConverter.ToByteArray(value, 0x3B), 0x00, _raw, 0x49, 0x76); }
        }

        // One unknown byte (0xBF)
        // Probably always 00
    }
}
