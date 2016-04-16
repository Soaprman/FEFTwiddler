using System;

namespace FEFTwiddler.Enums
{
    [Flags]
    public enum Route : byte
    {
        None = 0x0,
        Birthright = 0x1,
        Conquest = 0x2,
        Revelation = 0x4
    }
}
