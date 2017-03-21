namespace FEFTwiddler.Model.MapSaveRegions
{
    /// <summary>
    /// I sure do wish I had a better name for this!
    /// </summary>
    public class PERSRegion
    {
        public PERSRegion(byte[] raw)
        {
            _raw = raw;
        }

        private byte[] _raw;
        public byte[] Raw
        {
            get { return _raw; }
        }

        // SREP (four bytes) (0x00 through 0x03)
        
        // Two more bytes (0x04 through 0x05), 01 00
    }
}
