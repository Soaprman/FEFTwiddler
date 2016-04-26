using System.Linq;
using System.IO;

namespace FEFTwiddler.Model
{
    /// <summary>
    /// A Fire Emblem Fates "Chapter" save
    /// </summary>
    public class ChapterSave : ISave
    {
        public static ChapterSave FromSaveFile(SaveFile file)
        {
            var cs = new ChapterSave();
            cs.ReadFromFile(file);
            return cs;
        }

        private SaveFile _file;

        public ChapterSaveRegions.Header Header;
        public ChapterSaveRegions.CompressionRegion CompressionRegion;
        public ChapterSaveRegions.UserRegion UserRegion;
        public ChapterSaveRegions.LK08Region LK08Region;
        public ChapterSaveRegions.BattlefieldRegion BattlefieldRegion;
        public ChapterSaveRegions.ShopRegion ShopRegion;
        public ChapterSaveRegions.UnitRegion UnitRegion;
        public ChapterSaveRegions.WeaponNameRegion WeaponNameRegion;
        public ChapterSaveRegions.ConvoyRegion ConvoyRegion;
        public ChapterSaveRegions.MyCastleRegion MyCastleRegion;

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

                br.BaseStream.Seek(ChapterSaveRegions.CompressionRegion.Offset, SeekOrigin.Begin);
                CompressionRegion = new ChapterSaveRegions.CompressionRegion(br.ReadBytes(ChapterSaveRegions.CompressionRegion.Length));

                UserRegion = new ChapterSaveRegions.UserRegion(br.ReadBytes(CompressionRegion.LK08Offset - CompressionRegion.UserOffset));
                LK08Region = new ChapterSaveRegions.LK08Region(br.ReadBytes(CompressionRegion.BattlefieldOffset - CompressionRegion.LK08Offset));
                BattlefieldRegion = new ChapterSaveRegions.BattlefieldRegion(br.ReadBytes(CompressionRegion.ShopOffset - CompressionRegion.BattlefieldOffset));
                ShopRegion = new ChapterSaveRegions.ShopRegion(br.ReadBytes(CompressionRegion.UnitOffset - CompressionRegion.ShopOffset));
                UnitRegion = new ChapterSaveRegions.UnitRegion(br.ReadBytes(CompressionRegion.WeaponNameOffset - CompressionRegion.UnitOffset));
                WeaponNameRegion = new ChapterSaveRegions.WeaponNameRegion(br.ReadBytes(CompressionRegion.ConvoyOffset - CompressionRegion.WeaponNameOffset));
                ConvoyRegion = new ChapterSaveRegions.ConvoyRegion(br.ReadBytes(CompressionRegion.MyCastleOffset - CompressionRegion.ConvoyOffset));
                MyCastleRegion = new ChapterSaveRegions.MyCastleRegion(br.ReadBytes((int)br.BaseStream.Length - CompressionRegion.MyCastleOffset));
            }
        }

        public void Write()
        {
            // Get bytes from regions
            var userBytes = UserRegion.Raw;
            var lk08Bytes = LK08Region.Raw;
            var battlefieldBytes = BattlefieldRegion.Raw;
            var shopBytes = ShopRegion.Raw;
            var unitBytes = UnitRegion.Raw;
            var weaponNameBytes = WeaponNameRegion.Raw;
            var convoyBytes = ConvoyRegion.Raw;
            var myCastleBytes = MyCastleRegion.Raw;

            // Update offsets, in case regions changed size
            var offset = CompressionRegion.UserOffset; // First region, offset is always the same
            offset += userBytes.Length;
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

            // Combine regions and write to file
            _file.DecompressedBytes = Header.Raw
                .Concat(CompressionRegion.Raw)
                .Concat(userBytes)
                .Concat(lk08Bytes)
                .Concat(battlefieldBytes)
                .Concat(shopBytes)
                .Concat(unitBytes)
                .Concat(weaponNameBytes)
                .Concat(convoyBytes)
                .Concat(myCastleBytes)
                .ToArray();

            _file.Write();
        }

        #endregion
    }
}
