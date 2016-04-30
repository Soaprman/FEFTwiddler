using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FEFTwiddler.Extensions;

namespace FEFTwiddler.Model.ChapterSaveRegions
{
    public class WeaponNameRegion
    {
        public WeaponNameRegion(byte[] raw)
        {
            Raw = raw;
        }

        #region Raw data

        public byte[] Raw
        {
            get
            {
                return RawBlock1
                    .Concat(RawWeaponNamesCount.Yield())
                    .Concat(RawWeaponNames)
                    .ToArray();
            }
            set
            {
                using (var ms = new MemoryStream(value))
                using (var br = new BinaryReader(ms))
                {
                    RawBlock1 = br.ReadBytes(RawBlock1Length);

                    WeaponNames = new List<Model.WeaponName>();

                    var nameCount = br.ReadByte();
                    for (var i = 0; i < nameCount; i++)
                    {
                        WeaponNames.Add(ReadNextWeaponName(br));
                    }
                }
            }
        }

        private WeaponName ReadNextWeaponName(BinaryReader br)
        {
            var raw = new List<byte>();

            // ID
            raw.Add(br.ReadByte());

            // Name
            var moreLetters = true;
            while (moreLetters)
            {
                var letter = br.ReadBytes(0x2);
                raw.AddRange(letter);
                if (Enumerable.SequenceEqual(letter, WeaponName.TerminationCharacter)) moreLetters = false;
            }

            return new WeaponName(raw.ToArray());
        }

        public const int RawBlock1Length = 0x05;
        private byte[] _rawBlock1;
        public byte[] RawBlock1
        {
            get { return _rawBlock1; }
            set
            {
                if (value.Length != RawBlock1Length) throw new ArgumentException("WeaponNameRegion block 1 must be " + RawBlock1Length + " bytes in length");
                _rawBlock1 = value;
            }
        }

        public byte RawWeaponNamesCount
        {
            get { return (byte)WeaponNames.Count; }
        }

        public byte[] RawWeaponNames
        {
            get
            {
                var raw = new List<byte>();

                foreach (var weaponName in WeaponNames)
                {
                    raw.AddRange(weaponName.Raw);
                }

                return raw.ToArray();
            }
        }

        #endregion

        // IFER (four bytes) (0x00 through 0x03)

        // One byte (0x04)
        // Always 00?

        public List<WeaponName> WeaponNames { get; set; }
    }
}
