namespace FEFTwiddler.Model.ChapterSaveRegions
{
    /// <summary>
    /// I sure do wish I had a better name for this!
    /// </summary>
    public class LK08Region
    {
        public LK08Region(byte[] raw)
        {
            _raw = raw;
        }

        private byte[] _raw;
        public byte[] Raw
        {
            get { return _raw; }
        }

        // 80KL (four bytes) (0x00 through 0x03)
        // This is 70KL on 9Hopper's save, which also seems to have no indication that DLC is used with it

        // Current offset: four bytes (0x04 through 0x07)
        // This is some sort of YOU ARE HERE value. The value here actually points to the current offset.
        // It's a mystery to me, too.

        // The last four bytes (before TOPS in the next region) are another YOU ARE HERE offset.
    }
}
