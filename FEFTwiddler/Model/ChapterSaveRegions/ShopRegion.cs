using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FEFTwiddler.Model.ChapterSaveRegions
{
    public class ShopRegion
    {
        public ShopRegion(byte[] raw)
        {
            Raw = raw;
        }

        #region Raw Data

        public byte[] Raw
        {
            get
            {
                return RawBlock1
                    .Concat(RawHoshidoArmory)
                    .Concat(RawNohrArmory)
                    .Concat(RawHoshidoStore)
                    .Concat(RawNohrStore)
                    .ToArray();
            }
            set
            {
                using (var ms = new MemoryStream(value))
                using (var br = new BinaryReader(ms))
                {
                    RawBlock1 = br.ReadBytes(RawBlock1Length);

                    byte[] chunk; byte itemCount;

                    // Dawn Armory
                    chunk = new byte[0x03];
                    br.Read(chunk, 0, 0x03);

                    HoshidoArmory = new Shop();
                    HoshidoArmory.Level = chunk[1];
                    itemCount = chunk[2];
                    for (var i = 0; i < itemCount; i++)
                    {
                        HoshidoArmory.Items.Add(new ShopItem(br.ReadBytes(0x04)));
                    }

                    // Dusk Armory
                    chunk = new byte[0x03];
                    br.Read(chunk, 0, 0x03);

                    NohrArmory = new Shop();
                    NohrArmory.Level = chunk[1];
                    itemCount = chunk[2];
                    for (var i = 0; i < itemCount; i++)
                    {
                        NohrArmory.Items.Add(new ShopItem(br.ReadBytes(0x04)));
                    }

                    // Rod Shop
                    chunk = new byte[0x03];
                    br.Read(chunk, 0, 0x03);

                    HoshidoStore = new Shop();
                    HoshidoStore.Level = chunk[1];
                    itemCount = chunk[2];
                    for (var i = 0; i < itemCount; i++)
                    {
                        HoshidoStore.Items.Add(new ShopItem(br.ReadBytes(0x04)));
                    }

                    // Staff Store
                    chunk = new byte[0x03];
                    br.Read(chunk, 0, 0x03);

                    NohrStore = new Shop();
                    NohrStore.Level = chunk[1];
                    itemCount = chunk[2];
                    for (var i = 0; i < itemCount; i++)
                    {
                        NohrStore.Items.Add(new ShopItem(br.ReadBytes(0x04)));
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
                if (value.Length != RawBlock1Length) throw new ArgumentException("ShopRegion block 1 must be " + RawBlock1Length + " bytes in length");
                _rawBlock1 = value;
            }
        }

        public byte[] RawHoshidoArmory
        {
            get
            {
                IEnumerable<byte> rawHoshidoArmory = new List<byte> { 0x00, HoshidoArmory.Level, (byte)HoshidoArmory.Items.Count };
                foreach (var item in HoshidoArmory.Items)
                {
                    rawHoshidoArmory = rawHoshidoArmory.Concat(item.Raw);
                }
                return rawHoshidoArmory.ToArray();
            }
        }

        public byte[] RawNohrArmory
        {
            get
            {
                IEnumerable<byte> rawNohrArmory = new List<byte> { 0x00, NohrArmory.Level, (byte)NohrArmory.Items.Count };
                foreach (var item in NohrArmory.Items)
                {
                    rawNohrArmory = rawNohrArmory.Concat(item.Raw);
                }
                return rawNohrArmory.ToArray();
            }
        }

        public byte[] RawHoshidoStore
        {
            get
            {
                IEnumerable<byte> rawHoshidoStore = new List<byte> { 0x00, HoshidoStore.Level, (byte)HoshidoStore.Items.Count };
                foreach (var item in HoshidoStore.Items)
                {
                    rawHoshidoStore = rawHoshidoStore.Concat(item.Raw);
                }
                return rawHoshidoStore.ToArray();
            }
        }

        public byte[] RawNohrStore
        {
            get
            {
                IEnumerable<byte> rawNohrStore = new List<byte> { 0x00, NohrStore.Level, (byte)NohrStore.Items.Count };
                foreach (var item in NohrStore.Items)
                {
                    rawNohrStore = rawNohrStore.Concat(item.Raw);
                }
                return rawNohrStore.ToArray();
            }
        }

        #endregion

        #region Block 1 Properties

        // POHS (four bytes) (0x00 through 0x03)

        // One uknown byte (0x04)
        // Probably always 00

        #endregion

        #region Hoshido Armory Properties

        public Shop HoshidoArmory { get; set; }

        #endregion

        #region Nohr Armory Properties

        public Shop NohrArmory { get; set; }

        #endregion

        #region Hoshido Store Properties

        public Shop HoshidoStore { get; set; }

        #endregion

        #region Nohr Store Properties

        public Shop NohrStore { get; set; }

        #endregion
    }
}
