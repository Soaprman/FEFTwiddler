using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEFTwiddler.Utils
{
    public class UnitUtil
    {
        public static void RemoveNamesFromHeldWeaponsWithInvalidNames(Model.ChapterSave chapterSave, Model.Unit unit)
        {
            if (unit.Item_1.IsNamed && !chapterSave.WeaponNameRegion.WeaponNames.Any((x) => x.ID == unit.Item_1.WeaponNameID))
            {
                unit.Item_1.IsNamed = false;
            }
            if (unit.Item_2.IsNamed && !chapterSave.WeaponNameRegion.WeaponNames.Any((x) => x.ID == unit.Item_2.WeaponNameID))
            {
                unit.Item_2.IsNamed = false;
            }
            if (unit.Item_3.IsNamed && !chapterSave.WeaponNameRegion.WeaponNames.Any((x) => x.ID == unit.Item_3.WeaponNameID))
            {
                unit.Item_3.IsNamed = false;
            }
            if (unit.Item_4.IsNamed && !chapterSave.WeaponNameRegion.WeaponNames.Any((x) => x.ID == unit.Item_4.WeaponNameID))
            {
                unit.Item_4.IsNamed = false;
            }
            if (unit.Item_5.IsNamed && !chapterSave.WeaponNameRegion.WeaponNames.Any((x) => x.ID == unit.Item_5.WeaponNameID))
            {
                unit.Item_5.IsNamed = false;
            }
        }
    }
}
