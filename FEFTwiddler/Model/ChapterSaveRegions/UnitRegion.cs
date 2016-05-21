using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FEFTwiddler.Extensions;

namespace FEFTwiddler.Model.ChapterSaveRegions
{
    public class UnitRegion
    {
        public UnitRegion(byte[] raw)
        {
            Raw = raw;
        }

        #region Raw Data

        public byte[] Raw
        {
            get
            {
                return RawBlock1
                    .Concat(RawUnits)
                    .ToArray();
            }
            set
            {
                using (var ms = new MemoryStream(value))
                using (var br = new BinaryReader(ms))
                {
                    RawBlock1 = br.ReadBytes(RawBlock1Length);

                    Units = new List<Model.Unit>();

                    bool stillReading = true;
                    while (stillReading)
                    {
                        // What's next?
                        var nextBlock = br.ReadByte();

                        switch (nextBlock)
                        {
                            case 0x00: // Deployed living units (battle prep only)
                            case 0x03: // Undeployed living units (all living units in my castle)
                            case 0x04: // Dead units (killed by gameplay)
                            case 0x05: // If after chapter 6, units you had at some point before the split who haven't rejoined yet (or at least I think that's what this is)
                            case 0x06: // Dead units (killed by plot)
                                var unitCount = br.ReadByte();
                                for (var i = 0; i < unitCount; i++)
                                {
                                    ReadCurrentUnit(br, (Enums.UnitBlock)nextBlock);
                                }
                                break;
                            case 0xFF: // End of unit block
                            default: // Just a failsafe
                                stillReading = false;
                                break;
                        }
                    }
                }
            }
        }

        public const int RawBlock1Length = 0x05;
        private byte[] _rawBlock1;
        public byte[] RawBlock1
        {
            get { return _rawBlock1; }
            set
            {
                if (value.Length != RawBlock1Length) throw new ArgumentException("UnitRegion block 1 must be " + RawBlock1Length + " bytes in length");
                _rawBlock1 = value;
            }
        }

        public byte[] RawUnits
        {
            get
            {
                IEnumerable<byte> rawUnits = new List<byte>();

                var deployedUnits = Units.Where((x) => x.UnitBlock == Enums.UnitBlock.Deployed).ToList();
                if (deployedUnits.Count > 0)
                {
                    rawUnits = rawUnits.Concat(((byte)0x00).Yield()).Concat(((byte)deployedUnits.Count).Yield());
                    foreach (var unit in deployedUnits)
                    {
                        rawUnits = rawUnits.Concat(unit.Raw);
                    }
                }

                var livingUnits = Units.Where((x) => x.UnitBlock == Enums.UnitBlock.Living).ToList();
                if (livingUnits.Count > 0)
                {
                    rawUnits = rawUnits.Concat(((byte)0x03).Yield()).Concat(((byte)livingUnits.Count).Yield());
                    foreach (var unit in livingUnits)
                    {
                        rawUnits = rawUnits.Concat(unit.Raw);
                    }
                }

                var deadByGameplayUnits = Units.Where((x) => x.UnitBlock == Enums.UnitBlock.DeadByGameplay).ToList();
                if (deadByGameplayUnits.Count > 0)
                {
                    rawUnits = rawUnits.Concat(((byte)0x04).Yield()).Concat(((byte)deadByGameplayUnits.Count).Yield());
                    foreach (var unit in deadByGameplayUnits)
                    {
                        rawUnits = rawUnits.Concat(unit.Raw);
                    }
                }

                var absentUnits = Units.Where((x) => x.UnitBlock == Enums.UnitBlock.Absent).ToList();
                if (absentUnits.Count > 0)
                {
                    rawUnits = rawUnits.Concat(((byte)0x05).Yield()).Concat(((byte)absentUnits.Count).Yield());
                    foreach (var unit in absentUnits)
                    {
                        rawUnits = rawUnits.Concat(unit.Raw);
                    }
                }

                var deadByPlotUnits = Units.Where((x) => x.UnitBlock == Enums.UnitBlock.DeadByPlot).ToList();
                if (deadByPlotUnits.Count > 0)
                {
                    rawUnits = rawUnits.Concat(((byte)0x06).Yield()).Concat(((byte)deadByPlotUnits.Count).Yield());
                    foreach (var unit in deadByPlotUnits)
                    {
                        rawUnits = rawUnits.Concat(unit.Raw);
                    }
                }

                // End unit block
                rawUnits = rawUnits.Concat(((byte)0xFF).Yield());

                return rawUnits.ToArray();
            }
        }

        #endregion

        #region Block 1 Properties

        // TOPS (four bytes) (0x00 through 0x03)

        // One unknown byte (0x04)
        // Always 01, I think

        #endregion

        #region Units Properties

        public const int SoftMaxUnits = 100;
        public const int HardMaxUnits = 255;
        public List<Unit> Units { get; set; }

        #endregion

        private void ReadCurrentUnit(BinaryReader br, Enums.UnitBlock unitBlock)
        {
            var unit = new Unit();

            unit.UnitBlock = unitBlock;

            unit.ReadFromReader(br);

            Units.Add(unit);
        }
    }
}
