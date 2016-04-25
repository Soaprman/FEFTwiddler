using System.Drawing;

namespace FEFTwiddler.Data
{
    public class Item
    {
        public Enums.Item ItemID { get; set; }
        public string DisplayName { get; set; }
        public Enums.ItemType Type { get; set; }
        public Enums.ItemSubType SubType { get; set; }
        public Enums.WeaponRank WeaponRank { get; set; }

        /// <summary>
        /// The maximum number of uses for this item. 0 = no maximum
        /// </summary>
        public byte MaximumUses { get; set; }

        /// <summary>
        /// Whether the item is normally only seen in the hands of an enemy or NPC
        /// </summary>
        public bool IsNpcOnly { get; set; }

        /// <summary>
        /// Whether the item disappears when leaving a map (example: Chest Key)
        /// </summary>
        public bool IsMapOnly { get; set; }


        public Bitmap GetIcon()
        {
            var imageName = "";

            switch (SubType)
            {
                case Enums.ItemSubType.Sword:
                    imageName = "ItemSubType_Sword"; break;
                case Enums.ItemSubType.Katana:
                    imageName = "ItemSubType_Katana"; break;
                case Enums.ItemSubType.Lance:
                    imageName = "ItemSubType_Lance"; break;
                case Enums.ItemSubType.Naginata:
                    imageName = "ItemSubType_Naginata"; break;
                case Enums.ItemSubType.Axe:
                    imageName = "ItemSubType_Axe"; break;
                case Enums.ItemSubType.Club:
                    imageName = "ItemSubType_Club"; break;
                case Enums.ItemSubType.Dagger:
                    imageName = "ItemSubType_Dagger"; break;
                case Enums.ItemSubType.Shuriken:
                    imageName = "ItemSubType_Shuriken"; break;
                case Enums.ItemSubType.Bow:
                    imageName = "ItemSubType_Bow"; break;
                case Enums.ItemSubType.Yumi:
                    imageName = "ItemSubType_Yumi"; break;
                case Enums.ItemSubType.Tome:
                    imageName = "ItemSubType_Tome"; break;
                case Enums.ItemSubType.Scroll:
                    imageName = "ItemSubType_Scroll"; break;
                case Enums.ItemSubType.Staff:
                    imageName = "ItemSubType_Staff"; break;
                case Enums.ItemSubType.Rod:
                    imageName = "ItemSubType_Rod"; break;
                case Enums.ItemSubType.Dragonstone:
                    imageName = "ItemSubType_Dragonstone"; break;
                case Enums.ItemSubType.Beaststone:
                    imageName = "ItemSubType_Beaststone"; break;
                case Enums.ItemSubType.HealingItem:
                    imageName = "ItemSubType_HealingItem"; break;
                case Enums.ItemSubType.StatTonic:
                case Enums.ItemSubType.StatBooster:
                    imageName = "ItemSubType_StatBooster"; break;
                case Enums.ItemSubType.SpecialConsumable:
                    imageName = "ItemSubType_SpecialConsumable"; break;
                case Enums.ItemSubType.ClassChanger:
                    imageName = "ItemSubType_ClassChanger"; break;
                case Enums.ItemSubType.SkillScroll:
                    imageName = "ItemSubType_SkillScroll"; break;
                case Enums.ItemSubType.Emblem:
                    imageName = "ItemSubType_Emblem"; break;
                case Enums.ItemSubType.Breath:
                    imageName = "ItemSubType_Breath"; break;
                case Enums.ItemSubType.Fist:
                    imageName = "ItemSubType_Fist"; break;
                case Enums.ItemSubType.Rock:
                    imageName = "ItemSubType_Rock"; break;
                case Enums.ItemSubType.Saw:
                    imageName = "ItemSubType_Saw"; break;
                case Enums.ItemSubType.GoldBar:
                    imageName = "ItemSubType_GoldBar"; break;
                case Enums.ItemSubType.Gold:
                    imageName = "ItemSubType_Gold"; break;
                case Enums.ItemSubType.Key:
                    imageName = "ItemSubType_Key"; break;
                case Enums.ItemSubType.Obstacle:
                    imageName = "ItemSubType_Obstacle"; break;
                case Enums.ItemSubType.Unknown:
                default:
                    imageName = "ItemSubType_Unknown"; break;
            }

            return (Bitmap)Properties.Resources.ResourceManager.GetObject(imageName);
        }
    }
}
