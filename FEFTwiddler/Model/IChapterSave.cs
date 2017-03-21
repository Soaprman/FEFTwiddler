namespace FEFTwiddler.Model
{
    public interface IChapterSave : ISave
    {
        ChapterSaveRegions.Header Header { get; set; }
        ChapterSaveRegions.UserRegion UserRegion { get; set; }
        ChapterSaveRegions.LK08Region LK08Region { get; set; }
        ChapterSaveRegions.BattlefieldRegion BattlefieldRegion { get; set; }
        ChapterSaveRegions.ShopRegion ShopRegion { get; set; }
        ChapterSaveRegions.UnitRegion UnitRegion { get; set; }
        ChapterSaveRegions.WeaponNameRegion WeaponNameRegion { get; set; }
        ChapterSaveRegions.ConvoyRegion ConvoyRegion { get; set; }
        ChapterSaveRegions.MyCastleRegion MyCastleRegion { get; set; }
    }
}
