using System.IO;
using System.Linq;

namespace FEFTwiddler.Model
{
    public class GlobalSave
    {
        public static GlobalSave FromSaveFile(SaveFile file)
        {
            var s = new GlobalSave();
            s.ReadFromFile(file);
            return s;
        }

        private SaveFile _file;

        public GlobalSaveRegions.CompressionRegion CompressionRegion;
        public GlobalSaveRegions.Region1 Region1;
        public GlobalSaveRegions.Region2 Region2;

        #region General IO

        private void ReadFromFile(SaveFile file)
        {
            _file = file;
            Read();
        }

        public void Read()
        {
            using (var ms = new MemoryStream(_file.DecompressedBytes))
            using (var br = new BinaryReader(ms))
            {
                br.BaseStream.Seek(GlobalSaveRegions.CompressionRegion.Offset, SeekOrigin.Begin);
                CompressionRegion = new GlobalSaveRegions.CompressionRegion(br.ReadBytes(GlobalSaveRegions.CompressionRegion.Length));

                Region1 = new GlobalSaveRegions.Region1(br.ReadBytes(CompressionRegion.Region2Offset - CompressionRegion.Region1Offset));
                Region2 = new GlobalSaveRegions.Region2(br.ReadBytes((int)br.BaseStream.Length - CompressionRegion.Region2Offset));
            }
        }

        public void Write()
        {
            // Get bytes from regions
            var region1Bytes = Region1.Raw;
            var region2Bytes = Region2.Raw;

            // Update offsets, in case regions changed size
            var offset = CompressionRegion.Region1Offset; // First region, offset is always the same
            offset += region1Bytes.Length;
            CompressionRegion.Region2Offset = offset;

            // Combine regions and write to file
            _file.DecompressedBytes = CompressionRegion.Raw
                .Concat(region1Bytes)
                .Concat(region2Bytes)
                .ToArray();

            _file.Write();
        }

        #endregion
    }
}
