using System.Linq;
using System.IO;

namespace FEFTwiddler.Model
{
    /// <summary>
    /// A Fire Emblem Fates "Chapter" save
    /// </summary>
    public class MapSave : IChapterSave
    {
        public Enums.SaveFileType GetSaveFileType() { return Enums.SaveFileType.Map; }

        public static MapSave FromSaveFile(SaveFile file)
        {
            var cs = new MapSave();
            cs.ReadFromFile(file);
            return cs;
        }

        private SaveFile _file;

        public ChapterSaveRegions.Header Header { get; set; }
        public MapSaveRegions.CompressionRegion CompressionRegion { get; set; }
        public ChapterSaveRegions.UserRegion UserRegion { get; set; }
        public MapSaveRegions.PERSRegion PERSRegion { get; set; }
        public ChapterSaveRegions.LK08Region LK08Region { get; set; }
        public ChapterSaveRegions.BattlefieldRegion BattlefieldRegion { get; set; }
        public ChapterSaveRegions.ShopRegion ShopRegion { get; set; }
        public ChapterSaveRegions.UnitRegion UnitRegion { get; set; }
        public ChapterSaveRegions.WeaponNameRegion WeaponNameRegion { get; set; }
        public ChapterSaveRegions.ConvoyRegion ConvoyRegion { get; set; }
        public ChapterSaveRegions.MyCastleRegion MyCastleRegion { get; set; }
        public MapSaveRegions.MapRegion MapRegion { get; set; }

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
                br.BaseStream.Seek(ChapterSaveRegions.Header.Offset, SeekOrigin.Begin);
                Header = new ChapterSaveRegions.Header(br.ReadBytes(ChapterSaveRegions.Header.Length));

                br.BaseStream.Seek(MapSaveRegions.CompressionRegion.Offset, SeekOrigin.Begin);
                CompressionRegion = new MapSaveRegions.CompressionRegion(br.ReadBytes(MapSaveRegions.CompressionRegion.Length));

                UserRegion = new ChapterSaveRegions.UserRegion(br.ReadBytes(CompressionRegion.PERSOffset - CompressionRegion.UserOffset));
                PERSRegion = new MapSaveRegions.PERSRegion(br.ReadBytes(CompressionRegion.LK08Offset - CompressionRegion.PERSOffset));
                LK08Region = new ChapterSaveRegions.LK08Region(br.ReadBytes(CompressionRegion.BattlefieldOffset - CompressionRegion.LK08Offset));
                BattlefieldRegion = new ChapterSaveRegions.BattlefieldRegion(br.ReadBytes(CompressionRegion.ShopOffset - CompressionRegion.BattlefieldOffset));
                ShopRegion = new ChapterSaveRegions.ShopRegion(br.ReadBytes(CompressionRegion.UnitOffset - CompressionRegion.ShopOffset));
                UnitRegion = new ChapterSaveRegions.UnitRegion(br.ReadBytes(CompressionRegion.WeaponNameOffset - CompressionRegion.UnitOffset));
                WeaponNameRegion = new ChapterSaveRegions.WeaponNameRegion(br.ReadBytes(CompressionRegion.ConvoyOffset - CompressionRegion.WeaponNameOffset));
                ConvoyRegion = new ChapterSaveRegions.ConvoyRegion(br.ReadBytes(CompressionRegion.MyCastleOffset - CompressionRegion.ConvoyOffset));
                MyCastleRegion = new ChapterSaveRegions.MyCastleRegion(br.ReadBytes(CompressionRegion.MapOffset - CompressionRegion.MyCastleOffset));
                MapRegion = new MapSaveRegions.MapRegion(br.ReadBytes((int)br.BaseStream.Length - CompressionRegion.MapOffset));
            }
        }

        public void Write()
        {
            // Get bytes from regions
            var userBytes = UserRegion.Raw;
            var presBytes = PERSRegion.Raw;
            var lk08Bytes = LK08Region.Raw;
            var battlefieldBytes = BattlefieldRegion.Raw;
            var shopBytes = ShopRegion.Raw;
            var unitBytes = UnitRegion.Raw;
            var weaponNameBytes = WeaponNameRegion.Raw;
            var convoyBytes = ConvoyRegion.Raw;
            var myCastleBytes = MyCastleRegion.Raw;
            var mapBytes = MapRegion.Raw;

            // Update offsets, in case regions changed size
            var offset = CompressionRegion.UserOffset; // First region, offset is always the same
            offset += userBytes.Length;
            CompressionRegion.PERSOffset = offset;
            offset += presBytes.Length;
            CompressionRegion.LK08Offset = offset;
            offset += lk08Bytes.Length;
            CompressionRegion.BattlefieldOffset = offset;
            offset += battlefieldBytes.Length;
            CompressionRegion.ShopOffset = offset;
            offset += shopBytes.Length;
            CompressionRegion.UnitOffset = offset;
            offset += unitBytes.Length;
            CompressionRegion.WeaponNameOffset = offset;
            offset += weaponNameBytes.Length;
            CompressionRegion.ConvoyOffset = offset;
            offset += convoyBytes.Length;
            CompressionRegion.MyCastleOffset = offset;
            offset += myCastleBytes.Length;
            CompressionRegion.MapOffset = offset;

            // Combine regions and write to file
            _file.DecompressedBytes = Header.Raw
                .Concat(CompressionRegion.Raw)
                .Concat(userBytes)
                .Concat(presBytes)
                .Concat(lk08Bytes)
                .Concat(battlefieldBytes)
                .Concat(shopBytes)
                .Concat(unitBytes)
                .Concat(weaponNameBytes)
                .Concat(convoyBytes)
                .Concat(myCastleBytes)
                .Concat(mapBytes)
                .ToArray();

            _file.Write();
        }

        #endregion
    }
}
