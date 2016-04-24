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

        // One unknown byte (0x04)
        // 0x04 is always 06?

        /// <summary>
        /// changed 2A > 4E between branch of fate and RV chapter 7
        /// Is 0x43 on a "new" save
        /// </summary>
        public byte Unknown_Block1_0x05
        {
            get { return _rawBlock1[0x05]; }
            set { _rawBlock1[0x05] = value; }
        }

        public byte[] TimeElapsed
        {
            get { return _rawBlock1.Skip(0x06).Take(0x03).ToArray(); }
            set { Array.Copy(value, 0x00, _rawBlock1, 0x06, 0x03); }
        }

        // These three look similar to elapsed time but they're not the same.
        public byte Unknown_Block1_0x0A
        {
            get { return _rawBlock1[0x0A]; }
            set { _rawBlock1[0x0A] = value; }
        }
        public byte Unknown_Block1_0x0B
        {
            get { return _rawBlock1[0x0B]; }
            set { _rawBlock1[0x0B] = value; }
        }
        public byte Unknown_Block1_0x0C
        {
            get { return _rawBlock1[0x0C]; }
            set { _rawBlock1[0x0C] = value; }
        }

        // One unknown byte (0x0C)
        // Always 00?

        // 0x06 changed 6C > 8F between branch of fate and RV chapter 7
        // 0x08 changed 45 > 34 between branch of fate and RV chapter 7
        // 0x09 changed 2D > 6F between branch of fate and RV chapter 7

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

        // 223 unknown bytes (0x00 through 0xDE)
        // (block of mostly 00s that starts with 00 80 and ends with 30 00 00 00)

        // These four are FF FF FF FF on my chapter 27 saves, but there are lower values on other saves.
        public byte Unknown_Block2_0xDF
        {
            get { return _rawBlock2[0xDF]; }
            set { _rawBlock2[0xDF] = value; }
        }
        public byte Unknown_Block2_0xE0
        {
            get { return _rawBlock2[0xE0]; }
            set { _rawBlock2[0xE0] = value; }
        }
        public byte Unknown_Block2_0xE1
        {
            get { return _rawBlock2[0xE1]; }
            set { _rawBlock2[0xE1] = value; }
        }
        public byte Unknown_Block2_0xE2
        {
            get { return _rawBlock2[0xE2]; }
            set { _rawBlock2[0xE2] = value; }
        }

        /// <summary>
        /// Values observed: 0x00, 0x01, 0x03
        /// </summary>
        public byte Unknown_Block2_0xE3
        {
            get { return _rawBlock2[0xE3]; }
            set { _rawBlock2[0xE3] = value; }
        }

        // One unknown byte (0xE4)
        // Always 00?

        // Classic mode is _rawBlock2[0xE5] = 0x00

        /// <summary>
        /// This flag is turned on until chapter 6 if playing on Classic mode.
        /// </summary>
        public bool UnitsGoAbsentWhenKilled
        {
            get { return _rawBlock2[0xE5].GetFlag(0x01); }
            set { _rawBlock2[0xE5] = _rawBlock2[0xE5].SetFlag(0x01, value); }
        }

        /// <summary>
        /// I've seen this on battle prep saves. It might only apply there.
        /// </summary>
        public bool UnknownDeathPenalty_Flag_0x02
        {
            get { return _rawBlock2[0xE5].GetFlag(0x02); }
            set { _rawBlock2[0xE5] = _rawBlock2[0xE5].SetFlag(0x02, value); }
        }

        public bool UnitsReviveAfterChapter
        {
            get { return _rawBlock2[0xE5].GetFlag(0x04); }
            set { _rawBlock2[0xE5] = _rawBlock2[0xE5].SetFlag(0x04, value); }
        }

        public bool UnitsReviveAfterTurn
        {
            get { return _rawBlock2[0xE5].GetFlag(0x08); }
            set { _rawBlock2[0xE5] = _rawBlock2[0xE5].SetFlag(0x08, value); }
        }

        /// <summary>
        /// I've seen this on saves made at the chapter 6 branch of fate
        /// </summary>
        public bool UnknownDeathPenalty_Flag_0x10
        {
            get { return _rawBlock2[0xE5].GetFlag(0x10); }
            set { _rawBlock2[0xE5] = _rawBlock2[0xE5].SetFlag(0x10, value); }
        }

        // Not sure what these three are.
        // 04 F0 C7 on my ch27 RV save
        // 01 00 00 on my "new" save
        // CHANGING THIS TO 01 00 00 DID IT. AFTER ALL THAT OTHER SHIT
        // WE RANDOMIZED NOW FAM. WE RANDOMIZED NOW
        public byte Unknown_Block2_0xE6
        {
            get { return _rawBlock2[0xE6]; }
            set { _rawBlock2[0xE6] = value; }
        }
        public byte Unknown_Block2_0xE7
        {
            get { return _rawBlock2[0xE7]; }
            set { _rawBlock2[0xE7] = value; }
        }
        public byte Unknown_Block2_0xE8
        {
            get { return _rawBlock2[0xE8]; }
            set { _rawBlock2[0xE8] = value; }
        }

        /// <summary>
        /// It's 0x04 on my battle prep saves.
        /// It's 0x01 on my revelation chapter 27 save.
        /// It's 0x02 on my birthright chapter 27 save.
        /// It's 0x01 on all my "new" saves.
        /// </summary>
        public byte Unknown_Block2_0xE9
        {
            get { return _rawBlock2[0xE9]; }
            set { _rawBlock2[0xE9] = value; }
        }

        public Enums.Difficulty Difficulty
        {
            get { return (Enums.Difficulty)_rawBlock2[0xEA]; }
            set { _rawBlock2[0xEA] = (byte)value; }
        }

        public Enums.Game Game
        {
            get { return (Enums.Game)_rawBlock2[0xEB]; }
            set { _rawBlock2[0xEB] = (byte)value; }
        }

        /// <summary>
        /// Might be story progress.
        /// It's 0x00 in my before-prologue save.
        /// It's 0x01 in my before-chapter1 save.
        /// It's 0x02 in my before-chapter2 save.
        /// It's 0x03 in my before-chapter3 save.
        /// It's 0x04 on 054\Chapter0, which is on ch 6 branch.
        /// It's 0x05 in EldinTokuro_Chapter0_dec, which is on revelation chapter 7.
        /// It's 0x05 on 054\Chapter2, which is on RV ch 7.
        /// It's 0x11 in hollablack2/Chapter1 save, which is on "chapter 16: pleasure palace".
        /// It's 0x26 on my chapter27 saves.
        /// </summary>
        public byte Unknown_Block2_0xEC
        {
            get { return _rawBlock2[0xEC]; }
            set { _rawBlock2[0xEC] = value; }
        }

        /// <summary>
        /// Might have to do with DLC stuff in possession.
        /// When I set it to 0x00, the game gave the "DLC is missing" message after booting a save.
        /// It's 0x03 on my three chapter 27 saves (regardless of game).
        /// It's 0x00 on my fresh save before the prologue.
        /// It's 0x01 on my pre-chapter 6 saves.
        /// It's 0x01 on EldinTokuro_Chapter0_dec.
        /// </summary>
        public byte Unknown_Block2_0xED
        {
            get { return _rawBlock2[0xED]; }
            set { _rawBlock2[0xED] = value; }
        }

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

        // Six unknown bytes (0xF2 through 0xF7)

        /// <summary>
        /// Might be DLC-related or region-related
        /// 0x01 on all saves except 9Hopper's save (which also has a 70KL section instead of an 80KL section)
        /// 9Hopper's save has Cipher DLC so it is prob a Japanese save?
        /// </summary>
        public byte Unknown_Block2_0xF8
        {
            get { return _rawBlock2[0xF8]; }
            set { _rawBlock2[0xF8] = value; }
        }

        // One unknown byte (0xF9)

        /// <summary>
        /// 0x1E on my "new" save
        /// 0x1C on my ch27 BR save
        /// 0x5E on my pre-ch6 saves
        /// 0x14 on my RV battle prep saves
        /// </summary>
        public byte Unknown_Block2_0xFA
        {
            get { return _rawBlock2[0xFA]; }
            set { _rawBlock2[0xFA] = value; }
        }

        /// <summary>
        /// 0x1E on my "new" save
        /// 0x16 on my ch27 BR save
        /// 0x5E on my pre-ch6 saves
        /// 0x5E on my RV battle prep saves
        /// </summary>
        public byte Unknown_Block2_0xFB
        {
            get { return _rawBlock2[0xFB]; }
            set { _rawBlock2[0xFB] = value; }
        }

        // Two unknown bytes (0xFC through 0xFD)

        /// <summary>
        /// It's 0x01 on saves before and immediately after the prologue.
        /// It's also 0x01 on EldinTokuro's save.
        /// It's 0x00 on other saves.
        /// </summary>
        public byte Unknown_Block2_0xFE
        {
            get { return _rawBlock2[0xFE]; }
            set { _rawBlock2[0xFE] = value; }
        }

        // Two unknown bytes (0xFF through 0x100)

        /// <summary>
        /// It is 0x01 on saves before the prologue.
        /// It is 0x00 on other saves.
        /// </summary>
        public byte Unknown_Block2_0x101
        {
            get { return _rawBlock2[0x101]; }
            set { _rawBlock2[0x101] = value; }
        }

        /// <summary>
        /// It is 0x02 on saves before the prologue. (example: 044\Chapter0)
        /// It is 0x01 on 044\Chapter1 and 044\Chapter2.
        /// It is 0x01 on my chapter 27 revelation saves.
        /// It is 0x02 on my chapter 27 birthright save.
        /// </summary>
        public byte Unknown_Block2_0x102
        {
            get { return _rawBlock2[0x102]; }
            set { _rawBlock2[0x102] = value; }
        }

        // Three unknown bytes (0x103 through 0x105)
        // Always 00 00 80?

        // Four unknown bytes (0x106 through 0x109)
        // Always FF FF FF FF?

        /// <summary>
        /// Seems related to story progress.
        /// It's 0x24 in my chapter 27 saves (regardless of game).
        /// It's 0x00 in my pre-chapter 6 saves.
        /// It's 0x10 in EldinTokuro_Chapter0_dec.
        /// It's 0x08 in hollablack2\Chapter1_dec.
        /// </summary>
        public byte Unknown_Block2_0x10A
        {
            get { return _rawBlock2[0x10A]; }
            set { _rawBlock2[0x10A] = value; }
        }

        // Three unknown bytes (0x10B through 0x10D)
        // Always 00 01 04, I think.

        // Sixty-four unknown bytes (0x10E through 0x14D)
        // This seems like four sets of sixteen random values.
        // When I saved in a separate slot after being each chapter, the middle two "rows" had the same values, but the top and bottom "rows" changed.
        // There probably isn't anything interesting in here.

        #endregion
    }
}
