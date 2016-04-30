namespace FEFTwiddler.Enums
{
    public enum UnitBlock : byte
    {
        Deployed = 0x00,
        // There might be "allied" and "enemy" blocks in Map saves.
        Living = 0x03,
        DeadByGameplay = 0x04,
        Absent = 0x05,
        DeadByPlot = 0x06,
        End = 0xFF
    }
}
