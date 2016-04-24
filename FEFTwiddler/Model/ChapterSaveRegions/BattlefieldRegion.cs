using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FEFTwiddler.Extensions;

namespace FEFTwiddler.Model.ChapterSaveRegions
{
    public class BattlefieldRegion
    {
        public BattlefieldRegion(byte[] raw)
        {
            Raw = raw;
        }

        #region Raw Data

        public byte[] Raw
        {
            get
            {
                return RawBlock1
                    .Concat(RawBattlefieldCount.Yield())
                    .Concat(RawBattlefields)
                    .ToArray();
            }
            set
            {
                using (var ms = new MemoryStream(value))
                using (var br = new BinaryReader(ms))
                {
                    RawBlock1 = br.ReadBytes(RawBlock1Length);

                    Battlefields = new List<Battlefield>();
                    var battlefieldCount = br.ReadByte();
                    for (var i = 0; i < battlefieldCount; i++)
                    {
                        Battlefields.Add(new Battlefield(br.ReadBytes(0x21)));
                    }
                }
            }
        }

        public const int RawBlock1Length = 0x20;
        private byte[] _rawBlock1;
        public byte[] RawBlock1
        {
            get { return _rawBlock1; }
            set
            {
                if (value.Length != RawBlock1Length) throw new ArgumentException("BattlefieldRegion block 1 must be " + RawBlock1Length + " bytes in length");
                _rawBlock1 = value;
            }
        }

        public byte RawBattlefieldCount
        {
            get { return (byte)Battlefields.Count; }
        }

        public byte[] RawBattlefields
        {
            get
            {
                IEnumerable<byte> rawBattlefields = new List<byte>();
                foreach (var entry in Battlefields)
                {
                    rawBattlefields = rawBattlefields.Concat(entry.Raw);
                }
                return rawBattlefields.ToArray();
            }
        }

        #endregion

        #region Block 1 Properties

        // TOPS (four bytes) (0x00 through 0x03)

        // Twenty-seven unknown bytes (0x04 through 0x1E)
        // The last thirteen bytes had this sequence on both my ch27 RV save and my "new" save, so it probably isn't something that changes much:
        // 77 00 00 00 00 DF F2 64 6E B4 1E 05 00

        public Enums.Chapter CurrentChapter
        {
            get { return (Enums.Chapter)_rawBlock1[0x1F]; }
            set { _rawBlock1[0x1F] = (byte)value; }
        }

        #endregion

        #region Battlefields Properties

        // One byte - ChapterHistoryCount (0x00)

        public List<Battlefield> Battlefields { get; set; }

        #endregion
    }
}
