using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FEFTwiddler.Extensions;

namespace FEFTwiddler.Model.ChapterSaveRegions
{
    public class MyCastleRegion
    {
        public MyCastleRegion(byte[] raw)
        {
            Raw = raw;
        }

        public byte[] Raw
        {
            get
            {
                return RawBlock1
                    .Concat(RawBlock2)
                    .Concat(RawUnknownACount.Yield())
                    .Concat(RawUnknownAList)
                    .Concat(RawBlock3)
                    .Concat(RawUnknownBCount.Yield())
                    .Concat(RawUnknownBList)
                    .Concat(RawBlock4)
                    .Concat(RawBuildingCount.Yield())
                    .Concat(RawBuildings)
                    .Concat(RawBlock5)
                    .ToArray();
            }
            set
            {
                using (var ms = new MemoryStream(value))
                using (var br = new BinaryReader(ms))
                {
                    // Block 1
                    RawBlock1 = br.ReadBytes(RawBlock1Length);

                    // Here we go... the hell block (block 2)
                    var rawBlock2 = new List<byte>();
                    rawBlock2.AddRange(br.ReadBytes(4));
                    var stuckInHell = true;
                    while (stuckInHell)
                    {
                        var nextByte = br.ReadByte();
                        rawBlock2.Add(nextByte);
                        if (nextByte == 0x02) stuckInHell = false;
                    }
                    rawBlock2.AddRange(br.ReadBytes(5));
                    RawBlock2 = rawBlock2.ToArray();

                    // UnknownA List
                    _unknownAList = new List<UnknownA>();

                    byte unknownACount = br.ReadByte();
                    for (var i = 0; i < unknownACount; i++)
                    {
                        _unknownAList.Add(new UnknownA(br.ReadBytes(0x04)));
                    }

                    // Block 3
                    RawBlock3 = br.ReadBytes(RawBlock3Length);

                    // UnknownB List
                    _unknownBList = new List<UnknownB>();

                    byte unknownBCount = br.ReadByte();
                    for (var i = 0; i < unknownBCount; i++)
                    {
                        _unknownBList.Add(new UnknownB(br.ReadBytes(0x1E)));
                    }

                    // Block 4
                    RawBlock4 = br.ReadBytes(RawBlock4Length);

                    // Buildings
                    _buildings = new List<Building>();

                    byte buildingCount = br.ReadByte();
                    for (var i = 0; i < buildingCount; i++)
                    {
                        var buildingBytes = new List<byte>();

                        // Lilith's Temple has four 00 bytes in this spot for some reason. Just skip them and add only the building ID bytes
                        var buildingId = br.ReadUInt16();
                        if (buildingId == (ushort)Enums.Building.LilithsTemple_1 ||
                            buildingId == (ushort)Enums.Building.LilithsTemple_2 ||
                            buildingId == (ushort)Enums.Building.LilithsTemple_3)
                        {
                            br.ReadBytes(0x04);
                        }
                        buildingBytes.Add((byte)buildingId);
                        buildingBytes.Add((byte)(buildingId >> 8));

                        // The rest are constant
                        buildingBytes.AddRange(br.ReadBytes(0x0B));

                        _buildings.Add(new Building(buildingBytes.ToArray()));
                    }

                    // Block 5
                    RawBlock5 = br.ReadBytes((int)br.BaseStream.Length - (int)br.BaseStream.Position); // Read to end
                }
            }
        }

        #region Block 1

        public const int RawBlock1Length = 0x247;
        private byte[] _rawBlock1;
        public byte[] RawBlock1
        {
            get { return _rawBlock1; }
            set
            {
                if (value.Length != RawBlock1Length) throw new ArgumentException("MyCastleRegion block 1 must be " + RawBlock1Length + " bytes in length");
                _rawBlock1 = value;
            }
        }

        // 43AC (four bytes) (0x00 through 0x03)

        // 108 unknown bytes (0x04 through 0x6F)

        public string CastleName
        {
            get { return Utils.TypeConverter.ToString(_rawBlock1.Skip(0x6F).Take(0x20).ToArray()); }
            set { Array.Copy(Utils.TypeConverter.ToByteArray(value, 0x10), 0x00, _rawBlock1, 0x6F, 0x20); }
        }

        // Eighteen unknown bytes (0x90 through 0xA1)

        public Enums.CastleMap CastleMap
        {
            get { return (Enums.CastleMap)_rawBlock1[0xA2]; }
            set { _rawBlock1[0xA2] = (byte)value; }
        }

        // Six unknown bytes (0xA3 through 0xA8)

        public ushort DragonVeinPoint
        {
            get { return BitConverter.ToUInt16(_rawBlock1, 0xA9); }
            set
            {
                _rawBlock1[0xA9] = (byte)(value);
                _rawBlock1[0xAA] = (byte)(value >> 8);
            }
        }

        // Two unknown bytes (0xAB through 0xAC)

        // Sixty-four semi-known bytes (0xAD through 0xEC)
        // Unlocked buildings block 1? (begins with XX XX XX F1 FF)
        // Skip 0x14 bytes
        // UnlockedStatues1 = 0x1B bytes
        // Skip 0x11 bytes

        // Sixty-four unknown bytes (0xED through 0x12C)
        // All 00s?

        // Sixty-four semi-known bytes (0x12D through 0x16C)
        // Unlocked buildings block 2? (begins with XX XX XX F1 FF)
        // Skip 0x14 bytes
        // UnlockedStatues2 = 0x1B bytes
        // Skip 0x11 bytes

        // Sixty-four unknown bytes (0x16D through 0x1AC)
        // All 00s?

        // Sixty-four semi-known bytes (0x1AD through 0x1EC)
        // Unlocked buildings block 3? (begins with XX XX XX F1 FF)
        // Skip 0x14 bytes
        // UnlockedStatues3 = 0x1B bytes
        // Skip 0x11 bytes

        // Sixty-four unknown bytes (0x1ED through 0x22C)
        // All 00s?

        #endregion

        #region Material Quantity (still part of Block 1)

        // A full material quantity block looks like:
        // 63 63 63 63 63 63 63 63 63 63 63 63 63 63 63 63 63 63 63 63 63 63

        public byte MaterialQuantity_Crystal
        {
            get { return _rawBlock1[0x22D]; }
            set { _rawBlock1[0x22D] = value; }
        }

        public byte MaterialQuantity_Ruby
        {
            get { return _rawBlock1[0x22E]; }
            set { _rawBlock1[0x22E] = value; }
        }

        public byte MaterialQuantity_Sapphire
        {
            get { return _rawBlock1[0x22F]; }
            set { _rawBlock1[0x22F] = value; }
        }

        public byte MaterialQuantity_Onyx
        {
            get { return _rawBlock1[0x230]; }
            set { _rawBlock1[0x230] = value; }
        }

        public byte MaterialQuantity_Emerald
        {
            get { return _rawBlock1[0x231]; }
            set { _rawBlock1[0x231] = value; }
        }

        public byte MaterialQuantity_Topaz
        {
            get { return _rawBlock1[0x232]; }
            set { _rawBlock1[0x232] = value; }
        }

        public byte MaterialQuantity_Pearl
        {
            get { return _rawBlock1[0x233]; }
            set { _rawBlock1[0x233] = value; }
        }

        public byte MaterialQuantity_Coral
        {
            get { return _rawBlock1[0x234]; }
            set { _rawBlock1[0x234] = value; }
        }

        public byte MaterialQuantity_Lapis
        {
            get { return _rawBlock1[0x235]; }
            set { _rawBlock1[0x235] = value; }
        }

        public byte MaterialQuantity_Quartz
        {
            get { return _rawBlock1[0x236]; }
            set { _rawBlock1[0x236] = value; }
        }

        public byte MaterialQuantity_Jade
        {
            get { return _rawBlock1[0x237]; }
            set { _rawBlock1[0x237] = value; }
        }

        public byte MaterialQuantity_Amber
        {
            get { return _rawBlock1[0x238]; }
            set { _rawBlock1[0x238] = value; }
        }

        public byte MaterialQuantity_Meat
        {
            get { return _rawBlock1[0x239]; }
            set { _rawBlock1[0x239] = value; }
        }

        public byte MaterialQuantity_Milk
        {
            get { return _rawBlock1[0x23A]; }
            set { _rawBlock1[0x23A] = value; }
        }

        public byte MaterialQuantity_Cabbage
        {
            get { return _rawBlock1[0x23B]; }
            set { _rawBlock1[0x23B] = value; }
        }

        public byte MaterialQuantity_Berries
        {
            get { return _rawBlock1[0x23C]; }
            set { _rawBlock1[0x23C] = value; }
        }

        public byte MaterialQuantity_Wheat
        {
            get { return _rawBlock1[0x23D]; }
            set { _rawBlock1[0x23D] = value; }
        }

        public byte MaterialQuantity_Beans
        {
            get { return _rawBlock1[0x23E]; }
            set { _rawBlock1[0x23E] = value; }
        }

        public byte MaterialQuantity_Fish
        {
            get { return _rawBlock1[0x23F]; }
            set { _rawBlock1[0x23F] = value; }
        }

        public byte MaterialQuantity_Daikon
        {
            get { return _rawBlock1[0x240]; }
            set { _rawBlock1[0x240] = value; }
        }

        public byte MaterialQuantity_Peaches
        {
            get { return _rawBlock1[0x241]; }
            set { _rawBlock1[0x241] = value; }
        }

        public byte MaterialQuantity_Rice
        {
            get { return _rawBlock1[0x242]; }
            set { _rawBlock1[0x242] = value; }
        }

        #endregion

        #region Material Discovered (still part of Block 1)

        // A completed material discovered block looks like:
        // FF 0F FF 03

        public bool MaterialDiscovered_Crystal
        {
            get { return _rawBlock1[0x243].GetFlag(0x01); }
            set { _rawBlock1[0x243] = _rawBlock1[0x243].SetFlag(0x01, value); }
        }
        public bool MaterialDiscovered_Ruby
        {
            get { return _rawBlock1[0x243].GetFlag(0x02); }
            set { _rawBlock1[0x243] = _rawBlock1[0x243].SetFlag(0x02, value); }
        }
        public bool MaterialDiscovered_Sapphire
        {
            get { return _rawBlock1[0x243].GetFlag(0x04); }
            set { _rawBlock1[0x243] = _rawBlock1[0x243].SetFlag(0x04, value); }
        }
        public bool MaterialDiscovered_Onyx
        {
            get { return _rawBlock1[0x243].GetFlag(0x08); }
            set { _rawBlock1[0x243] = _rawBlock1[0x243].SetFlag(0x08, value); }
        }
        public bool MaterialDiscovered_Emerald
        {
            get { return _rawBlock1[0x243].GetFlag(0x10); }
            set { _rawBlock1[0x243] = _rawBlock1[0x243].SetFlag(0x10, value); }
        }
        public bool MaterialDiscovered_Topaz
        {
            get { return _rawBlock1[0x243].GetFlag(0x20); }
            set { _rawBlock1[0x243] = _rawBlock1[0x243].SetFlag(0x20, value); }
        }
        public bool MaterialDiscovered_Pearl
        {
            get { return _rawBlock1[0x243].GetFlag(0x40); }
            set { _rawBlock1[0x243] = _rawBlock1[0x243].SetFlag(0x40, value); }
        }
        public bool MaterialDiscovered_Coral
        {
            get { return _rawBlock1[0x243].GetFlag(0x80); }
            set { _rawBlock1[0x243] = _rawBlock1[0x243].SetFlag(0x80, value); }
        }

        public bool MaterialDiscovered_Lapis
        {
            get { return _rawBlock1[0x244].GetFlag(0x01); }
            set { _rawBlock1[0x244] = _rawBlock1[0x244].SetFlag(0x01, value); }
        }
        public bool MaterialDiscovered_Quartz
        {
            get { return _rawBlock1[0x244].GetFlag(0x02); }
            set { _rawBlock1[0x244] = _rawBlock1[0x244].SetFlag(0x02, value); }
        }
        public bool MaterialDiscovered_Jade
        {
            get { return _rawBlock1[0x244].GetFlag(0x04); }
            set { _rawBlock1[0x244] = _rawBlock1[0x244].SetFlag(0x04, value); }
        }
        public bool MaterialDiscovered_Amber
        {
            get { return _rawBlock1[0x244].GetFlag(0x08); }
            set { _rawBlock1[0x244] = _rawBlock1[0x244].SetFlag(0x08, value); }
        }

        public bool MaterialDiscovered_Meat
        {
            get { return _rawBlock1[0x245].GetFlag(0x01); }
            set { _rawBlock1[0x245] = _rawBlock1[0x245].SetFlag(0x01, value); }
        }
        public bool MaterialDiscovered_Milk
        {
            get { return _rawBlock1[0x245].GetFlag(0x02); }
            set { _rawBlock1[0x245] = _rawBlock1[0x245].SetFlag(0x02, value); }
        }
        public bool MaterialDiscovered_Cabbage
        {
            get { return _rawBlock1[0x245].GetFlag(0x04); }
            set { _rawBlock1[0x245] = _rawBlock1[0x245].SetFlag(0x04, value); }
        }
        public bool MaterialDiscovered_Berries
        {
            get { return _rawBlock1[0x245].GetFlag(0x08); }
            set { _rawBlock1[0x245] = _rawBlock1[0x245].SetFlag(0x08, value); }
        }
        public bool MaterialDiscovered_Wheat
        {
            get { return _rawBlock1[0x245].GetFlag(0x10); }
            set { _rawBlock1[0x245] = _rawBlock1[0x245].SetFlag(0x10, value); }
        }
        public bool MaterialDiscovered_Beans
        {
            get { return _rawBlock1[0x245].GetFlag(0x20); }
            set { _rawBlock1[0x245] = _rawBlock1[0x245].SetFlag(0x20, value); }
        }
        public bool MaterialDiscovered_Fish
        {
            get { return _rawBlock1[0x245].GetFlag(0x40); }
            set { _rawBlock1[0x245] = _rawBlock1[0x245].SetFlag(0x40, value); }
        }
        public bool MaterialDiscovered_Daikon
        {
            get { return _rawBlock1[0x245].GetFlag(0x80); }
            set { _rawBlock1[0x245] = _rawBlock1[0x245].SetFlag(0x80, value); }
        }

        public bool MaterialDiscovered_Peaches
        {
            get { return _rawBlock1[0x246].GetFlag(0x01); }
            set { _rawBlock1[0x246] = _rawBlock1[0x246].SetFlag(0x01, value); }
        }
        public bool MaterialDiscovered_Rice
        {
            get { return _rawBlock1[0x246].GetFlag(0x02); }
            set { _rawBlock1[0x246] = _rawBlock1[0x246].SetFlag(0x02, value); }
        }

        #endregion

        #region Hell Block (Block 2)

        // Some stuff that I haven't got a clue about yet that varies in size
        // I am calling this the HELL PATTERN
        // Examples:
        // 00 01 02 00 01 00 E0 07 00 00 09 00 E0 07 00 00 01 00 01 00 E0 07 00 00 00 00 
        // 00 01 00 00 00 00 00 00 
        // 00 00 03 00 1C 00 E0 07 00 00 0A 00 E0 07 00 00 3E 00 E0 07 00 00 00 00 03 00 1C 00 E0 07 00 00 0A 00 E0 07 00 00 3E 00 E0 07 00 00 
        // 00 02 04 00 1C 00 E0 07 00 00 0A 00 E0 07 00 00 3E 00 E0 07 00 00 08 00 E0 07 00 00 00 00 04 00 1C 00 E0 07 00 00 0A 00 E0 07 00 00 3E 00 E0 07 00 00 08 00 E0 07 00 00
        // 00 00 00 00 00 00 00 00
        // 00 02 01 00 08 00 E0 07 00 00 00 00 01 00 08 00 E0 07 00 00
        // 00 01 01 00 1E 00 E0 07 00 00 00 00 00 00
        // 01 02 01 00 01 00 E0 07 00 00 01 00 01 00 E0 07 00 00 00 00
        // 01 02 01 00 01 00 E0 07 00 00 00 00 00 00
        // 00 00 05 00 08 00 E0 07 00 00 1E 00 E0 07 00 00 32 00 E0 07 00 00 31 00 E0 07 00 00 20 00 E0 07 00 00 00 00 00 00
        // 00 02 00 00 00 00 00 00
        // 00 02 01 00 08 00 E0 07 00 00 00 00 00 00
        // 00 00 01 00 1C 00 E0 07 00 00 00 00 01 00 1C 00 E0 07 00 00
        // 00 01 03 00 1C 00 E0 07 00 00 0A 00 E0 07 00 00 3E 00 E0 07 00 00 00 00 03 00 1C 00 E0 07 00 00 0A 00 E0 07 00 00 3E 00 E0 07 00 00
        // 00 00 03 00 1C 00 E0 07 00 00 0A 00 E0 07 00 00 3E 00 E0 07 00 00 00 00 03 00 1C 00 E0 07 00 00 0A 00 E0 07 00 00 3E 00 E0 07 00 00
        // 00 01 02 00 01 00 E0 07 00 00 09 00 E0 07 00 00 01 00 01 00 E0 07 00 00 00 00
        // 00 00 05 00 21 00 E0 07 00 00 04 00 E0 07 00 00 24 00 E0 07 00 00 09 00 E0 07 00 00 3E 00 E0 07 00 00 00 00 05 00 21 00 E0 07 00 00 04 00 E0 07 00 00 24 00 E0 07 00 00 09 00 E0 07 00 00 3E 00 E0 07 00 00
        /*
         * For now, probably best to do this:
         * Skip the first four bytes
         * Read bytes until reaching 0x02 (beginning of next block)
         * 
         * This fails if any 0x02 values are in the middle
         * 
         * */

        // Six bytes, the first is (always?) 0x02

        private byte[] _rawBlock2;
        public byte[] RawBlock2
        {
            get { return _rawBlock2; }
            set
            {
                _rawBlock2 = value;
            }
        }

        #endregion

        #region UnknownA collection

        // A byte saying how many four-byte things are coming up

        // Four-byte things
        // 00 XX YY ZZ
        // XX: Usually 00, but I've seen 04
        // YY: Usually 00, but I've seen 01 and 02
        // ZZ: Some value, seems unique within the list

        public byte RawUnknownACount
        {
            get { return (byte)(_unknownAList.Count); }
        }

        public byte[] RawUnknownAList
        {
            get
            {
                IEnumerable<byte> rawUnknownAList = new List<byte>();
                foreach (var unknownA in _unknownAList)
                {
                    rawUnknownAList = rawUnknownAList.Concat(unknownA.Raw);
                }
                return rawUnknownAList.ToArray();
            }
        }

        private List<UnknownA> _unknownAList;
        public List<UnknownA> UnknownAList
        {
            get { return _unknownAList; }
        }

        #endregion

        #region Block 3

        public const int RawBlock3Length = 0x39F;
        private byte[] _rawBlock3;
        public byte[] RawBlock3
        {
            get { return _rawBlock3; }
            set
            {
                if (value.Length != RawBlock3Length) throw new ArgumentException("MyCastleRegion block 3 must be " + RawBlock3Length + " bytes in length");
                _rawBlock3 = value;
            }
        }

        // 0x10B bytes
        // 00 00 00 01, then some 01s, 02s, and 04s mostly, followed by a fat chunk of 00s

        // Strings like Corrin name, castle greeting, etc
        // From the beginning of corrin name to before the beginning of the four strings, there are 0x150 (336) bytes. This seems consistent
        // 0x42 bytes per string, though that may include a 00 00 terminator or separator. There are four of these:
        // Calling Card Message, Plaza Message, Throne Message, Battle Cry

        // 0x3B (59) bytes of stuff before the chaos block's starting 0x02 bytes
        // Probably not terribly interesting values. Examples:
        // My 026/Chapter0_dec save
        // 00 00 00 00 17 11 14 20 00 08 00 00 00 FF 00 50 00 00 00 7F 00 00 00 00 00 00 00 00 00 00 50 00 00 00 FF FF FF 0F 00 00 00 00 00 00 00 50 00 00 00 00 00 00 00 00 00 00 00 00 00
        // "CART" save
        // 00 00 00 00 17 11 14 20 00 08 00 00 00 00 00 50 00 00 00 00 00 00 00 00 00 00 00 00 00 00 50 00 00 00 00 00 00 00 00 00 00 00 00 00 00 50 00 00 00 00 00 00 00 00 00 00 00 00 00

        // Finally, a single byte that is always 0x02

        #endregion

        #region UnknownB collection

        // CHAOS BLOCK
        // 0x02 bytes: a ushort containing the number of entries in the chaos block
        // Each entry is a 0x1E (30) byte string similar to this:
        // 19 08 14 20 00 1D 00 FF FF FF CD CD CD CD 02 CD CD CD CD CD CD CD CD 02 FD CC 7A 03 00 0A 
        // In that line, the OO bytes seem constant and the XX bytes seem to change:
        // OO OO OO OO OO XX OO OO OO OO OO OO OO OO OO OO OO OO OO OO OO OO OO XX XX XX XX XX OO XX
        // Column headers:
        // AA BB CC DD EE FF GG HH II JJ KK LL MM NN OO PP QQ RR SS TT UU VV WW XX YY ZZ 11 22 33 44
        // FF-GG: may be a ushort value. Values range from 0x03 to 0x46, with most higher values toward the bottom of the list.
        // XX: is a value between 01-05, or FF.
        // YY ZZ 11 22: are either 00 or seemingly random values. Seems similar to buildings...
        // 33: has a value of 0x01 in four cases. FF values for those cases are: 22, 24, 11, 14
        // ------- In another save, when FF's value is 11 or 14, 33's value is 00
        // 44: mostly lowish values, sometimes FF

        public byte RawUnknownBCount
        {
            get { return (byte)(_unknownBList.Count); }
        }

        public byte[] RawUnknownBList
        {
            get
            {
                IEnumerable<byte> rawUnknownBList = new List<byte>();
                foreach (var unknownB in _unknownBList)
                {
                    rawUnknownBList = rawUnknownBList.Concat(unknownB.Raw);
                }
                return rawUnknownBList.ToArray();
            }
        }

        private List<UnknownB> _unknownBList;
        public List<UnknownB> UnknownBList
        {
            get { return _unknownBList; }
        }

        #endregion

        #region Block 4

        public const int RawBlock4Length = 0x75;
        private byte[] _rawBlock4;
        public byte[] RawBlock4
        {
            get { return _rawBlock4; }
            set
            {
                if (value.Length != RawBlock4Length) throw new ArgumentException("MyCastleRegion block 4 must be " + RawBlock4Length + " bytes in length");
                _rawBlock4 = value;
            }
        }

        // 0x74 unknown bytes between chaos block entries and building entries

        // The last byte is always 0x01

        #endregion

        #region Buildings

        public byte RawBuildingCount
        {
            get { return (byte)(_buildings.Count); }
        }

        public byte[] RawBuildings
        {
            get
            {
                IEnumerable<byte> rawBuildings = new List<byte>();
                foreach (var building in _buildings)
                {
                    rawBuildings = rawBuildings.Concat(building.Raw);
                }
                return rawBuildings.ToArray();
            }
        }

        private List<Building> _buildings;
        public List<Building> Buildings
        {
            get { return _buildings; }
        }

        #endregion

        #region Block 5

        private byte[] _rawBlock5;
        public byte[] RawBlock5
        {
            get { return _rawBlock5; }
            set
            {
                _rawBlock5 = value;
            }
        }

        // The last block. Covers the entire rest of the save file

        #endregion
    }
}
