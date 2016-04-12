namespace FEFTwiddler.Enums
{
    // TODO: Figure this whole thing out.
    // This is a total guess for now as how to this would be implemented.
    public enum Map : byte
    {
        SomeDefaultValue = 0x00,
        // TODO: A value for between battles (save prompt after finishing a battle)
        RevelationChapter15 = 0x3F, // Source: guess based on cokacommando's save (it's the one called "rainbow sage")
        MyCastle = 0x4B, // Could have variants based on castle style?
        ThievesDen = 0x55, // challenge battle
        PoachersForest = 0x56 // challenge battle
    }
}
