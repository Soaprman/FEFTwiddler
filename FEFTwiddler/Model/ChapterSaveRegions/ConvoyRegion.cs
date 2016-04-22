namespace FEFTwiddler.Model.ChapterSaveRegions
{
    public class ConvoyRegion
    {
        public ConvoyRegion(byte[] raw)
        {
            _raw = raw;
        }

        private byte[] _raw;
        public byte[] Raw
        {
            get { return _raw; }
        }

        // NART (four bytes) (0x00 through 0x03)
    }
}
