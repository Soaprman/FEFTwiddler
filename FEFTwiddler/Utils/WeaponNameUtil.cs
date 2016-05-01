using System.Linq;

namespace FEFTwiddler.Utils
{
    public class WeaponNameUtil
    {
        public static void RemoveWeaponNameIfUnused(Model.ChapterSave chapterSave, byte nameId)
        {
            bool nameInUse = false;

            nameInUse = chapterSave.ConvoyRegion.Convoy.Any((x) => x.IsNamed && x.WeaponNameID == nameId);

            if (!nameInUse)
            {
                foreach (var unit in chapterSave.UnitRegion.Units)
                {
                    nameInUse |= unit.Item_1.IsNamed && unit.Item_1.WeaponNameID == nameId;
                    nameInUse |= unit.Item_2.IsNamed && unit.Item_2.WeaponNameID == nameId;
                    nameInUse |= unit.Item_3.IsNamed && unit.Item_3.WeaponNameID == nameId;
                    nameInUse |= unit.Item_4.IsNamed && unit.Item_4.WeaponNameID == nameId;
                    nameInUse |= unit.Item_5.IsNamed && unit.Item_5.WeaponNameID == nameId;

                    if (nameInUse) break;
                }
            }

            if (!nameInUse)
            {
                chapterSave.WeaponNameRegion.WeaponNames.RemoveAll((x) => x.ID == nameId);
            }
        }
    }
}
