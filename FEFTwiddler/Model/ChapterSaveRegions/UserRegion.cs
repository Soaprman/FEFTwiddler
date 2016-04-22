using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FEFTwiddler.Extensions;

namespace FEFTwiddler.Model.ChapterSaveRegions
{
    public class UserRegion
    {
        public UserRegion(byte[] raw)
        {
            Raw = raw;
        }

        #region Raw Data

        public byte[] Raw
        {
            get
            {
                return RawBlock1
                    .Concat(RawChapterHistoryCount.Yield())
                    .Concat(RawChapterHistory)
                    .Concat(RawBlock2)
                    .ToArray();
            }
            set
            {
                using (var ms = new MemoryStream(value))
                using (var br = new BinaryReader(ms))
                {
                    RawBlock1 = br.ReadBytes(RawBlock1Length);

                    ChapterHistory = new List<ChapterHistoryEntry>();
                    var chapterCount = br.ReadByte();
                    for (var i = 0; i < chapterCount; i++)
                    {
                        ChapterHistory.Add(new ChapterHistoryEntry(br.ReadBytes(0x10)));
                    }

                    RawBlock2 = br.ReadBytes(RawBlock2Length);
                }
            }
        }

        public const int RawBlock1Length = 0x12;
        private byte[] _rawBlock1;
        public byte[] RawBlock1
        {
            get { return _rawBlock1; }
            set
            {
                if (value.Length != RawBlock1Length) throw new ArgumentException("UserRegion block 1 must be " + RawBlock1Length + " bytes in length");
                _rawBlock1 = value;
            }
        }

        public byte RawChapterHistoryCount
        {
            get { return (byte)ChapterHistory.Count; }
        }

        public byte[] RawChapterHistory
        {
            get
            {
                IEnumerable<byte> rawChapterHistory = new List<byte>();
                foreach (var entry in ChapterHistory)
                {
                    rawChapterHistory = rawChapterHistory.Concat(entry.Raw);
                }
                return rawChapterHistory.ToArray();
            }
        }

        public const int RawBlock2Length = 0x14E;
        private byte[] _rawBlock2;
        public byte[] RawBlock2
        {
            get { return _rawBlock2; }
            set
            {
                if (value.Length != RawBlock2Length) throw new ArgumentException("UserRegion block 2 must be " + RawBlock2Length + " bytes in length");
                _rawBlock2 = value;
            }
        }

        #endregion

        #region Block 1 Properties

        // RESU (four bytes) (0x00 through 0x03)

        // Nine unknown bytes (0x04 through 0x0C)

        public Enums.Chapter CurrentChapter
        {
            get { return (Enums.Chapter)_rawBlock1[0x0D]; }
            set { _rawBlock1[0x0D] = (byte)value; }
        }

        // Four unknown bytes (0x0E through 0x11)

        #endregion

        #region ChapterHistory Properties

        // One byte - ChapterHistoryCount (0x12)

        public List<ChapterHistoryEntry> ChapterHistory { get; set; }

        #endregion

        #region Block 2 Properties

        // 225 unknown bytes (0x00 through 0xE0)
        // (block of mostly 00s that starts with 00 80 and ends with 30 00 00 00 FF FF)

        // Four unknown bytes (0xE1 through 0xE4)

        public Enums.DeathPenalty DeathPenalty
        {
            get { return (Enums.DeathPenalty)_rawBlock2[0xE5]; }
            set { _rawBlock2[0xE5] = (byte)value; }
        }

        // Four unknown bytes (0xE6 through 0xE9)

        public Enums.Difficulty Difficulty
        {
            get { return (Enums.Difficulty)_rawBlock2[0xEA]; }
            set { _rawBlock2[0xEA] = (byte)value; }
        }

        // Three unknown bytes (0xEB through 0xED)

        public uint Gold
        {
            get { return BitConverter.ToUInt32(_rawBlock2, 0xEE); }
            set
            {
                _rawBlock2[0xEE] = (byte)(value);
                _rawBlock2[0xEF] = (byte)(value >> 8);
                _rawBlock2[0xF0] = (byte)(value >> 16);
                _rawBlock2[0xF1] = (byte)(value >> 24);
            }
        }

        // Twenty unknown bytes (0xF2 through 0x105)

        // Four unknown bytes (0x106 through 0x109)
        // Always FF FF FF FF?

        // Sixty-eight unknown bytes (0x10A through 0x14E)

        #endregion
    }
}
