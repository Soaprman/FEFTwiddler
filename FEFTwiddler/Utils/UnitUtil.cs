using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEFTwiddler.Utils
{
    public class UnitUtil
    {
        public static void RemoveNamesFromHeldWeaponsWithInvalidNames(Model.IChapterSave chapterSave, Model.Unit unit)
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

        public static void Undeploy(Model.Unit unit)
        {
            if (unit.UnitBlock == Enums.UnitBlock.Deployed)
            {
                unit.UnitBlock = Enums.UnitBlock.Living;
                unit.Position_FromLeft = Model.Unit.PositionIfNotDeployed;
                unit.Position_FromTop = Model.Unit.PositionIfNotDeployed;

                unit.RawDeployedUnitInfo = Model.Unit.GetEmptyDeployedInfoBlock();
            }
        }

        /// <summary>
        /// Set a unit's block based on its data (whether it's dead, etc.)
        /// </summary>
        public static void FixBlock(Model.Unit unit)
        {
            if (unit.WasKilledByPlot) unit.UnitBlock = Enums.UnitBlock.DeadByPlot;
            else if (unit.IsDead) unit.UnitBlock = Enums.UnitBlock.DeadByGameplay;
            else if (unit.RawDeployedUnitInfo.Length > 0) unit.UnitBlock = Enums.UnitBlock.Deployed;
            else unit.UnitBlock = Enums.UnitBlock.Living;
        }
    }
}
