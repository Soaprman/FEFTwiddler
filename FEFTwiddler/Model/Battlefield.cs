using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEFTwiddler.Model
{
    public class Battlefield
    {
        private byte[] _raw;
        public byte[] Raw
        {
            get { return _raw; }
        }

        public Battlefield(byte[] raw)
        {
            if (raw.Length != 0x21) throw new ArgumentException("Battlefield Entries must be 33 (0x21) bytes");
            _raw = raw;
        }

        // TODO: Figure out what this value *really* is and finish the enum for it
        public byte BattlefieldID
        {
            get { return _raw[0x01]; }
            set { _raw[0x01] = value; }
        }

        public Enums.Chapter ChapterID
        {
            get { return (Enums.Chapter)_raw[0x02]; }
            set { _raw[0x02] = (byte)value; }
        }

        public Enums.BattlefieldStatus BattlefieldStatus
        {
            get { return (Enums.BattlefieldStatus)_raw[0x03]; }
            set { _raw[0x03] = (byte)value; }
        }

        // One unknown byte (0x04)
        // Always 00?

        /// <summary>
        /// It's 0xFF on Available or Unavailable battlefields.
        /// It has a value on Completed battlefields.
        /// The value may indicate order of completion.
        /// In my RV save, the child paralogues all had 0x24 and 0x26.
        /// </summary>
        public byte Unknown_0x05
        {
            get { return _raw[0x05]; }
            set { _raw[0x05] = value; }
        }

        /// <summary>
        /// It's 0xFF on Available or Unavailable battlefields.
        /// It's 0x00 on Completed battlefields.
        /// </summary>
        public byte Unknown_0x06
        {
            get { return _raw[0x06]; }
            set { _raw[0x06] = value; }
        }

        /// <summary>
        /// A hack for this until the data is better documented
        /// </summary>
        public void RemoveEnemies()
        {
            var nothingToSeeHere = new byte[]
            {
                0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xFF, 0x00, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xFF
            };

            Array.Copy(nothingToSeeHere, 0x00, _raw, 0x07, nothingToSeeHere.Length);
        }

        /* About everything else:
         * 
         * 0x07
         * Always 00
         * 
         * 0x08-0x09
         * These values are both 0x00 when no enemy is here.
         * These values are both 0x01 when an enemy is present.
         * Two enemies on one spot have not been tested.
         * 
         * 0x0A-0x0B
         * The class ID of the first enemy.
         * FF FF otherwise.
         * 
         * 0x0C-0x12
         * Stuff related to the first enemy.
         * 
         * 0x13
         * Always FF
         * 
         * 0x14
         * Always 00
         * 
         * 0x15-0x16
         * These values are both 0x00 when no enemy is here.
         * These values are both 0x01 when an enemy is present.
         * Two enemies on one spot have not been tested.
         * 
         * 0x17-0x18
         * The class ID of the second enemy.
         * FF FF otherwise.
         * 
         * 0x19-0x1F
         * Stuff related to the second enemy.
         * 
         * 0x20
         * Always FF
         * 
         * Values to think about:
         * Enemy level
         * Number of enemies
         * Enemy layout (maybe an algorithm instead of an ID)
         * Items carried
         * Gold carried
         * 
         * */
    }
}
