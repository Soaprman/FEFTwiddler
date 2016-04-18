using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEFTwiddler.Model
{
    public class Stat
    {
        /// <summary>
        /// HP
        /// </summary>
        public sbyte HP { get; set; }
        
        /// <summary>
        /// Strength
        /// </summary>
        public sbyte Str { get; set; }

        /// <summary>
        /// Magic
        /// </summary>
        public sbyte Mag { get; set; }

        /// <summary>
        /// Skill
        /// </summary>
        public sbyte Skl { get; set; }

        /// <summary>
        /// Speed
        /// </summary>
        public sbyte Spd { get; set; }

        /// <summary>
        /// Luck
        /// </summary>
        public sbyte Lck { get; set; }

        /// <summary>
        /// Defense
        /// </summary>
        public sbyte Def { get; set; }

        /// <summary>
        /// Resistance
        /// </summary>
        public sbyte Res { get; set; }

        /// <summary>
        /// Add two stat bytes
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
    }
}
