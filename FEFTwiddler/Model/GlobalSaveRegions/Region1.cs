using System;
using System.Drawing;
using System.IO;
using System.Linq;

namespace FEFTwiddler.Model.GlobalSaveRegions
{
    public class Region1
    {
        public Region1(byte[] raw)
        {
            Raw = raw;
        }

        public byte[] Raw
        {
            get
            {
                return RawBlock1
                    .Concat(RawSupportCount1)
                    .Concat(RawSupportCount2)
                    .Concat(RawSupports)
                    .Concat(RawBlock2)
                    .ToArray();
            }
            set
            {
                using (var ms = new MemoryStream(value))
                using (var br = new BinaryReader(ms))
                {
                    RawBlock1 = br.ReadBytes(RawBlock1Length);

                    SupportCount1 = br.ReadUInt32();
                    SupportCount2 = br.ReadUInt32();

                    // Read supports
                    var supportBytes = (int)((SupportCount1 / 8) + (SupportCount1 % 8 != 0 ? 1 : 0));
                    RawSupports = br.ReadBytes(supportBytes);

                    RawBlock2 = br.ReadBytes(RawBlock2Length);
                }
            }
        }

        public const int RawBlock1Length = 0x42;
        private byte[] _rawBlock1;
        public byte[] RawBlock1
        {
            get { return _rawBlock1; }
            set
            {
                if (value.Length != RawBlock1Length) throw new ArgumentException("Region1 block 1 must be " + RawBlock1Length + " bytes in length");
                _rawBlock1 = value;
            }
        }

        public byte[] RawSupportCount1
        {
            get
            {
                return new byte[]
                {
                    (byte)(SupportCount1),
                    (byte)(SupportCount1 >> 8),
                    (byte)(SupportCount1 >> 16),
                    (byte)(SupportCount1 >> 24)
                };
            }
        }

        public byte[] RawSupportCount2
        {
            get
            {
                return new byte[]
                {
                    (byte)(SupportCount2),
                    (byte)(SupportCount2 >> 8),
                    (byte)(SupportCount2 >> 16),
                    (byte)(SupportCount2 >> 24)
                };
            }
        }

        private byte[] _rawSupports;
        public byte[] RawSupports
        {
            get
            {
                return _rawSupports;
            }
            set
            {
                _rawSupports = value;
            }
        }

        public const int RawBlock2Length = 0xD7;
        private byte[] _rawBlock2;
        public byte[] RawBlock2
        {
            get { return _rawBlock2; }
            set
            {
                if (value.Length != RawBlock2Length) throw new ArgumentException("Region1 block 2 must be " + RawBlock2Length + " bytes in length");
                _rawBlock2 = value;
            }
        }

        #region Block 1

        // 1ULG (four bytes) (0x00 through 0x03)

        // Forty-one unknown bytes (0x04 through 0x2C)

        // Seems to be 01 in full support log saves
        public byte Unknown_Block1_0x2D
        {
            get { return _rawBlock1[0x2D]; }
            set { _rawBlock1[0x2D] = value; }
        }

        // Two unknown bytes (0x2E through 0x2F)
        // Always 03 00?

        public uint UnlockedCharacterCount1
        {
            get
            {
                return BitConverter.ToUInt32(_rawBlock1, 0x30);
            }
            set
            {
                _rawBlock1[0x30] = (byte)(value);
                _rawBlock1[0x31] = (byte)(value >> 8);
                _rawBlock1[0x32] = (byte)(value >> 16);
                _rawBlock1[0x33] = (byte)(value >> 24);
            }
        }

        public uint UnlockedCharacterCount2
        {
            get
            {
                return BitConverter.ToUInt32(_rawBlock1, 0x34);
            }
            set
            {
                _rawBlock1[0x34] = (byte)(value);
                _rawBlock1[0x35] = (byte)(value >> 8);
                _rawBlock1[0x36] = (byte)(value >> 16);
                _rawBlock1[0x37] = (byte)(value >> 24);
            }
        }

        public byte[] UnlockedCharacters
        {
            get { return _rawBlock1.Skip(0x38).Take(0x09).ToArray(); }
            set { Array.Copy(value, 0x00, _rawBlock1, 0x38, 0x09); }
        }

        // One unknown byte (0x41)
        // Always 00?

        #endregion

        #region Supports

        public uint SupportCount1
        {
            get; set;
        }

        public uint SupportCount2
        {
            get; set;
        }

        #endregion

        #region Block 2

        // Five unknown bytes (0x00 through 0x04)
        // Always 00 18 00 00 00?

        // 7F 7F 7F on the "completed" saves I have
        // Maybe movies
        public byte[] UnknownUnlocks1
        {
            get { return _rawBlock2.Skip(0x05).Take(0x03).ToArray(); }
            set { Array.Copy(value, 0x00, _rawBlock2, 0x05, 0x03); }
        }

        // Five unknown bytes (0x08 through 0x0C)
        // Always 00 80 00 00 00?

        // FC FF FF FF FF FF FF FF FF FF FF FF on the "completed" saves I have
        // Maybe music
        public byte[] UnknownUnlocks2
        {
            get { return _rawBlock2.Skip(0x0D).Take(0x0C).ToArray(); }
            set { Array.Copy(value, 0x00, _rawBlock2, 0x0D, 0x0C); }
        }

        // Two unknown bytes (0x19 through 0x1A)
        // Not sure. They seem to vary

        // Eleven unknown bytes (0x1B through 0x2F)
        // Always 00 00 00 16 00 00 00 16 00 00 00?

        // Hair color block
        // Eighty-eight (0x58) bytes (0x26 through 0x7D)
        // Why there gotta be so many of these damn kids?????

        public Color HairColor_KanaM
        {
            get { return Color.FromArgb(_rawBlock2[0x29], _rawBlock2[0x26], _rawBlock2[0x27], _rawBlock2[0x28]); }
            set
            {
                _rawBlock2[0x26] = value.R;
                _rawBlock2[0x27] = value.G;
                _rawBlock2[0x28] = value.B;
                _rawBlock2[0x29] = value.A;
            }
        }

        public Color HairColor_KanaF
        {
            get { return Color.FromArgb(_rawBlock2[0x2D], _rawBlock2[0x2A], _rawBlock2[0x2B], _rawBlock2[0x2C]); }
            set
            {
                _rawBlock2[0x2A] = value.R;
                _rawBlock2[0x2B] = value.G;
                _rawBlock2[0x2C] = value.B;
                _rawBlock2[0x2D] = value.A;
            }
        }

        public Color HairColor_Shigure
        {
            get { return Color.FromArgb(_rawBlock2[0x31], _rawBlock2[0x2E], _rawBlock2[0x2F], _rawBlock2[0x30]); }
            set
            {
                _rawBlock2[0x2E] = value.R;
                _rawBlock2[0x2F] = value.G;
                _rawBlock2[0x30] = value.B;
                _rawBlock2[0x31] = value.A;
            }
        }

        public Color HairColor_Dwyer
        {
            get { return Color.FromArgb(_rawBlock2[0x35], _rawBlock2[0x32], _rawBlock2[0x33], _rawBlock2[0x34]); }
            set
            {
                _rawBlock2[0x32] = value.R;
                _rawBlock2[0x33] = value.G;
                _rawBlock2[0x34] = value.B;
                _rawBlock2[0x35] = value.A;
            }
        }

        public Color HairColor_Sophie
        {
            get { return Color.FromArgb(_rawBlock2[0x39], _rawBlock2[0x36], _rawBlock2[0x37], _rawBlock2[0x38]); }
            set
            {
                _rawBlock2[0x36] = value.R;
                _rawBlock2[0x37] = value.G;
                _rawBlock2[0x38] = value.B;
                _rawBlock2[0x39] = value.A;
            }
        }

        public Color HairColor_Midori
        {
            get { return Color.FromArgb(_rawBlock2[0x3D], _rawBlock2[0x3A], _rawBlock2[0x3B], _rawBlock2[0x3C]); }
            set
            {
                _rawBlock2[0x3A] = value.R;
                _rawBlock2[0x3B] = value.G;
                _rawBlock2[0x3C] = value.B;
                _rawBlock2[0x3D] = value.A;
            }
        }

        public Color HairColor_Shiro
        {
            get { return Color.FromArgb(_rawBlock2[0x41], _rawBlock2[0x3E], _rawBlock2[0x3F], _rawBlock2[0x40]); }
            set
            {
                _rawBlock2[0x3E] = value.R;
                _rawBlock2[0x3F] = value.G;
                _rawBlock2[0x40] = value.B;
                _rawBlock2[0x41] = value.A;
            }
        }

        public Color HairColor_Kiragi
        {
            get { return Color.FromArgb(_rawBlock2[0x45], _rawBlock2[0x42], _rawBlock2[0x43], _rawBlock2[0x44]); }
            set
            {
                _rawBlock2[0x42] = value.R;
                _rawBlock2[0x43] = value.G;
                _rawBlock2[0x44] = value.B;
                _rawBlock2[0x45] = value.A;
            }
        }

        public Color HairColor_Asugi
        {
            get { return Color.FromArgb(_rawBlock2[0x49], _rawBlock2[0x46], _rawBlock2[0x47], _rawBlock2[0x48]); }
            set
            {
                _rawBlock2[0x46] = value.R;
                _rawBlock2[0x47] = value.G;
                _rawBlock2[0x48] = value.B;
                _rawBlock2[0x49] = value.A;
            }
        }

        public Color HairColor_Selkie
        {
            get { return Color.FromArgb(_rawBlock2[0x4D], _rawBlock2[0x4A], _rawBlock2[0x4B], _rawBlock2[0x4C]); }
            set
            {
                _rawBlock2[0x4A] = value.R;
                _rawBlock2[0x4B] = value.G;
                _rawBlock2[0x4C] = value.B;
                _rawBlock2[0x4D] = value.A;
            }
        }

        public Color HairColor_Hisame
        {
            get { return Color.FromArgb(_rawBlock2[0x51], _rawBlock2[0x4E], _rawBlock2[0x4F], _rawBlock2[0x50]); }
            set
            {
                _rawBlock2[0x4E] = value.R;
                _rawBlock2[0x4F] = value.G;
                _rawBlock2[0x50] = value.B;
                _rawBlock2[0x51] = value.A;
            }
        }

        public Color HairColor_Mitama
        {
            get { return Color.FromArgb(_rawBlock2[0x55], _rawBlock2[0x52], _rawBlock2[0x53], _rawBlock2[0x54]); }
            set
            {
                _rawBlock2[0x52] = value.R;
                _rawBlock2[0x53] = value.G;
                _rawBlock2[0x54] = value.B;
                _rawBlock2[0x55] = value.A;
            }
        }

        public Color HairColor_Caeldori
        {
            get { return Color.FromArgb(_rawBlock2[0x59], _rawBlock2[0x56], _rawBlock2[0x57], _rawBlock2[0x58]); }
            set
            {
                _rawBlock2[0x56] = value.R;
                _rawBlock2[0x57] = value.G;
                _rawBlock2[0x58] = value.B;
                _rawBlock2[0x59] = value.A;
            }
        }

        public Color HairColor_Rhajat
        {
            get { return Color.FromArgb(_rawBlock2[0x5D], _rawBlock2[0x5A], _rawBlock2[0x5B], _rawBlock2[0x5C]); }
            set
            {
                _rawBlock2[0x5A] = value.R;
                _rawBlock2[0x5B] = value.G;
                _rawBlock2[0x5C] = value.B;
                _rawBlock2[0x5D] = value.A;
            }
        }

        public Color HairColor_Siegbert
        {
            get { return Color.FromArgb(_rawBlock2[0x61], _rawBlock2[0x5E], _rawBlock2[0x5F], _rawBlock2[0x60]); }
            set
            {
                _rawBlock2[0x5E] = value.R;
                _rawBlock2[0x5F] = value.G;
                _rawBlock2[0x60] = value.B;
                _rawBlock2[0x61] = value.A;
            }
        }

        public Color HairColor_Forrest
        {
            get { return Color.FromArgb(_rawBlock2[0x65], _rawBlock2[0x62], _rawBlock2[0x63], _rawBlock2[0x64]); }
            set
            {
                _rawBlock2[0x62] = value.R;
                _rawBlock2[0x63] = value.G;
                _rawBlock2[0x64] = value.B;
                _rawBlock2[0x65] = value.A;
            }
        }

        public Color HairColor_Ignatius
        {
            get { return Color.FromArgb(_rawBlock2[0x69], _rawBlock2[0x66], _rawBlock2[0x67], _rawBlock2[0x68]); }
            set
            {
                _rawBlock2[0x66] = value.R;
                _rawBlock2[0x67] = value.G;
                _rawBlock2[0x68] = value.B;
                _rawBlock2[0x69] = value.A;
            }
        }

        public Color HairColor_Velouria
        {
            get { return Color.FromArgb(_rawBlock2[0x6D], _rawBlock2[0x6A], _rawBlock2[0x6B], _rawBlock2[0x6C]); }
            set
            {
                _rawBlock2[0x6A] = value.R;
                _rawBlock2[0x6B] = value.G;
                _rawBlock2[0x6C] = value.B;
                _rawBlock2[0x6D] = value.A;
            }
        }

        public Color HairColor_Percy
        {
            get { return Color.FromArgb(_rawBlock2[0x71], _rawBlock2[0x6E], _rawBlock2[0x6F], _rawBlock2[0x70]); }
            set
            {
                _rawBlock2[0x6E] = value.R;
                _rawBlock2[0x6F] = value.G;
                _rawBlock2[0x70] = value.B;
                _rawBlock2[0x71] = value.A;
            }
        }

        public Color HairColor_Ophelia
        {
            get { return Color.FromArgb(_rawBlock2[0x75], _rawBlock2[0x72], _rawBlock2[0x73], _rawBlock2[0x74]); }
            set
            {
                _rawBlock2[0x72] = value.R;
                _rawBlock2[0x73] = value.G;
                _rawBlock2[0x74] = value.B;
                _rawBlock2[0x75] = value.A;
            }
        }

        public Color HairColor_Soleil
        {
            get { return Color.FromArgb(_rawBlock2[0x79], _rawBlock2[0x76], _rawBlock2[0x77], _rawBlock2[0x78]); }
            set
            {
                _rawBlock2[0x76] = value.R;
                _rawBlock2[0x77] = value.G;
                _rawBlock2[0x78] = value.B;
                _rawBlock2[0x79] = value.A;
            }
        }

        public Color HairColor_Nina
        {
            get { return Color.FromArgb(_rawBlock2[0x7D], _rawBlock2[0x7A], _rawBlock2[0x7B], _rawBlock2[0x7C]); }
            set
            {
                _rawBlock2[0x7A] = value.R;
                _rawBlock2[0x7B] = value.G;
                _rawBlock2[0x7C] = value.B;
                _rawBlock2[0x7D] = value.A;
            }
        }

        // One unknown byte (0x7E)
        // Always 04?

        public string Name_CorrinM
        {
            get { return Utils.TypeConverter.ToString(_rawBlock2.Skip(0x7E).Take(0x18).ToArray()); }
            set { Array.Copy(Utils.TypeConverter.ToByteArray(value, 0x0C), 0x00, _rawBlock2, 0x7E, 0x18); }
        }

        // Ten unknown bytes (0x97 through 0xA0)
        // Probably hair style, face marks, etc. for Corrin M

        public Color HairColor_CorrinM
        {
            get { return Color.FromArgb(_rawBlock2[0xA4], _rawBlock2[0xA1], _rawBlock2[0xA2], _rawBlock2[0xA3]); }
            set
            {
                _rawBlock2[0xA1] = value.R;
                _rawBlock2[0xA2] = value.G;
                _rawBlock2[0xA3] = value.B;
                _rawBlock2[0xA4] = value.A;
            }
        }

        // Five unknown bytes (0xA5 through 0xA9)
        // Probably some last info for Corrin M

        // One unknown byte (0xAA)
        // Always 04?

        public string Name_CorrinF
        {
            get { return Utils.TypeConverter.ToString(_rawBlock2.Skip(0xAA).Take(0x18).ToArray()); }
            set { Array.Copy(Utils.TypeConverter.ToByteArray(value, 0x0C), 0x00, _rawBlock2, 0xAA, 0x18); }
        }

        // Ten unknown bytes (0xC3 through 0xCC)
        // Probably hair style, face marks, etc. for Corrin F

        public Color HairColor_CorrinF
        {
            get { return Color.FromArgb(_rawBlock2[0xD0], _rawBlock2[0xCD], _rawBlock2[0xCE], _rawBlock2[0xCF]); }
            set
            {
                _rawBlock2[0xCD] = value.R;
                _rawBlock2[0xCE] = value.G;
                _rawBlock2[0xCF] = value.B;
                _rawBlock2[0xD0] = value.A;
            }
        }

        // Five unknown bytes (0xD1 through 0xD5)
        // Probably some last info for Corrin F

        // One byte (0xD6)
        // Always 00?

        // End of block and region

        #endregion
    }
}
