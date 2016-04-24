using System;

namespace FEFTwiddler.Model
{
    /// <summary>
    /// Used for both stats and stat gains.
    /// Despite using sbytes, this is safe to use for gains because no combined growths go over 127% in Fates... I think.
    /// </summary>
    public class Stat
    {
        private byte[] _raw;
        public byte[] Raw
        {
            get { return _raw; }
        }

        public Stat(byte[] raw)
        {
            _raw = raw;
        }

        public Stat()
        {
            _raw = new byte[0x08];
            HP = 0;
            Str = 0;
            Mag = 0;
            Skl = 0;
            Spd = 0;
            Lck = 0;
            Def = 0;
            Res = 0;
        }

        /// <summary>
        /// HP
        /// </summary>
        public sbyte HP
        {
            get { return (sbyte)_raw[0x00]; }
            set { _raw[0x00] = (byte)value; }
        }
        
        /// <summary>
        /// Strength
        /// </summary>
        public sbyte Str
        {
            get { return (sbyte)_raw[0x01]; }
            set { _raw[0x01] = (byte)value; }
        }

        /// <summary>
        /// Magic
        /// </summary>
        public sbyte Mag
        {
            get { return (sbyte)_raw[0x02]; }
            set { _raw[0x02] = (byte)value; }
        }

        /// <summary>
        /// Skill
        /// </summary>
        public sbyte Skl
        {
            get { return (sbyte)_raw[0x03]; }
            set { _raw[0x03] = (byte)value; }
        }

        /// <summary>
        /// Speed
        /// </summary>
        public sbyte Spd
        {
            get { return (sbyte)_raw[0x04]; }
            set { _raw[0x04] = (byte)value; }
        }

        /// <summary>
        /// Luck
        /// </summary>
        public sbyte Lck
        {
            get { return (sbyte)_raw[0x05]; }
            set { _raw[0x05] = (byte)value; }
        }

        /// <summary>
        /// Defense
        /// </summary>
        public sbyte Def
        {
            get { return (sbyte)_raw[0x06]; }
            set { _raw[0x06] = (byte)value; }
        }

        /// <summary>
        /// Resistance
        /// </summary>
        public sbyte Res
        {
            get { return (sbyte)_raw[0x07]; }
            set { _raw[0x07] = (byte)value; }
        }

        /// <summary>
        /// Add two stats.
        /// </summary>
        /// <param name="s1">First stat bytes</param>
        /// <param name="s2">Second stat bytes</param>
        /// <returns>A new stat bytes</returns>
        public static Stat operator +(Stat s1, Stat s2)
        {
            return new Stat()
            {
                HP = (sbyte)(s1.HP + s2.HP),
                Str = (sbyte)(s1.Str + s2.Str),
                Mag = (sbyte)(s1.Mag + s2.Mag),
                Skl = (sbyte)(s1.Skl + s2.Skl),
                Spd = (sbyte)(s1.Spd + s2.Spd),
                Lck = (sbyte)(s1.Lck + s2.Lck),
                Def = (sbyte)(s1.Def + s2.Def),
                Res = (sbyte)(s1.Res + s2.Res)
            };
        }

        /// <summary>
        /// Subtract two stats.
        /// </summary>
        /// <param name="s1"></param>
        /// <param name="s2"></param>
        /// <returns></returns>
        public static Stat operator -(Stat s1, Stat s2)
        {
            return new Stat()
            {
                HP = (sbyte)(s1.HP - s2.HP),
                Str = (sbyte)(s1.Str - s2.Str),
                Mag = (sbyte)(s1.Mag - s2.Mag),
                Skl = (sbyte)(s1.Skl - s2.Skl),
                Spd = (sbyte)(s1.Spd - s2.Spd),
                Lck = (sbyte)(s1.Lck - s2.Lck),
                Def = (sbyte)(s1.Def - s2.Def),
                Res = (sbyte)(s1.Res - s2.Res)
            };
        }

        /// <summary>
        /// Return stat string.
        /// </summary>
        /// <returns>Stat string</returns>
        public override string ToString()
        {
            return String.Format("{0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", HP, Str, Mag, Skl, Spd, Lck, Def, Res);
        }
    }
}
