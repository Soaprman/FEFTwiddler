using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEFTwiddler.Utils
{
    public class Analytics
    {
        public static int GetItemTotalAcrossUnits(Model.ChapterSave chapterSave, Enums.Item itemId)
        {
            int count = 0;
            foreach (var unit in chapterSave.UnitRegion.Units)
            {
                if (unit.Item_1.ItemID == itemId) count++;
                if (unit.Item_2.ItemID == itemId) count++;
                if (unit.Item_3.ItemID == itemId) count++;
                if (unit.Item_4.ItemID == itemId) count++;
                if (unit.Item_5.ItemID == itemId) count++;
            }
            return count;
        }
    }
}
