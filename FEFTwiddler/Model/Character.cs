using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEFTwiddler.Model
{
    public class Character: ISavePart
    {
        /// <summary>
        /// The location of this character in the hex
        /// </summary>
        /// <remarks>This technique goes out the window if we allow adding or switching character positions. But it's fine for now.</remarks>
        public long BinaryPosition { get; set; }

        public byte Level { get; set; }
        public byte Experience { get; set; }
        public byte Unknown00C { get; set; }
        public byte EternalSealsUsed { get; set; }
        public Enums.Character CharacterID { get; set; }
        public byte Unknown00F { get; set; }
        public Enums.Class ClassID { get; set; }
        public byte Unknown011 { get; set; }

        public byte WeaponExperience_Sword { get; set; }
        public byte WeaponExperience_Lance { get; set; }
        public byte WeaponExperience_Axe { get; set; }
        public byte WeaponExperience_Shuriken { get; set; }
        public byte WeaponExperience_Bow { get; set; }
        public byte WeaponExperience_Tome { get; set; }
        public byte WeaponExperience_Staff { get; set; }
        public byte WeaponExperience_Stone { get; set; }

        public byte MaximumHP { get; set; }

        public Enums.Skill EquippedSkill_1 { get; set; }
        public Enums.Skill EquippedSkill_2 { get; set; }
        public Enums.Skill EquippedSkill_3 { get; set; }
        public Enums.Skill EquippedSkill_4 { get; set; }
        public Enums.Skill EquippedSkill_5 { get; set; }

        public bool IsDead { get; set; }
        public bool IsRecruited { get; set; }

        public byte[] LearnedSkills { get; set; }

        public override string ToString()
        {
            return CharacterID.ToString() + ": " + ClassID.ToString() +  " // lv" + Level.ToString() + " exp" + Experience.ToString();
        }

        #region Cheats

        public void LearnAllNonNpcSkills()
        {
            LearnedSkills = new byte[] { 0xDF, 0xFF, 0x7F, 0xFB, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFE, 0xFF, 0xEF, 0xFF, 0xBF, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00 };
        }

        public void SRankAllWeapons()
        {
            byte sRank = 0xFB;
            WeaponExperience_Sword = sRank;
            WeaponExperience_Lance = sRank;
            WeaponExperience_Axe = sRank;
            WeaponExperience_Shuriken = sRank;
            WeaponExperience_Bow = sRank;
            WeaponExperience_Tome = sRank;
            WeaponExperience_Staff = sRank;
            WeaponExperience_Stone = sRank;
        }

        #endregion

        #region Character enum lookup stuff

        public static bool IsCorrin(Enums.Character charId)
        {
            return charId == Enums.Character.Corrin_F || charId == Enums.Character.Corrin_M;
        }

        public static bool IsAzura(Enums.Character charId)
        {
            return charId == Enums.Character.Azura;
        }

        public static bool IsGen1Ordinary(Enums.Character charId)
        {
            return charId == Enums.Character.Arthur ||
                charId == Enums.Character.Azama ||
                charId == Enums.Character.Benny ||
                charId == Enums.Character.Beruka ||
                charId == Enums.Character.Camilla ||
                charId == Enums.Character.Charlotte ||
                charId == Enums.Character.Effie ||
                charId == Enums.Character.Elise ||
                charId == Enums.Character.Felicia ||
                charId == Enums.Character.Hana ||
                charId == Enums.Character.Hayato ||
                charId == Enums.Character.Hinata ||
                charId == Enums.Character.Hinoka ||
                charId == Enums.Character.Jakob ||
                charId == Enums.Character.Kaden ||
                charId == Enums.Character.Kagero ||
                charId == Enums.Character.Kaze ||
                charId == Enums.Character.Keaton ||
                charId == Enums.Character.Laslow ||
                charId == Enums.Character.Leo ||
                charId == Enums.Character.Niles ||
                charId == Enums.Character.Nyx ||
                charId == Enums.Character.Oboro ||
                charId == Enums.Character.Odin ||
                charId == Enums.Character.Orochi ||
                charId == Enums.Character.Peri ||
                charId == Enums.Character.Rinkah ||
                charId == Enums.Character.Ryoma ||
                charId == Enums.Character.Saizo ||
                charId == Enums.Character.Sakura ||
                charId == Enums.Character.Selena ||
                charId == Enums.Character.Setsuna ||
                charId == Enums.Character.Silas ||
                charId == Enums.Character.Subaki ||
                charId == Enums.Character.Takumi ||
                charId == Enums.Character.Xander;
        }

        public static bool IsGen1Special(Enums.Character charId)
        {
            return charId == Enums.Character.Anna ||
            charId == Enums.Character.Flora ||
            charId == Enums.Character.Fuga ||
            charId == Enums.Character.Gunter ||
            charId == Enums.Character.Izana ||
            charId == Enums.Character.Reina ||
            charId == Enums.Character.Scarlet ||
            charId == Enums.Character.Shura ||
            charId == Enums.Character.Yukimura;
        }

        public static bool IsKana(Enums.Character charId)
        {
            return charId == Enums.Character.Kana_F || charId == Enums.Character.Kana_M;
        }

        public static bool IsGen2Ordinary(Enums.Character charId)
        {
            return Enums.Character.Shigure <= charId && charId <= Enums.Character.Nina;
        }

        public static bool IsBoss(Enums.Character charId)
        {
            return Enums.Character.Daniela <= charId && charId <= Enums.Character.Zhara;
        }

        public static bool IsGeneric(Enums.Character charId)
        {
            return Enums.Character.Paladin <= charId && charId <= Enums.Character.Lancer;
        }

        public static bool IsAmiibo(Enums.Character charId)
        {
            return Enums.Character.Marth <= charId && charId <= Enums.Character.Robin;
        }

        public static int GetBlockSize(Enums.Character charId)
        {
            switch (charId)
            {
                case Enums.Character.Corrin_F: // guess
                case Enums.Character.Corrin_M:
                    return 0x15A;
                case Enums.Character.Azura:
                case Enums.Character.Felicia:
                case Enums.Character.Jakob:
                case Enums.Character.Kaze:
                case Enums.Character.Mozu:
                case Enums.Character.Silas:
                    return 0x103;
                case Enums.Character.Arthur:
                case Enums.Character.Azama:
                case Enums.Character.Beruka:
                case Enums.Character.Camilla:
                case Enums.Character.Charlotte:
                case Enums.Character.Effie:
                case Enums.Character.Elise:
                case Enums.Character.Hana:
                case Enums.Character.Hayato:
                case Enums.Character.Hinata:
                case Enums.Character.Hinoka:
                case Enums.Character.Kaden:
                case Enums.Character.Kagero:
                case Enums.Character.Keaton:
                case Enums.Character.Laslow:
                case Enums.Character.Leo:
                case Enums.Character.Niles:
                case Enums.Character.Nyx:
                case Enums.Character.Oboro:
                case Enums.Character.Odin:
                case Enums.Character.Orochi:
                case Enums.Character.Peri:
                case Enums.Character.Rinkah:
                case Enums.Character.Ryoma:
                case Enums.Character.Saizo:
                case Enums.Character.Sakura:
                case Enums.Character.Selena:
                case Enums.Character.Setsuna:
                case Enums.Character.Subaki:
                case Enums.Character.Takumi:
                case Enums.Character.Xander:
                    return 0xFE;
                case Enums.Character.Benny:
                    return 0xFD;
                case Enums.Character.Kana_F:
                case Enums.Character.Kana_M: // guess
                case Enums.Character.Ophelia:
                case Enums.Character.Nina:
                    return 0x123;
                case Enums.Character.Asugi:
                case Enums.Character.Caeldori:
                case Enums.Character.Forrest:
                case Enums.Character.Hisame:
                case Enums.Character.Ignatius:
                case Enums.Character.Kiragi:
                case Enums.Character.Mitama:
                case Enums.Character.Percy:
                case Enums.Character.Rhajat:
                case Enums.Character.Selkie:
                case Enums.Character.Shigure:
                case Enums.Character.Shiro:
                case Enums.Character.Siegbert:
                case Enums.Character.Soleil:
                case Enums.Character.Velouria:
                    return 0x124;
                case Enums.Character.Dwyer:
                case Enums.Character.Midori:
                case Enums.Character.Sophie:
                    return 0x125;
                case Enums.Character.Flora:
                case Enums.Character.Fuga:
                case Enums.Character.Gunter:
                    return 0xEE;
                case Enums.Character.Anna: // guess
                case Enums.Character.Izana: // guess
                case Enums.Character.Reina:
                case Enums.Character.Scarlet: // guess
                case Enums.Character.Shura:
                case Enums.Character.Yukimura: // guess
                    return 0xED;
                case Enums.Character.Banba: // guess
                case Enums.Character.Candace:
                case Enums.Character.Daichi:
                case Enums.Character.Daniela: // guess
                case Enums.Character.Funke: // guess
                case Enums.Character.Gazak:
                case Enums.Character.Haitaka: // guess
                case Enums.Character.Kumagera: // guess
                case Enums.Character.Llewelyn: // guess
                case Enums.Character.Lloyd: // guess
                case Enums.Character.Nichol:
                case Enums.Character.Senno:
                case Enums.Character.Zhara:
                    return 0xEB;
                default: // lazily handle the generics
                    return 0xEB;
            }
        }

        public int GetSupportBlockSize()
        {
            return GetSupportBlockSize(this.CharacterID);
        }

        public static int GetSupportBlockSize(Enums.Character charId)
        {
            var supportSize = GetBlockSize(charId);

            // TODO
            supportSize -= 9;

            // Character main data
            supportSize -= 8;

            // TODO
            supportSize -= 70;

            // Skills
            supportSize -= 10;

            // Inventory
            supportSize -= 25;

            // Unknown stuff right after support block
            supportSize -= 17;
            
            // Everything else
            supportSize -= 90;
            
            // Corrin has 44 bytes of stuff at the end
            if (IsCorrin(charId))
            {
                supportSize -= 44;
            }

            // Gen 2 units have supports split into two blocks
            if (IsKana(charId) || IsGen2Ordinary(charId))
            {
                supportSize -= 42;
            }

            return supportSize;
        }

        public int GetSupportBlock2Size()
        {
            return GetSupportBlock2Size(this.CharacterID);
        }

        public static int GetSupportBlock2Size(Enums.Character charId)
        {
            // Gen 2 units have supports split into two blocks
            if (IsKana(charId) || IsGen2Ordinary(charId))
            {
                return 42;
            }
            else
            {
                return 0;
            }
        }

        public int GetEndBlockSize()
        {
            if (IsCorrin(this.CharacterID))
            {
                return 44;
            }
            else
            {
                return 0;
            }
        }

        #endregion
    }
}