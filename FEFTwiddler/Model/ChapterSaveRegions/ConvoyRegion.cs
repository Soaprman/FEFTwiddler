using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FEFTwiddler.Model.ChapterSaveRegions
{
    public class ConvoyRegion
    {
        public ConvoyRegion(byte[] raw)
        {
            Raw = raw;
        }

        public byte[] Raw
        {
            get
            {
                return RawBlock1
                    .Concat(RawConvoyCount)
                    .Concat(RawConvoy)
                    .ToArray();
            }
            set
            {
                using (var ms = new MemoryStream(value))
                using (var br = new BinaryReader(ms))
                {
                    RawBlock1 = br.ReadBytes(RawBlock1Length);

                    _convoy = new List<ConvoyItem>();

                    ushort itemCount = br.ReadUInt16();
                    for (var i = 0; i < itemCount; i++)
                    {
                        _convoy.Add(new ConvoyItem(br.ReadBytes(0x07)));
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
                if (value.Length != RawBlock1Length) throw new ArgumentException("ConvoyRegion block 1 must be " + RawBlock1Length + " bytes in length");
                _rawBlock1 = value;
            }
        }

        public byte[] RawConvoyCount
        {
            get
            {
                return new byte[] 
                {
                    (byte)(_convoy.Count),
                    (byte)((ushort)(_convoy.Count) >> 8)
                };
            }
        }

        public byte[] RawConvoy
        {
            get
            {
                IEnumerable<byte> rawConvoy = new List<byte>();
                foreach (var item in _convoy)
                {
                    rawConvoy = rawConvoy.Concat(item.Raw);
                }
                return rawConvoy.ToArray();
            }
        }

        // NART (four bytes) (0x00 through 0x03)

        // One byte (0x04)
        // Always 00

        // Two bytes (0x05 through 0x06)
        // Number of items in convoy (ushort)

        private List<ConvoyItem> _convoy;
        public List<ConvoyItem> Convoy
        {
            get { return _convoy; }
        }
    }
}
