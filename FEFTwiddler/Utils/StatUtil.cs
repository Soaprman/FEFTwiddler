using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEFTwiddler.Utils
{
    class StatUtil
    {
        /// <summary>
        /// Calculate base stats for a unit.
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        public static Model.Stat CalculateBaseStats(Model.Character character)
        {
            Model.Stat baseStats = new Model.Stat();
            if (!Enum.IsDefined(typeof(Enums.Character), character.CharacterID) ||
                !Enum.IsDefined(typeof(Enums.Class), character.ClassID))
                return null;
            
            var characterData = Data.Database.Characters.GetByID(character.CharacterID);
            var classData = Data.Database.Classes.GetByID(character.ClassID);

            // Corrin and Bond units
            if (character.CorrinName != null)
            {
                if (Enum.IsDefined(typeof(Enums.Stat), character.Boon) &&
                    Enum.IsDefined(typeof(Enums.Stat), character.Bane))
                {
                    return characterData.BaseStats +
                        Data.Database.Stats.GetByID(character.Boon).BaseBoonModifiers +
                        Data.Database.Stats.GetByID(character.Bane).BaseBaneModifiers +
                        classData.BaseStats;
                }
                else
                    return null;
            }
            else // All other units
            {
                return characterData.BaseStats + classData.BaseStats;
            }
        }

        /// <summary>
        /// Calculate the true stats for a character
        /// </summary>
        /// <returns>A character's true stats</returns>
        public static Model.Stat CalculateTrueStats(Model.Character character)
        {
            Model.Stat baseStats = CalculateBaseStats(character);
            if (baseStats != null)
                return baseStats + character.GainedStats;
            else
                return null;
        }

        /// <summary>
        /// Calculate stat caps for a character
        /// </summary>
        /// <returns>Character's stat caps</returns>
        public static Model.Stat CalculateStatCaps(Model.Character character)
        {
            if (!Enum.IsDefined(typeof(Enums.Character), character.CharacterID) ||
                !Enum.IsDefined(typeof(Enums.Class), character.ClassID))
                return null;

            var characterData = Data.Database.Characters.GetByID(character.CharacterID);
            var classData = Data.Database.Classes.GetByID(character.ClassID);

            if (character.CorrinName != null) // Corrin and bond units
            {
                if (Enum.IsDefined(typeof(Enums.Stat), character.Boon) &&
                    Enum.IsDefined(typeof(Enums.Stat), character.Bane))
                {
                    return classData.MaximumStats +
                        Data.Database.Stats.GetByID(character.Boon).MaxBoonModifiers +
                        Data.Database.Stats.GetByID(character.Bane).MaxBaneModifiers +
                        character.StatueBonusStats;
                }
                else
                    return null;
            }
            else if (characterData.IsChild) // Children
            {
                var childBonusStats = new Model.Stat { HP = 0, Str = 1, Mag = 1, Skl = 1, Spd = 1, Lck = 1, Def = 1, Res = 1 };
                return classData.MaximumStats +
                    Data.Database.Characters.GetByID(character.FatherID).Modifiers +
                    Data.Database.Stats.GetByID(character.FatherBoon).MaxBoonModifiers +
                    Data.Database.Stats.GetByID(character.FatherBane).MaxBaneModifiers +
                    Data.Database.Characters.GetByID(character.MotherID).Modifiers +
                    Data.Database.Stats.GetByID(character.MotherBoon).MaxBoonModifiers +
                    Data.Database.Stats.GetByID(character.MotherBane).MaxBaneModifiers +
                    childBonusStats + character.StatueBonusStats;
            }
            else // All other units
            {
                return classData.MaximumStats + characterData.Modifiers + character.StatueBonusStats;
            }
        }

        /// <summary>
        /// Calculate stats for a character
        /// </summary>
        /// <returns>Character's stats</returns>
        public static Model.Stat CalculateStats(Model.Character character)
        {
            Model.Stat stats = new Model.Stat();
            Model.Stat trueStats = CalculateTrueStats(character);
            Model.Stat caps = CalculateStatCaps(character);

            if (trueStats != null && caps != null)
            {
                if (trueStats.HP < 0) stats.HP = 0; else if (trueStats.HP < caps.HP) stats.HP = trueStats.HP; else stats.HP = caps.HP;
                if (trueStats.Str < 0) stats.Str = 0; else if (trueStats.Str < caps.Str) stats.Str = trueStats.Str; else stats.Str = caps.Str;
                if (trueStats.Mag < 0) stats.Mag = 0; else if (trueStats.Mag < caps.Mag) stats.Mag = trueStats.Mag; else stats.Mag = caps.Mag;
                if (trueStats.Skl < 0) stats.Skl = 0; else if (trueStats.Skl < caps.Skl) stats.Skl = trueStats.Skl; else stats.Skl = caps.Skl;
                if (trueStats.Spd < 0) stats.Spd = 0; else if (trueStats.Spd < caps.Spd) stats.Spd = trueStats.Spd; else stats.Spd = caps.Spd;
                if (trueStats.Lck < 0) stats.Lck = 0; else if (trueStats.Lck < caps.Lck) stats.Lck = trueStats.Lck; else stats.Lck = caps.Lck;
                if (trueStats.Def < 0) stats.Def = 0; else if (trueStats.Def < caps.Def) stats.Def = trueStats.Def; else stats.Def = caps.Def;
                if (trueStats.Res < 0) stats.Res = 0; else if (trueStats.Res < caps.Res) stats.Res = trueStats.Res; else stats.Res = caps.Res;
            }
            else
            {
                stats = null;
            }
            return stats;
        }
    }
}
