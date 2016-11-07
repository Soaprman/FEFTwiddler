using System;
using System.Collections.Generic;
using System.Linq;

namespace FEFTwiddler.Model
{
    public class Building
    {
        private byte[] _raw;
        public byte[] Raw
        {
            get
            {
                if (IsLilithsTemple())
                {
                    // Re-add the four 00 bytes
                    IEnumerable<byte> before = _raw.Take(0x02);
                    IEnumerable<byte> after = _raw.Skip(0x02).Take(0x0B);

                    return before
                        .Concat(new byte[] { 0x00, 0x00, 0x00, 0x00 })
                        .Concat(after)
                        .ToArray();
                }
                else { return _raw; }
            }
        }

        public Building(byte[] raw)
        {
            if (raw.Length != 0x0D) throw new ArgumentException("Building Entries must be 13 (0x0D) bytes");
            _raw = raw;
        }

        public Enums.Building BuildingID
        {
            get { return (Enums.Building)(ushort)((_raw[0x01] * 0x100) + _raw[0x00]); }
            set
            {
                // To my knowledge, this value goes in both spots
                _raw[0x00] = (byte)(((ushort)value) & 0xFF);
                _raw[0x01] = (byte)(((ushort)value >> 8) & 0xFF);
                _raw[0x03] = (byte)(((ushort)value) & 0xFF);
                _raw[0x04] = (byte)(((ushort)value >> 8) & 0xFF);
            }
        }

        // To my knowledge, this is always 01.
        // It could be something silly like an "on the map" bit. I dunno.
        public byte Unknown02
        {
            get { return _raw[0x02]; }
            set { _raw[0x02] = value; }
        }

        // 0x03 and 0x04 are also BuildingID (see above)

        /// <summary>
        /// Position from left. Range: 0x01 through 0x1D (29)
        /// </summary>
        public byte LeftPosition
        {
            get { return _raw[0x05]; }
            set { _raw[0x05] = value; }
        }

        /// <summary>
        /// Position from top. Range: 0x01 through 0x1E (30)
        /// </summary>
        public byte TopPosition
        {
            get { return _raw[0x06]; }
            set { _raw[0x06] = value; }
        }

        /// <summary>
        /// 0: down; 1: left; 2: up; 3: right
        /// </summary>
        public byte DirectionFacing
        {
            get { return _raw[0x07]; }
            set { _raw[0x07] = value; }
        }

        // Four unknown bytes (0x08 through 0x0B)
        // They seem random on most things but sometimes all four make 00000000.
        // Values appear to be everywhere between 00 and FF for each byte.

        /// <summary>
        /// Not actually sure what this is for. But it's 00 on statues and 01 on everything else
        /// </summary>
        /// <remarks>
        /// It's also 01 on Travelers' Plaza, so it probably isn't size directly, though it is probably related to size
        /// It could also be a flag that determines whether something can't be interacted with?
        /// </remarks>
        public byte NotAStatue
        {
            get { return _raw[0x0C]; }
            set { _raw[0x0C] = value; }
        }

        public bool IsLilithsTemple()
        {
            return BuildingID == Enums.Building.LilithsTemple_1 ||
                BuildingID == Enums.Building.LilithsTemple_2 ||
                BuildingID == Enums.Building.LilithsTemple_3;
        }

        public bool IsTravelersPlaza()
        {
            return BuildingID == Enums.Building.TravelersPlaza_1 ||
                BuildingID == Enums.Building.TravelersPlaza_2 ||
                BuildingID == Enums.Building.TravelersPlaza_3;
        }

        public override string ToString()
        {
            return GetCondensedInformation();
        }

        public string GetCondensedInformation()
        {
            string name, left, top, facing;

            name = BuildingID.ToString();
            left = LeftPosition.ToString();
            top = TopPosition.ToString();

            switch (DirectionFacing)
            {
                case 0: facing = "down"; break;
                case 1: facing = "left"; break;
                case 2: facing = "up"; break;
                case 3: facing = "right"; break;
                default: facing = "???"; break;
            }

            return string.Format("{0} -- position: (left: {1}, top: {2}) -- facing: {3}", name, left, top, facing);
        }
    }
}
