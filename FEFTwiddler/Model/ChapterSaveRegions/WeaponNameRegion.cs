namespace FEFTwiddler.Model.ChapterSaveRegions
{
    public class WeaponNameRegion
    {
        public WeaponNameRegion(byte[] raw)
        {
            _raw = raw;
        }

        private byte[] _raw;
        public byte[] Raw
        {
            get { return _raw; }
        }

        // IFER (four bytes) (0x00 through 0x03)
    }
}
