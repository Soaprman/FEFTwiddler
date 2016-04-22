using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEFTwiddler.Model.ChapterSaveRegions
{
    public class MyCastleRegion
    {
        public MyCastleRegion(byte[] raw)
        {
            _raw = raw;
        }

        private byte[] _raw;
        public byte[] Raw
        {
            get { return _raw; }
        }

        // 43AC (four bytes) (0x00 through 0x03)

        // 108 unknown bytes (0x04 through 0x6F)

        public string CastleName
        {
            get { return Utils.TypeConverter.ToString(_raw.Skip(0x6F).Take(0x20).ToArray()); }
            set { Array.Copy(Utils.TypeConverter.ToByteArray(value, 0x10), 0x00, _raw, 0x6F, 0x20); }
        }

        // Twenty-five unknown bytes (0x90 through 0xA8)

        public ushort DragonVeinPoint
        {
            get { return BitConverter.ToUInt16(_raw, 0xA9); }
            set
            {
                _raw[0xA9] = (byte)(value);
                _raw[0xAA] = (byte)(value >> 8);
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

        public byte MaterialQuantity_Crystal
        {
            get { return _raw[0x22D]; }
            set { _raw[0x22D] = value; }
        }

        public byte MaterialQuantity_Ruby
        {
            get { return _raw[0x22E]; }
            set { _raw[0x22E] = value; }
        }

        public byte MaterialQuantity_Sapphire
        {
            get { return _raw[0x22F]; }
            set { _raw[0x22F] = value; }
        }

        public byte MaterialQuantity_Onyx
        {
            get { return _raw[0x230]; }
            set { _raw[0x230] = value; }
        }

        public byte MaterialQuantity_Emerald
        {
            get { return _raw[0x231]; }
            set { _raw[0x231] = value; }
        }

        public byte MaterialQuantity_Topaz
        {
            get { return _raw[0x232]; }
            set { _raw[0x232] = value; }
        }

        public byte MaterialQuantity_Pearl
        {
            get { return _raw[0x233]; }
            set { _raw[0x233] = value; }
        }

        public byte MaterialQuantity_Coral
        {
            get { return _raw[0x234]; }
            set { _raw[0x234] = value; }
        }

        public byte MaterialQuantity_Lapis
        {
            get { return _raw[0x235]; }
            set { _raw[0x235] = value; }
        }

        public byte MaterialQuantity_Quartz
        {
            get { return _raw[0x236]; }
            set { _raw[0x236] = value; }
        }

        public byte MaterialQuantity_Jade
        {
            get { return _raw[0x237]; }
            set { _raw[0x237] = value; }
        }

        public byte MaterialQuantity_Amber
        {
            get { return _raw[0x238]; }
            set { _raw[0x238] = value; }
        }

        public byte MaterialQuantity_Meat
        {
            get { return _raw[0x239]; }
            set { _raw[0x239] = value; }
        }

        public byte MaterialQuantity_Milk
        {
            get { return _raw[0x23A]; }
            set { _raw[0x23A] = value; }
        }

        public byte MaterialQuantity_Cabbage
        {
            get { return _raw[0x23B]; }
            set { _raw[0x23B] = value; }
        }

        public byte MaterialQuantity_Berries
        {
            get { return _raw[0x23C]; }
            set { _raw[0x23C] = value; }
        }

        public byte MaterialQuantity_Wheat
        {
            get { return _raw[0x23D]; }
            set { _raw[0x23D] = value; }
        }

        public byte MaterialQuantity_Beans
        {
            get { return _raw[0x23E]; }
            set { _raw[0x23E] = value; }
        }

        public byte MaterialQuantity_Fish
        {
            get { return _raw[0x23F]; }
            set { _raw[0x23F] = value; }
        }

        public byte MaterialQuantity_Daikon
        {
            get { return _raw[0x240]; }
            set { _raw[0x240] = value; }
        }

        public byte MaterialQuantity_Peaches
        {
            get { return _raw[0x241]; }
            set { _raw[0x241] = value; }
        }

        public byte MaterialQuantity_Rice
        {
            get { return _raw[0x242]; }
            set { _raw[0x242] = value; }
        }
    }
}
