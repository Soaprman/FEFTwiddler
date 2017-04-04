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

        public uint TimeElapsed
        {
            get { return BitConverter.ToUInt32(_rawBlock1, 0x05); }
            set
            {
                _rawBlock1[0x05] = (byte)(value);
                _rawBlock1[0x06] = (byte)(value >> 8);
                _rawBlock1[0x07] = (byte)(value >> 16);
                _rawBlock1[0x08] = (byte)(value >> 24);
            }
        }

        // These four look similar to elapsed time but they're not the same.
        public byte Unknown_Block1_0x09
        {
            get { return _rawBlock1[0x09]; }
            set { _rawBlock1[0x09] = value; }
        }
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
        // Unknown_Block2_0x05 = 8 then 12 after passing a turn in chapter 2 and a new tutorial appeared.

        // These four are FF FF FF FF on my chapter 27 saves, but there are lower values on other saves.
        // Chapter1_FreshChapter7_dec: FF FF 1F 3B
        // They are likely flags set by doing one-time-only stuff in battles over the course of the game.
        public byte Unknown_Block2_0xDF
        {
            get { return _rawBlock2[0xDF]; }
            set { _rawBlock2[0xDF] = value; }
        }
        // In chapter 2, this is 0x40 after you've talked to Felicia and 0x60 after you've talked to Gunter. It's 0x96 as soon as you are allowed to save on the following turn
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
        // F0 C7 00 on my ch27 RV save
        // 00 00 00 on my "new" save
        // F0 C3 00 on "Fate.bak"
        // F0 03 00 on some save that boots to My Castle
        // Changing these to 00 00 00 successfully kicked a save back to the prologue.
        // Also, changing these to 00 00 00 worked for re-enabling branch of fate.
        // Just unsetting the first F0 also works for branch of fate.
        public byte Unknown_Block2_0xE6
        {
            get { return _rawBlock2[0xE6]; }
            set { _rawBlock2[0xE6] = value; }
        }
        // C7 on my castle RV ch 27
        // C3 on battle prep RV ch 27
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
        /// It's 0x05 on my map saves.
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

        // Putting this here because it's as good a place as anywhere.
        // The "Change BGM" setting is unaccounted for.

        // A setting. Changed from 14 (in 075_0) to CB (in 075_1)
        /// <summary>
        /// 0x1E on my "new" save
        /// 0x1C on my ch27 BR save
        /// 0x5E on my pre-ch6 saves
        /// 0x14 on my RV battle prep saves
        /// </summary>
        public byte SettingsFlags_0xFA
        {
            get { return _rawBlock2[0xFA]; }
            set { _rawBlock2[0xFA] = value; }
        }

        // A setting. Changed from 5E (in 075_0) to 80 (in 075_1)
        /// <summary>
        /// 0x1E on my "new" save
        /// 0x16 on my ch27 BR save
        /// 0x5E on my pre-ch6 saves
        /// 0x5E on my RV battle prep saves
        /// </summary>
        public byte SettingsFlags_0xFB
        {
            get { return _rawBlock2[0xFB]; }
            set { _rawBlock2[0xFB] = value; }
        }

        // A setting. Changed from 01 (in 075_0) to 00 (in 075_1)
        public byte Setting_0xFC
        {
            get { return _rawBlock2[0xFC]; }
            set { _rawBlock2[0xFC] = value; }
        }

        // A setting. Changed from 00 (in 075_0) to 40 (in 075_1)
        public byte Setting_0xFD
        {
            get { return _rawBlock2[0xFD]; }
            set { _rawBlock2[0xFD] = value; }
        }

        /// <summary>
        /// 01: On
        /// 02: Your Units
        /// 03 probably: Your Turn
        /// 04: Custom (check units' BattleAnimationsEnabled flags)
        /// 05 probably: Off
        /// </summary>
        public byte CombatAnimations
        {
            get { return _rawBlock2[0xFE]; }
            set { _rawBlock2[0xFE] = value; }
        }

        // One unknown byte (0xFF)

        // A setting. Changed from 00 (in 075_0) to 01 (in 075_1)
        // Guess is one of the following: Game Speed, Camera Pisitions, Skip Actions, Danger Area
        public byte Setting_0x100
        {
            get { return _rawBlock2[0x100]; }
            set { _rawBlock2[0x100] = value; }
        }

        // A setting. Changed from 00 (in 075_0) to 02 (in 075_1)
        // Guess is one of the following: Game Speed, Camera Pisitions, Skip Actions, Danger Area
        public byte Setting_0x101
        {
            get { return _rawBlock2[0x101]; }
            set { _rawBlock2[0x101] = value; }
        }

        // A setting. Changed from 01 (in 075_0) to 02 (in 075_1)
        // Guess is one of the following: Game Speed, Camera Pisitions, Skip Actions, Danger Area
        public byte GameSpeed
        {
            get { return _rawBlock2[0x102]; }
            set { _rawBlock2[0x102] = value; }
        }

        // A setting. Changed from 00 (in 075_0) to 01 (in 075_1)
        // Guess is one of the following: Game Speed, Camera Pisitions, Skip Actions, Danger Area
        public byte Setting_0x103
        {
            get { return _rawBlock2[0x103]; }
            set { _rawBlock2[0x103] = value; }
        }

        // A setting. Changed from 00 (in 075_0) to 01 (in 075_1)
        // Guess is one of the following: Game Speed, Camera Pisitions, Skip Actions, Danger Area
        public byte Setting_0x104
        {
            get { return _rawBlock2[0x104]; }
            set { _rawBlock2[0x104] = value; }
        }

        // One unknown byte (0x105)
        // Always 80?

        /// <summary>
        /// Values: 00 through FF, not sure of increments (maybe 0x08)
        /// </summary>
        public byte MusicVolume
        {
            get { return _rawBlock2[0x106]; }
            set { _rawBlock2[0x106] = value; }
        }

        /// <summary>
        /// Values: 00 through FF, not sure of increments (maybe 0x08)
        /// </summary>
        public byte SfxVolume
        {
            get { return _rawBlock2[0x107]; }
            set { _rawBlock2[0x107] = value; }
        }

        /// <summary>
        /// Values: 00 through FF, not sure of increments (maybe 0x08)
        /// </summary>
        public byte SystemVolume
        {
            get { return _rawBlock2[0x108]; }
            set { _rawBlock2[0x108] = value; }
        }

        /// <summary>
        /// Values: 00 through FF, not sure of increments (maybe 0x08)
        /// </summary>
        public byte VoiceVolume
        {
            get { return _rawBlock2[0x109]; }
            set { _rawBlock2[0x109] = value; }
        }

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
