namespace FEFTwiddler.Model.MapSaveRegions
{
    /// <summary>
    /// I sure do wish I had a better name for this!
    /// </summary>
    public class MapRegion
    {
        public MapRegion(byte[] raw)
        {
            _raw = raw;
        }

        private byte[] _raw;
        public byte[] Raw
        {
            get { return _raw; }
        }

        // " PAM" (four bytes) (0x00 through 0x03)
        
        // Unknown bytes for the rest of it
    }
}
