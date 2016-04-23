using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FEFTwiddler.Extensions;

namespace FEFTwiddler.Model.NewGamePlus
{
    public class Randomizer
    {
        private Random _rng;
        private Model.ChapterSave _chapterSave;

        public Randomizer(Model.ChapterSave chapterSave)
        {
            _rng = new Random();
            _chapterSave = chapterSave;
        }

        public void RandomizeClasses()
        {
            foreach (var unit in _chapterSave.UnitRegion.Units)
            {
                unit.ClassID = GetRandomClass();
            }
        }

        public Enums.Class GetRandomClass()
        {
            // TODO: Restrict by gender
            var classes = Data.Database.Classes.GetAll().Where((x) => (byte)x.ClassID > 0x00 && (byte)x.ClassID < 0x6A);
            return classes.RandomElement(_rng).ClassID;
        }

        public void EquipWeapons()
        {
            foreach (var unit in _chapterSave.UnitRegion.Units)
            {
                var classData = Data.Database.Classes.GetByID(unit.ClassID);
                var items = new List<InventoryItem>();

                if (classData.UsesSword)
                {
                    var weapon = Data.Database.Items.GetAll()
                        .Where((x) => x.Type == Enums.ItemType.Sword && x.WeaponRank <= Enums.WeaponRank.B)
                        .RandomElement(_rng);

                    items.Add(InventoryItem.FromID(weapon.ItemID));
                    unit.WeaponExperience_Sword = Character.WeaponExperienceForRankB;
                }

                if (classData.UsesLance)
                {
                    var weapon = Data.Database.Items.GetAll()
                        .Where((x) => x.Type == Enums.ItemType.Lance && x.WeaponRank <= Enums.WeaponRank.B)
                        .RandomElement(_rng);

                    items.Add(InventoryItem.FromID(weapon.ItemID));
                    unit.WeaponExperience_Lance = Character.WeaponExperienceForRankB;
                }

                if (classData.UsesAxe)
                {
                    var weapon = Data.Database.Items.GetAll()
                        .Where((x) => x.Type == Enums.ItemType.Axe && x.WeaponRank <= Enums.WeaponRank.B)
                        .RandomElement(_rng);

                    items.Add(InventoryItem.FromID(weapon.ItemID));
                    unit.WeaponExperience_Axe = Character.WeaponExperienceForRankB;
                }

                if (classData.UsesShuriken)
                {
                    var weapon = Data.Database.Items.GetAll()
                        .Where((x) => x.Type == Enums.ItemType.Shuriken && x.WeaponRank <= Enums.WeaponRank.B)
                        .RandomElement(_rng);

                    items.Add(InventoryItem.FromID(weapon.ItemID));
                    unit.WeaponExperience_Shuriken = Character.WeaponExperienceForRankB;
                }

                if (classData.UsesBow)
                {
                    var weapon = Data.Database.Items.GetAll()
                        .Where((x) => x.Type == Enums.ItemType.Bow && x.WeaponRank <= Enums.WeaponRank.B)
                        .RandomElement(_rng);

                    items.Add(InventoryItem.FromID(weapon.ItemID));
                    unit.WeaponExperience_Bow = Character.WeaponExperienceForRankB;
                }

                if (classData.UsesTome)
                {
                    var weapon = Data.Database.Items.GetAll()
                        .Where((x) => x.Type == Enums.ItemType.Tome && x.WeaponRank <= Enums.WeaponRank.B)
                        .RandomElement(_rng);

                    items.Add(InventoryItem.FromID(weapon.ItemID));
                    unit.WeaponExperience_Tome = Character.WeaponExperienceForRankB;
                }

                if (classData.UsesStaff)
                {
                    var weapon = Data.Database.Items.GetAll()
                        .Where((x) => x.Type == Enums.ItemType.Staff && x.WeaponRank <= Enums.WeaponRank.B)
                        .RandomElement(_rng);

                    items.Add(InventoryItem.FromID(weapon.ItemID));
                    unit.WeaponExperience_Staff = Character.WeaponExperienceForRankB;
                }

                if (classData.UsesStone)
                {
                    var weapon = Data.Database.Items.GetAll()
                        .Where((x) => x.Type == Enums.ItemType.Stone && x.WeaponRank <= Enums.WeaponRank.B)
                        .RandomElement(_rng);

                    items.Add(InventoryItem.FromID(weapon.ItemID));
                    unit.WeaponExperience_Stone = Character.WeaponExperienceForRankB;
                }

                // Maybe a healing item too. Or some other consumable. Have fun!
                if ((_rng.Next(100)) < 75)
                {
                    var consumable = Data.Database.Items.GetAll()
                        .Where((x) => x.Type == Enums.ItemType.Consumable)
                        .RandomElement(_rng);

                    var inventoryItem = InventoryItem.FromID(consumable.ItemID);
                    inventoryItem.Uses = consumable.MaximumUses;
                    items.Add(inventoryItem);
                }

                for (var i = 0; i < items.Count; i++)
                {
                    if (i == 0) unit.Item_1 = items[i];
                    if (i == 1) unit.Item_2 = items[i];
                    if (i == 2) unit.Item_3 = items[i];
                    if (i == 3) unit.Item_4 = items[i];
                }
            }
        }

        // TODO: Give weapons + items based on class. also A rank all weapon exp
        // TODO: Give skills (retain locktouch if original class had it?)
    }
}
