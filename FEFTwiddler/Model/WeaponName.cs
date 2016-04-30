using System;
using System.Linq;
using FEFTwiddler.Extensions;

namespace FEFTwiddler.Model
{
    public class WeaponName
    {
        private byte[] _raw;
        public byte[] Raw
        {
            get { return _raw; }
        }

        public WeaponName(byte[] raw)
        {
            _raw = raw;
        }

        public byte ID
        {
            get { return _raw[0x00]; }
            set { _raw[0x00] = value; }
        }

        public const int NameMaxLength = 0x10; // 16 characters
        public static byte[] TerminationCharacter = new byte[] { 0x00, 0x00 };
        public string Name
        {
            get
            {
                // Skip the ID byte. ToString chops off the termination character
                return Utils.TypeConverter.ToString(_raw.Skip(0x01).ToArray());
            }
            set
            {
                if (value.Length > NameMaxLength) throw new ArgumentException("Weapon names cannot be longer than 16 characters");
                var bytes = Utils.TypeConverter.ToByteArray(value, value.Length);
                // Re-add the termination character
                _raw = (ID.Yield()).Concat(bytes).Concat(TerminationCharacter).ToArray();
            }
        }

        public override string ToString()
        {
            return string.Format("ID: {0} / {1}", this.ID.ToString(), this.Name);
        }
    }
}
