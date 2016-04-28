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
                    .Concat(RawHairColorCount1)
                    .Concat(RawHairColorCount2)
                    .Concat(RawHairColors)
                    .Concat(RawBlock3)
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

                    HairColorCount1 = br.ReadUInt32();
                    HairColorCount2 = br.ReadUInt32();

                    RawHairColors = br.ReadBytes((int)HairColorCount1 * 4);

                    RawBlock3 = br.ReadBytes(RawBlock3Length);
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

        public const int RawBlock2Length = 0x1E;
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

        public byte[] RawHairColorCount1
        {
            get
            {
                return new byte[]
                {
                    (byte)(HairColorCount1),
                    (byte)(HairColorCount1 >> 8),
                    (byte)(HairColorCount1 >> 16),
                    (byte)(HairColorCount1 >> 24)
                };
            }
        }

        public byte[] RawHairColorCount2
        {
            get
            {
                return new byte[]
                {
                    (byte)(HairColorCount2),
                    (byte)(HairColorCount2 >> 8),
                    (byte)(HairColorCount2 >> 16),
                    (byte)(HairColorCount2 >> 24)
                };
            }
        }

        private byte[] _rawHairColors;
        public byte[] RawHairColors
        {
            get { return _rawHairColors; }
            set
            {
                _rawHairColors = value;
            }
        }

        public const int RawBlock3Length = 0x59;
        private byte[] _rawBlock3;
        public byte[] RawBlock3
        {
            get { return _rawBlock3; }
            set
            {
                if (value.Length != RawBlock3Length) throw new ArgumentException("Region1 block 3 must be " + RawBlock3Length + " bytes in length");
                _rawBlock3 = value;
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

        // Might be dynamic size based on UnlockedCharacterCount
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

        // Two unknown bytes (0x1B through 0x1C)
        // Always 00 00?

        #endregion

        #region Hair Color Block

        public uint HairColorCount1
        {
            get; set;
        }

        public uint HairColorCount2
        {
            get; set;
        }

        // Up to eighty-eight (0x58) bytes (0x00 through 0x57)
        // Why there gotta be so many of these damn kids?????

        public Color HairColor_KanaM
        {
            get { return GetChildHairColor(0x00); }
            set { SetChildHairColor(0x00, value); }
        }

        public Color HairColor_KanaF
        {
            get { return GetChildHairColor(0x04); }
            set { SetChildHairColor(0x04, value); }
        }

        public Color HairColor_Shigure
        {
            get { return GetChildHairColor(0x08); }
            set { SetChildHairColor(0x08, value); }
        }

        public Color HairColor_Dwyer
        {
            get { return GetChildHairColor(0x0C); }
            set { SetChildHairColor(0x0C, value); }
        }

        public Color HairColor_Sophie
        {
            get { return GetChildHairColor(0x10); }
            set { SetChildHairColor(0x10, value); }
        }

        public Color HairColor_Midori
        {
            get { return GetChildHairColor(0x14); }
            set { SetChildHairColor(0x14, value); }
        }

        public Color HairColor_Shiro
        {
            get { return GetChildHairColor(0x18); }
            set { SetChildHairColor(0x18, value); }
        }

        public Color HairColor_Kiragi
        {
            get { return GetChildHairColor(0x1C); }
            set { SetChildHairColor(0x1C, value); }
        }

        public Color HairColor_Asugi
        {
            get { return GetChildHairColor(0x20); }
            set { SetChildHairColor(0x20, value); }
        }

        public Color HairColor_Selkie
        {
            get { return GetChildHairColor(0x24); }
            set { SetChildHairColor(0x24, value); }
        }

        public Color HairColor_Hisame
        {
            get { return GetChildHairColor(0x28); }
            set { SetChildHairColor(0x28, value); }
        }

        public Color HairColor_Mitama
        {
            get { return GetChildHairColor(0x2C); }
            set { SetChildHairColor(0x2C, value); }
        }

        public Color HairColor_Caeldori
        {
            get { return GetChildHairColor(0x30); }
            set { SetChildHairColor(0x30, value); }
        }

        public Color HairColor_Rhajat
        {
            get { return GetChildHairColor(0x34); }
            set { SetChildHairColor(0x34, value); }
        }

        public Color HairColor_Siegbert
        {
            get { return GetChildHairColor(0x38); }
            set { SetChildHairColor(0x38, value); }
        }

        public Color HairColor_Forrest
        {
            get { return GetChildHairColor(0x3C); }
            set { SetChildHairColor(0x3C, value); }
        }

        public Color HairColor_Ignatius
        {
            get { return GetChildHairColor(0x40); }
            set { SetChildHairColor(0x40, value); }
        }

        public Color HairColor_Velouria
        {
            get { return GetChildHairColor(0x44); }
            set { SetChildHairColor(0x44, value); }
        }

        public Color HairColor_Percy
        {
            get { return GetChildHairColor(0x48); }
            set { SetChildHairColor(0x48, value); }
        }

        public Color HairColor_Ophelia
        {
            get { return GetChildHairColor(0x4C); }
            set { SetChildHairColor(0x4C, value); }
        }

        public Color HairColor_Soleil
        {
            get { return GetChildHairColor(0x50); }
            set { SetChildHairColor(0x50, value); }
        }

        public Color HairColor_Nina
        {
            get { return GetChildHairColor(0x54); }
            set { SetChildHairColor(0x54, value); }
        }

        private Color GetChildHairColor(int offset)
        {
            // Convert alpha to 255 here
            try { return Color.FromArgb(255, _rawHairColors[offset], _rawHairColors[offset + 1], _rawHairColors[offset + 2]); }
            catch (IndexOutOfRangeException)
            {
                // There's no such thing as "Color.None", so here's a color that Fates will never produce.
                // This way, the GUI can tell that no color was found and make its own decisions.
                return Color.FromArgb(1,0,0,0);
            }
        }

        private void SetChildHairColor(int offset, Color color)
        {
            try
            {
                _rawHairColors[offset] = color.R;
                _rawHairColors[offset + 1] = color.G;
                _rawHairColors[offset + 2] = color.B;
                _rawHairColors[offset + 3] = color.A;
            }
            catch (IndexOutOfRangeException) { return; }
        }

        #endregion

        #region Block 3

        // One unknown byte (0x00)
        // Always 04?

        public string Name_CorrinM
        {
            get { return Utils.TypeConverter.ToString(_rawBlock3.Skip(0x01).Take(0x18).ToArray()); }
            set { Array.Copy(Utils.TypeConverter.ToByteArray(value, 0x0C), 0x00, _rawBlock3, 0x01, 0x18); }
        }

        // Ten unknown bytes (0x19 through 0x22)
        // Probably hair style, face marks, etc. for Corrin M

        public Color HairColor_CorrinM
        {
            get { return Color.FromArgb(_rawBlock3[0x26], _rawBlock3[0x23], _rawBlock3[0x24], _rawBlock3[0x25]); }
            set
            {
                _rawBlock3[0x23] = value.R;
                _rawBlock3[0x24] = value.G;
                _rawBlock3[0x25] = value.B;
                _rawBlock3[0x26] = value.A;
            }
        }

        // Five unknown bytes (0x27 through 0x2B)
        // Probably some last info for Corrin M

        // One unknown byte (0x2C)
        // Always 04?

        public string Name_CorrinF
        {
            get { return Utils.TypeConverter.ToString(_rawBlock3.Skip(0x2D).Take(0x18).ToArray()); }
            set { Array.Copy(Utils.TypeConverter.ToByteArray(value, 0x0C), 0x00, _rawBlock3, 0x2D, 0x18); }
        }

        // Ten unknown bytes (0x45 through 0x4E)
        // Probably hair style, face marks, etc. for Corrin F

        public Color HairColor_CorrinF
        {
            get { return Color.FromArgb(_rawBlock3[0x52], _rawBlock3[0x4F], _rawBlock3[0x50], _rawBlock3[0x51]); }
            set
            {
                _rawBlock3[0x4F] = value.R;
                _rawBlock3[0x50] = value.G;
                _rawBlock3[0x51] = value.B;
                _rawBlock3[0x52] = value.A;
            }
        }

        // Five unknown bytes (0x53 through 0x57)
        // Probably some last info for Corrin F

        // One byte (0x58)
        // Always 00?

        // End of block and region

        #endregion
    }
}
