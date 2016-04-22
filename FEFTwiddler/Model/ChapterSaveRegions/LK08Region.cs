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
    }
}
