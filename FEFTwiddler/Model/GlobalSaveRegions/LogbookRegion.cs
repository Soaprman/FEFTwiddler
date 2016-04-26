namespace FEFTwiddler.Model.GlobalSaveRegions
{
    public class LogbookRegion
    {
        public LogbookRegion(byte[] raw)
        {
            _raw = raw;
        }

        private byte[] _raw;
        public byte[] Raw
        {
            get { return _raw; }
        }

        // 41GL (four bytes) (0x00 through 0x03)

        // The rest of the bytes

        // The end of this section looks like the LK08 region from ChapterSave
    }
}
