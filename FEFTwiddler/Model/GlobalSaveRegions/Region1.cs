namespace FEFTwiddler.Model.GlobalSaveRegions
{
    public class Region1
    {
        public Region1(byte[] raw)
        {
            _raw = raw;
        }

        private byte[] _raw;
        public byte[] Raw
        {
            get { return _raw; }
        }

        // 1ULG (four bytes) (0x00 through 0x03)

        // The rest of the bytes
    }
}
