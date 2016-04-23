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

                    Units = new List<Model.Character>();

                    bool stillReading = true;
                    while (stillReading)
                    {
                        // What's next?
                        var nextBlock = br.ReadByte();

                        switch (nextBlock)
                        {
                            case 0x00: // Deployed living units (battle prep only)
                            case 0x03: // Undeployed living units (all living units in my castle)
                            case 0x05: // If after chapter 6, units you had at some point before the split who haven't rejoined yet (or at least I think that's what this is)
                            case 0x06: // Dead units
                                var characterCount = br.ReadByte();
                                for (var i = 0; i < characterCount; i++)
                                {
                                    ReadCurrentUnit(br, nextBlock == 0x05);
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

                var deployedUnits = Units.Where((x) => !x.IsAbsent && x.IsDeployed).ToList();
                if (deployedUnits.Count > 0)
                {
                    rawUnits = rawUnits.Concat(((byte)0x00).Yield()).Concat(((byte)deployedUnits.Count).Yield());
                    foreach (var unit in deployedUnits)
                    {
                        rawUnits = rawUnits.Concat(unit.Raw);
                    }
                }

                var livingUnits = Units.Where((x) => !x.IsAbsent && !x.IsDeployed && !x.IsDead).ToList();
                if (livingUnits.Count > 0)
                {
                    rawUnits = rawUnits.Concat(((byte)0x03).Yield()).Concat(((byte)livingUnits.Count).Yield());
                    foreach (var unit in livingUnits)
                    {
                        rawUnits = rawUnits.Concat(unit.Raw);
                    }
                }

                var absentUnits = Units.Where((x) => x.IsAbsent).ToList();
                if (absentUnits.Count > 0)
                {
                    rawUnits = rawUnits.Concat(((byte)0x05).Yield()).Concat(((byte)absentUnits.Count).Yield());
                    foreach (var unit in absentUnits)
                    {
                        rawUnits = rawUnits.Concat(unit.Raw);
                    }
                }

                var deadUnits = Units.Where((x) => x.IsDead).ToList();
                if (deadUnits.Count > 0)
                {
                    rawUnits = rawUnits.Concat(((byte)0x06).Yield()).Concat(((byte)deadUnits.Count).Yield());
                    foreach (var unit in deadUnits)
                    {
                        rawUnits = rawUnits.Concat(unit.Raw);
                    }
                }

                // End character block
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

        public List<Character> Units { get; set; }

        #endregion

        private void ReadCurrentUnit(BinaryReader br, bool isAbsent)
        {
            byte[] chunk;

            var unit = new Character();

            unit.IsAbsent = isAbsent;

            chunk = new byte[Model.Character.RawBlock1Length];
            br.Read(chunk, 0, Model.Character.RawBlock1Length);
            unit.RawBlock1 = chunk;

            chunk = new byte[Model.Character.RawInventoryLength];
            br.Read(chunk, 0, Model.Character.RawInventoryLength);
            unit.RawInventory = chunk;

            chunk = new byte[0x01];
            br.Read(chunk, 0, 0x01);
            unit.RawNumberOfSupports = chunk.First();

            chunk = new byte[unit.RawNumberOfSupports];
            br.Read(chunk, 0, unit.RawNumberOfSupports);
            unit.RawSupports = chunk;

            chunk = new byte[Model.Character.RawBlock2Length];
            br.Read(chunk, 0, Model.Character.RawBlock2Length);
            unit.RawBlock2 = chunk;

            chunk = new byte[Model.Character.RawLearnedSkillsLength];
            br.Read(chunk, 0, Model.Character.RawLearnedSkillsLength);
            unit.RawLearnedSkills = chunk;

            var depLength = (unit.IsDeployed ? Model.Character.RawDeployedUnitInfoLengthIfDeployed : Model.Character.RawDeployedUnitInfoLengthIfNotDeployed);
            chunk = new byte[depLength];
            br.Read(chunk, 0, depLength);
            unit.RawDeployedUnitInfo = chunk;

            chunk = new byte[Model.Character.RawBlock3Length];
            br.Read(chunk, 0, Model.Character.RawBlock3Length);
            unit.RawBlock3 = chunk;

            chunk = new byte[0x01];
            br.Read(chunk, 0, 0x01);
            unit.RawEndBlockType = chunk.First();

            chunk = new byte[unit.GetRawEndBlockSize()];
            br.Read(chunk, 0, unit.GetRawEndBlockSize());
            unit.RawEndBlock = chunk;

            Units.Add(unit);
        }
    }
}
